$dir = Join-Path (Split-Path $script:MyInvocation.MyCommand.Path) ".."
Push-Location $dir
dotnet restore --packages $dir\packages
Pop-Location