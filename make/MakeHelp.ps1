Write-Host "Start Help file creation."
$module='TITools'

cd $PSScriptRoot
cd ../
try
{
    Get-InstalledModule platyps -ErrorAction Stop | Out-Null
}
catch
{
    Write-Host "Install Platyps."
    Install-PackageProvider -Name NuGet -Scope CurrentUser -MinimumVersion 2.8.5.201 -Force -Verbose
    Install-Module platyps -Scope CurrentUser -Force -Verbose
    Write-Host "Platyps install completed."
 }

 Write-Host "Processing..."
 New-ExternalHelp -Path ".\src\help" -OutputPath ".\output\$module" -Force
 Write-Host "Help file creation is completed."
