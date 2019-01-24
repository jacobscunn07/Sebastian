function Drop-Database {
    [CmdletBinding()]
    param(
        [string] $connectionString
    )

    $roundhousePath = "$HOME\.dotnet\tools\rh";

    exec { & $roundhousePath --connectionstring $connectionString `
                                    --commandtimeout 300 `
                                    --drop `
                                    --silent }
}