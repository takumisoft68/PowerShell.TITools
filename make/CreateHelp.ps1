# モジュールに新たにコマンドを追加したときに help ファイルのひな型を作成するスクリプト
# 先に build.bat を実行しておく必要がある
# 出力先の .\output\temp\help ディレクトリは空または削除しておかないとエラーする
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
 New-MarkdownHelp -Module "$module" -OutputFolder ".\output\temp\help"
 Write-Host "Help markdown file update is completed."
