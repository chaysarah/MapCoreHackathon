setlocal

rem Finding MapCore root path...
set "CURR_PATH=%CD%"
set "MAPCORE_ROOT_PATH="

for /f "delims=" %%A in ("%CURR_PATH%") do set "PATH_REMAINING=%%~dpA"

:loop
for /f "tokens=1* delims=\" %%A in ("%PATH_REMAINING%") do (
    if /I "%%A"=="mapcore" (
        set "MAPCORE_ROOT_PATH=%MAPCORE_ROOT_PATH%\%%A"
        goto :found
    )
    set "MAPCORE_ROOT_PATH=%MAPCORE_ROOT_PATH%\%%A"
    set "PATH_REMAINING=%%B"
    if "%PATH_REMAINING%"=="" goto :notfound
    goto :loop
)

:notfound
echo "mapcore" directory not found in path!
exit /b 1

:found
:: Remove leading backslash if present
set "MAPCORE_ROOT_PATH=%MAPCORE_ROOT_PATH:~1%"

rem Running MapLayerServer...
cd %MAPCORE_ROOT_PATH%\MapCore\Tools\MapLayerServer
set PATH=%PATH%;C:\git\mapcore\bin\Windows\%1
bin\Windows\%1\McMapLayerServer.exe 0.0.0.0 6767 5 ./WWWRoot . C:/ServerData

endlocal