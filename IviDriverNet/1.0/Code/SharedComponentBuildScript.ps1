param (
    [string]$configuration = 'Release',
    [string]$version = ''  # Defaults to Major.Minor.Patch.0 from Version.props. The last number is the build number
)

$ErrorActionPreference = 'Stop'

# Single source of truth for the version: Version.props
if (-not $version) {
    [xml]$versionXml = Get-Content "$PSScriptRoot/Version.props" -Raw
    $group = @($versionXml.Project.PropertyGroup)[0]
    if (-not ($group.IviMajorVersion -and $group.IviMinorVersion -and $group.IviPatchVersion)) {
        Write-Error "Could not read IviMajorVersion/IviMinorVersion/IviPatchVersion from $PSScriptRoot/Version.props"
    }
    $version = "$($group.IviMajorVersion).$($group.IviMinorVersion).$($group.IviPatchVersion).0"
}
if (-not $configuration) {
    $configuration = 'Release'
    Write-Host "Warning: configuration argument is empty, building with default configuration $configuration"
}

# Projects to build, in dependency order: shared component, sample driver, sample example.
$projects = @(
    "$PSScriptRoot/Ivi.DriverCore",
    "$PSScriptRoot/AcmeDriver",
    "$PSScriptRoot/AcmeExample"
)

Write-Host "Building with version: $version and configuration: $configuration"

foreach ($project in $projects) {
    Write-Host "Building $project"
    dotnet build $project --configuration $configuration /p:Version=$version
    # dotnet is a native command: failures set the exit code instead of throwing, so check it explicitly.
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed for $project (exit code $LASTEXITCODE)."
        exit $LASTEXITCODE
    }
}

Write-Host "Shared Component version $version build completed successfully."
