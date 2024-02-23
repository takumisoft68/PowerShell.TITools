# モジュールに新たにコマンドを追加したときに help ファイルのひな型を作成するスクリプト
# 事前に build.bat を実行しておく必要がある
# 事前に platyps をインストールしておく必要がある
# 出力先の .\output\temp\help ディレクトリは空または削除しておかないとエラーする
Write-Host "Start help file updating."
$module='TITools'

Import-Module "$PSScriptRoot\..\Output\$module\$module.psd1"

cd "$PSScriptRoot\..\"
Write-Host "Processing..."
New-MarkdownAboutHelp -OutputFolder ".\src\help" -AboutName $module
Update-MarkdownHelpModule -Path ".\src\help"


New-ExternalHelp -Path ".\src\help" -OutputPath ".\output\$module" -Force
Write-Host "Help markdown file update is completed."
