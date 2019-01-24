function Get-App-Settings($filePath) {
    return Get-Content -Raw -Path $filePath | Out-String | ConvertFrom-Json
}