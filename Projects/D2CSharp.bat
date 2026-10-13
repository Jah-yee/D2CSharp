..\bin\ExtractReworked.exe -g ..\TargetGenerated -w ..\TargetWorking -r ..\TargetReworked -d
..\bin\D2CSharp.exe
xcopy /s /y ..\TargetGenerated ..\TargetWorking
xcopy /s /y ..\TargetReworked ..\TargetWorking
call ..\D2CSharpTests\Formatting\FormatAll.cmd
Pause
