. .\scripts\Get-ConnectionString.ps1
. .\scripts\Update-Database.ps1
. .\scripts\Drop-Database.ps1

$basePath = Get-Location
$srcPath = "$basePath/src"
$apiProject = "$srcPath\Sebastian.Api"
$testProject = "$srcPath\Sebastian.Tests"
$targetFramework = "netcoreapp2.1"
$configuration = 'Debug'
$databaseProjPath = "$srcPath/Sebastian.Database"

$devConnectionString = Get-ConnectionString $apiProject;
$testConnectionString = Get-ConnectionString $testProject;

# Synopsis: Builds solution and runs integration tests
Task CI {}

# Synopsis: Builds solution
Task Build -Before Run-Tests, CI {
    Set-Location -Path $srcPath
    dotnet build --configuration $configuration
    Set-Location -Path $basePath
}

# Synopsis: Runs integration tests
Task Run-Tests -Before CI {
    Set-Location -Path $testProject
    dotnet fixie
    Set-Location -Path $basePath
}

# Synopsis: Drops and recreates dev database
Task Reset-Dev-Database {
    Drop-Database $devConnectionString
    Update-Database $devConnectionString DEV $databaseProjPath
}

# Synopsis: Runs new migration scripts on dev database
Task Update-Dev-Database {
    Update-Database $devConnectionString DEV $databaseProjPath
}

# Synopsis: Drops and recreates test database
Task Reset-Test-Database -Before Run-Tests, CI {
    Drop-Database $testConnectionString
    Update-Database $testConnectionString TEST $databaseProjPath
}

# Synopsis: Runs new migration scripts on test database
Task Update-Test-Database {
    Update-Database $testConnectionString TEST $databaseProjPath
}
