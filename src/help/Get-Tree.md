---
external help file: TIToolsDll.dll-Help.xml
Module Name: TITools
online version:
schema: 2.0.0
---

# Get-Tree

## SYNOPSIS

Show directories/files Tree-diagram.

## SYNTAX

```
Get-Tree [-DirPath] <String> [[-Deco] <DecorationType>] [[-DirOnly] <Boolean>] [<CommonParameters>]
```

## DESCRIPTION

Show directories/files Tree-diagram.

## EXAMPLES

### Example 1

```powershell
PS C:\> Get-Tree .
```

Get a tree diagram from current directory.

## PARAMETERS

### -Deco

Select a decoration type.

```yaml
Type: DecorationType
Parameter Sets: (All)
Aliases:
Accepted values: Default, DotLine, Short, Ascii, AsciiWide, AsciiShort, AsciiVeryShort, AsciiUltraShort

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -DirOnly

Show only directories or not.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -DirPath

Root directory path.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### TIToolsDll.Tree.DecorationType

### System.Boolean

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
