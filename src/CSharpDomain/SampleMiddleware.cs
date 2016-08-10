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
		private readonly IStatsd statsd;
		private readonly ILog log;

		public SampleMiddleware(OwinMiddleware next, IStatsd statsd, ILog log) : base(next)
		{
			this.statsd = statsd;
			this.log = log;
		}

		public override async Task Invoke(IOwinContext context)
		{
			try
			{
				var stopwatch = Stopwatch.StartNew();

				await Next.Invoke(context);

				LogRequest(context.Request.Path.Value, stopwatch.ElapsedMilliseconds, context.Response.StatusCode, context.Request.Uri.ToString(), context.Request.QueryString);
			}
			catch (Exception e)
			{
				log.Error("Exception invoking next owin middleware component", e);
			}
		}


		private void LogRequest(string metric, long responseTimeInMs, int statusCode, string url, QueryString queryString)
		{
			try
			{
				statsd.LogTiming($"{metric}.response_time", responseTimeInMs);
				statsd.LogCount($"{metric}.status_code.{statusCode}", 1);
			}
			catch (Exception e)
			{
				log.Error("Exception logging to Statsd", e);
			}

			if (responseTimeInMs > 2000)
			{
				log.Warn($"Call took too long ({responseTimeInMs}ms): {statusCode} {url} {queryString} ");
			}
		}
	}
}
