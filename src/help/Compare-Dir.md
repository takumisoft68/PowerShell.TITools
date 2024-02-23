---
external help file: TIToolsDll.dll-Help.xml
Module Name: TITools
online version:
schema: 2.0.0
---

# Compare-Dir

## SYNOPSIS

Compare files in directory A and B.

## SYNTAX

```
Compare-Dir [-DirPathA] <String> [-DirPathB] <String> [[-Method] <DiffMethod>] [-Full] [-ThreadCount <Int32>]
 [<CommonParameters>]
```

## DESCRIPTION

Compare files in directory A and B.
Compare with exist or not, timestamp, MD5 hash.

## EXAMPLES

### Example 1

```powershell
PS C:\> Compare-Dir C:\temp\tagetDir1 C:\temp\targetDir2 ExistOrNot_MD5Hash -Full -ThreadCound 4 -Verbose
```

Compare C:\temp\tagetDir1 and C:\temp\targetDir2 include sub-directories.
Compare by exist or not, MD5 hash value matching.
Show result not only not matched files but also matched files.

## PARAMETERS

### -DirPathA
{{ Fill DirPathA Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -DirPathB
{{ Fill DirPathB Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Full

Show result only differed files, or all files.


```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Method

Comparing method.

```yaml
Type: DiffMethod
Parameter Sets: (All)
Aliases:
Accepted values: ExistOrNot, ExistOrNot_Timestamp, ExistOrNot_MD5Hash

Required: False
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ThreadCount

Multi thread count.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### TIToolsDll.Compare.DiffMethod

### System.Management.Automation.SwitchParameter

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
