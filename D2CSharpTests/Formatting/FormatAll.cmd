@echo off
setlocal

pushd "%~dp0"

echo Formatting TargetGenerated (Tests and UserCode)...
dotnet format GeneratedFormatting.csproj whitespace --no-restore

if errorlevel 1 (
    echo.
    echo Formatting TargetGenerated failed.
    popd
    exit /b 1
)

echo.
echo Formatting TargetWorking (Tests and UserCode)...
dotnet format WorkingFormatting.csproj whitespace --no-restore

if errorlevel 1 (
    echo.
    echo Formatting TargetWorking failed.
    popd
    exit /b 1
)

echo.
echo TargetGenerated and TargetWorking were formatted successfully.

popd
exit /b 0
