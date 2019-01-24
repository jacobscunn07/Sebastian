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

Task Build -Before Run-Tests {
    Set-Location -Path $srcPath
    dotnet build --configuration $configuration
    Set-Location -Path $basePath
}

Task Run-Tests {
    Set-Location -Path $testProject
    dotnet fixie
    Set-Location -Path $basePath
}

Task Reset-Dev-Database {
    Drop-Database $devConnectionString
    Update-Database $devConnectionString DEV $databaseProjPath
}

Task Update-Dev-Database {
    Update-Database $devConnectionString DEV $databaseProjPath
}

Task Reset-Test-Database {
    Drop-Database $testConnectionString
    Update-Database $testConnectionString TEST $databaseProjPath
}

Task Update-Test-Database {
    Update-Database $testConnectionString TEST $databaseProjPath
}
