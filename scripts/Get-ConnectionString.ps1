. .\scripts\Get-App-Settings.ps1

function Get-ConnectionString($projectPath) {
    $json = Get-App-Settings "$projectPath\appsettings.json"
    return $json.Database.ConnectionString
}