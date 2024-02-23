# $here = Split-Path -Parent $MyInvocation.MyCommand.Path
# $sut = (Split-Path -Leaf $MyInvocation.MyCommand.Path) -replace '\.Tests\.', '.'
# . "$here\$sut"

Describe "Test1" {
    BeforeAll {
        Import-Module $PSScriptRoot\..\output\TITools\bin\TITools.dll
    }

    Context '最初のテスト' {
        It "does something useful" {
            $true | Should Be $true
        }
        It "2つ目のテスト" {
            Resolve-MyCmdlet aaa | Should Be aaaa
        }
    }
}