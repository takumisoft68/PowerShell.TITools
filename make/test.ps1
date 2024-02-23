Set-Location $PSScriptRoot
Set-Location ../

Invoke-Pester ".\Tests"
