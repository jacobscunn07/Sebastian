function Delete-Database {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true, Position=0, ParameterSetName='ConnectionString')]
        [string] $ConnectionString
    )

    Import-Module SqlServer

    $arr = $ConnectionString -split ';'
    foreach ($item in $arr) {
        $key,$value = $item -split '='
        # Write-Host "Key $key"
        # Write-Host "Value $value"

        if($key -eq 'Server') {
            $server = $value
        }
        elseif($key -eq 'Database') {
            $dbname = $value
        }
        elseif($key -eq 'User Id') {
            $user = $value
        }
        elseif($key -eq 'Password') {
            $pw = $value
        }
    }

    
    $dbServer = New-Object Microsoft.SqlServer.Management.Smo.Server $server
    $smoSecurePassword = $pw  | ConvertTo-SecureString -asPlainText -Force
    $dbServer.ConnectionContext.LoginSecure = $false
    $dbServer.ConnectionContext.set_Login($user)
    $dbServer.ConnectionContext.set_SecurePassword($smoSecurePassword)
    $dbServer.ConnectionContext.Connect() 
    $dbServer.KillDatabase($dbname)
    $db = $dbServer.Databases[$dbname]
    $db.Drop()
    # Write-Host $db
}

function Create-Database {

}