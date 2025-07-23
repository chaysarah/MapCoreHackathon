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
cd %MAPCORE_ROOT_PATH%

rem Building MapLayerServer...
echo tova %1
if "%1"=="RelWithDebInfo" (
	set "config_type=Release"
) else (
	set "config_type=%1"
)
call CreateVS-%config_type%.bat WEB_SERVER_BUILDER
"C:\Program Files\CMake\bin\cmake.exe"  --build VS%config_type% --target MapLayerServer --config %1

endlocal