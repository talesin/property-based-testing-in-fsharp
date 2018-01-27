$dir = Join-Path (Split-Path $script:MyInvocation.MyCommand.Path) ".."
Push-Location $dir
dotnet build --packages $dir\packages
Pop-Location