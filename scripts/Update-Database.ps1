function Update-Database {
    [CmdletBinding()]
    param(
        [string] $connectionString,
        [string] $environment,
        [string] $migrationsScriptsPath
    )

    $roundhousePath = "$HOME\.dotnet\tools\rh";

    exec { & $roundhousePath --connectionstring $connectionString `
                                --commandtimeout 300 `
                                --env $environment `
                                --sqlfilesdirectory $migrationsScriptsPath `
                                --transaction `
                                --silent }
}