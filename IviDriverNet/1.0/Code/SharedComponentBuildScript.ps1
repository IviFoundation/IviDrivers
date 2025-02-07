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

# Define the project directory (modify this path as needed)
$sharedComponentProjectPath = "$PSScriptRoot/Ivi.DriverCore"  # Set to the project path

# Define the project directory (modify this path as needed)
$sharedComponentUnitTestProjectPath = "$PSScriptRoot/IviDriverCoreTests"  # Set to the project path

# Navigate to the project directory
#cd $sharedComponentProjectPath

# Build the project using the provided version and configuration
Write-Host "Building the .NET project with version: $version and configuration: $configuration"

# Set the version and perform the build
$buildCommand = "dotnet build $sharedComponentProjectPath --configuration $configuration /p:Version=$version"
# Build the project using the provided version and configuration
$buildCommandUnitTest = "dotnet build $sharedComponentUnitTestProjectPath --configuration $configuration /p:Version=$version"


try {
    Invoke-Expression $buildCommand
    Write-Host "Shared Component version $version build completed successfully."
	try
		{
			Invoke-Expression $buildCommandUnitTest
			Write-Host "Shared Component unit tests version $version build completed successfully."
		} catch {
			Write-Host "Build failed: $_"
} 
}catch {
    Write-Host "Build failed: $_"
}
