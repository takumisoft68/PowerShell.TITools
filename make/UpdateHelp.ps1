# モジュールの更新をヘルプ markdown へ自動的に取り込みたいときに実行するスクリプト
# 先に build.bat を実行しておく必要がある
# 更新内容が良い感じに取り込まれて markdown が上書きされる
Write-Host "Start help file updating."
$module='TITools'

Import-Module "$PSScriptRoot\..\Output\$module\$module.psd1"

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
 Update-MarkdownHelp -Path ".\src\help"
 Write-Host "Help markdown file update is completed."
