param (
    [string]$configuration = 'Release',
    [string]$version = '1.0.0.0'  # Default version last one is the build number
)

# Check if the version is provided
if (-not $version) {
    Write-Host "Warning: Version argument is empty, building with default version $version"
}
if (-not $configuration) {
    Write-Host "Warning: configuration argument is empty, building with default configuration"
}

# Define the Ivi.DriverCore project directory (modify this path as needed)
$sharedComponentProjectPath = "$PSScriptRoot/Ivi.DriverCore"  # Set to the project path

# Define the Ivi.DriverCoreTests project directory (modify this path as needed)
$sharedComponentUnitTestProjectPath = "$PSScriptRoot/Ivi.DriverCoreTests"  # Set to the project path

# Define the KtIviNetDriver project directory (modify this path as needed)
$drivertProjectPath = "$PSScriptRoot/KtIviNetDriver"  # Set to the project path

# Define the KtExample project directory (modify this path as needed)
$exampletProjectPath = "$PSScriptRoot/KtExample"  # Set to the project path

# Navigate to the project directory
#cd $sharedComponentProjectPath

# Build the project using the provided version and configuration
Write-Host "Building the .NET project with version: $version and configuration: $configuration"

# Set the version and perform the build
$buildCommandSharedComponent = "dotnet build $sharedComponentProjectPath --configuration $configuration /p:Version=$version"
# Build the project using the provided version and configuration
$buildCommandSharedComponentUnitTest = "dotnet build $sharedComponentUnitTestProjectPath --configuration $configuration /p:Version=$version"
# Set the version and perform the build
$buildCommandDriver = "dotnet build $drivertProjectPath --configuration $configuration /p:Version=$version"
# Build the project using the provided version and configuration
$buildCommandDriverExample = "dotnet build $exampletProjectPath --configuration $configuration /p:Version=$version"


try {
    Invoke-Expression $buildCommandSharedComponent
	Invoke-Expression $buildCommandSharedComponentUnitTest
	Invoke-Expression $buildCommandDriver
	Invoke-Expression $buildCommandDriverExample
    Write-Host "Shared Component version $version build completed successfully."

}catch {
    Write-Host "Build failed: $_"
}
