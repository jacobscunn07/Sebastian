$basePath = Get-Location
$srcPath = "$basePath\src"
$targetFramework = "netcoreapp2.1"
$configuration = 'Debug'
$testsProjPath = "$srcPath\Sebastian.Tests"
$databaseProjPath = "$srcPath\Sebastian.Database"

$connectionStrings = @{
    DEV = "Server=.;Database=Sebastian_Dev;Trusted_Connection=True;";
    TEST = "Server=.;Database=Sebastian_Test;Trusted_Connection=True;";
}

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