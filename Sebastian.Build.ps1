$basePath = Get-Location
$srcPath = "$basePath\src"
$configuration = 'Release'
$testsProjPath = "$srcPath\Sebastian.Tests"

Task Build -Before Run-Tests {
    Set-Location -Path $srcPath
    dotnet build --configuration $configuration
    Set-Location -Path $basePath
}

Task Run-Tests {
    Set-Location -Path $testsProjPath
    dotnet fixie
    Set-Location -Path $basePath
}