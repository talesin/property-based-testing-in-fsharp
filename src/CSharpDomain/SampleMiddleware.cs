using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using StatsdClient;
using log4net;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Examples
{

	public class SampleMiddleware : OwinMiddleware
	{
		private readonly Action<string,long> logTiming;
		private readonly Action<string, int> logCount;
		private readonly Action<string, Exception> error;
		private readonly Action<string> warn;

		public SampleMiddleware(OwinMiddleware next, IStatsd statsd, ILog log) : this(next, statsd.LogTiming, statsd.LogCount, log.Error, log.Warn)
		{
		}

		public SampleMiddleware(OwinMiddleware next, Action<string, long> logTiming, Action<string, int> logCount, Action<string, Exception> error, Action<string> warn) : base(next)
		{
			this.logTiming = logTiming;
			this.logCount = logCount;
			this.error = error;
			this.warn = warn;
		}

		public override async Task Invoke(IOwinContext context)
		{
			var stopwatch = Stopwatch.StartNew();

			try
			{
				await Next.Invoke(context);
			}
			catch (Exception e)
			{
				error("Failed to invoke next component", e);
			}

			LogRequest(context?.Request?.Path.Value, stopwatch.ElapsedMilliseconds, context?.Response?.StatusCode, context?.Request?.Uri?.ToString(), context?.Request?.QueryString);
		}


		private void LogRequest(string metric, long responseTimeInMs, int? statusCode, string url, QueryString? queryString)
		{
			try
			{
				logTiming($"{metric}.response_time", responseTimeInMs);
				logCount($"{metric}.status_code.{statusCode}", 1);
			}
			catch (Exception e)
			{
				error("Failed to log metrics", e);
			}

			if (responseTimeInMs > 2000)
			{
				warn($"Call took too long ({responseTimeInMs}ms): {statusCode} {url} {queryString} ");
			}
		}
	}
}
