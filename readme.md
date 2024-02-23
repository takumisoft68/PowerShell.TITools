# PowerShell.TITools

PowerShell用のニッチな便利コマンド

## インストール

```ps
Install-Module TITools
```

## コマンド

### Get-Tree

ディレクトリ内をツリー表示する
装飾が8種類から選べるのが特徴

```txt
構文
    Get-Tree [-DirPath] <String> [[-Deco] {Default | DotLine | Short | Ascii | AsciiWide | AsciiShort | AsciiVeryShort | AsciiUltraShort}] [[-DirOnly] <Boolean>] [<CommonParameters>]
```

### Compare-Dir

２つのディレクトリを比較する

```txt
構文
    Compare-Dir [-DirPathA] <String> [-DirPathB] <String> [[-Method] {ExistOrNot | ExistOrNot_Timestamp | ExistOrNot_MD5Hash}] [[-Full]] [-ThreadCount <Int32>] [<CommonParameters>]
```
