$dir = Join-Path (Split-Path $script:MyInvocation.MyCommand.Path) ".."
Push-Location $dir\src\Eg
dotnet test
Pop-Location