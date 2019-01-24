. .\scripts\Update-Database.ps1
. .\scripts\Drop-Database.ps1

$basePath = Get-Location
$srcPath = "$basePath/src"
$apiProject = "$srcPath\Sebastian.Api"
$testProject = "$srcPath\Sebastian.Tests"
$targetFramework = "netcoreapp2.1"
$configuration = 'Debug'
$databaseProjPath = "$srcPath/Sebastian.Database"

function Get-App-Settings($projectPath) {
    return Get-Content -Raw -Path "$projectPath\appsettings.json" | Out-String | ConvertFrom-Json
}

function Get-Connection-String($projectPath) {
    $json = Get-App-Settings $projectPath
    return $json.Database.ConnectionString
}

$devConnectionString = Get-Connection-String $apiProject;
$testConnectionString = Get-Connection-String $testProject;

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

Task Refresh-Dev-Database {
    Drop-Database $devConnectionString
    Update-Database $devConnectionString DEV $databaseProjPath
}

Task Update-Dev-Database {
    Update-Database $devConnectionString DEV $databaseProjPath
}

Task Drop-Dev-Database {
    Drop-Database $devConnectionString
}

Task Refresh-Test-Database {
    Drop-Database $testConnectionString
    Update-Database $testConnectionString TEST $databaseProjPath
}

Task Update-Test-Database {
    Update-Database $testConnectionString TEST $databaseProjPath
}

Task Drop-Test-Database {
    Drop-Database $testConnectionString
}
