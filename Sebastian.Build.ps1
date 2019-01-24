. .\scripts\Delete-Database.ps1

$basePath = Get-Location
$srcPath = "$basePath\src"
$apiProject = "$srcPath\Sebastian.Api"
$testProject = "$srcPath\Sebastian.Tests"
$targetFramework = "netcoreapp2.1"
$configuration = 'Debug'
#$testsProjPath = "$srcPath\Sebastian.Tests"
$databaseProjPath = "$srcPath\Sebastian.Database"

function Get-App-Settings($projectPath) {
    return Get-Content -Raw -Path "$projectPath\appsettings.json" | Out-String | ConvertFrom-Json
}

function Get-Connection-String($projectPath) {
    $json = Get-App-Settings $projectPath
    return $json.Database.ConnectionString
}

$connectionStrings = @{
    DEV = Get-Connection-String $apiProject;
    TEST = Get-Connection-String $testProject;
}

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
    Drop-Database DEV
    Update-Database DEV
}

Task Update-Dev-Database {
    Update-Database DEV
}

Task Drop-Dev-Database {
    Drop-Database DEV
}

Task Refresh-Test-Database {
    Drop-Database TEST
    Update-Database TEST
}

Task Update-Test-Database {
    Update-Database TEST
}

Task Drop-Test-Database {
    Drop-Database TEST
}

Task Say-Hello {
    SayHello
}

Task Try-Delete-Db {
    Delete-Database -ConnectionString $connectionStrings["TEST"];
}

Task Exec-Rh {
    rh
}

function Update-Database([Parameter(ValueFromRemainingArguments)]$environments) {
    $migrationsProject =  "Sebastian.Database"
    $roundhouseExePath = "$basepath\tools\rh.exe"
    $roundhouseOutputDir = [System.IO.Path]::GetDirectoryName($roundhouseExePath) + "\output"

    $migrationsScriptsPath = "$srcPath\$migrationsProject"
    $roundhouseVersionFile = "$srcPath\Sebastian.Database\bin\$configuration\$targetFramework\$migrationsProject.dll"

    foreach ($environment in $environments) {
        $connectionString = $connectionStrings[$environment]

    Write-Host "Executing RoundhousE for environment:" $environment

    exec { & $roundhouseExePath --connectionstring $connectionString `
                                    --commandtimeout 300 `
                                    --env $environment `
                                    --output $roundhouseOutputDir `
                                    --sqlfilesdirectory $migrationsScriptsPath `
                                    --versionfile $roundhouseVersionFile `
                                    --transaction `
                                    --silent }
    }
}

function Drop-Database([Parameter(ValueFromRemainingArguments)]$environments) {
    $migrationsProject =  "Sebastian.Database"
    $roundhouseExePath = "$basepath\tools\rh.exe"
    $roundhouseOutputDir = [System.IO.Path]::GetDirectoryName($roundhouseExePath) + "\output"

    $migrationScriptsPath ="/"
    $roundhouseVersionFile = "$srcPath\Sebastian.Database\bin\$configuration\$targetFramework\$migrationsProject.dll"

    foreach ($environment in $environments) {
        $connectionString = $connectionStrings[$environment]

    Write-Host "Executing RoundhousE for environment:" $environment

    exec { & $roundhouseExePath --connectionstring $connectionString `
                                    --commandtimeout 300 `
                                    --drop `
                                    --silent }
    }
}

function SayHello {
    [CmdletBinding()]
     param()
    #  Import-Module SqlServer
    # [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo")
    $dbServer = New-Object Microsoft.SqlServer.Management.Smo.Server 'localhost'
    $smoSecurePassword = 'P@ssw0rd'  | ConvertTo-SecureString -asPlainText -Force
    $dbServer.ConnectionContext.LoginSecure = $false
    $dbServer.ConnectionContext.set_Login('sa')            
    $dbServer.ConnectionContext.set_SecurePassword($smoSecurePassword)
    $dbServer.ConnectionContext.Connect() 
    $db = $dbServer.Databases
    Write-Host $db.Count
}
