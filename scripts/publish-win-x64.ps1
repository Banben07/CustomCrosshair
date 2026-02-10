param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64"
)

$ErrorActionPreference = "Stop"
$projectRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$publishDir = Join-Path $projectRoot "artifacts/$Runtime"

Write-Host "Publishing $Runtime ($Configuration) ..."
dotnet publish (Join-Path $projectRoot "CrossfireCrosshair.csproj") `
  -c $Configuration `
  -r $Runtime `
  --self-contained true `
  -p:PublishSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true `
  -p:PublishTrimmed=false `
  -p:DebugType=None `
  -o $publishDir

$zipName = "CrossfireCrosshair-local-$Runtime.zip"
$zipPath = Join-Path $projectRoot "artifacts/$zipName"
$shaPath = Join-Path $projectRoot "artifacts/$zipName.sha256"

if (Test-Path $zipPath) {
    Remove-Item $zipPath -Force
}

Compress-Archive -Path (Join-Path $publishDir "*") -DestinationPath $zipPath -Force
$hash = (Get-FileHash $zipPath -Algorithm SHA256).Hash.ToLowerInvariant()
"$hash  $zipName" | Out-File $shaPath -Encoding ascii

Write-Host "Done:"
Write-Host "  $zipPath"
Write-Host "  $shaPath"
