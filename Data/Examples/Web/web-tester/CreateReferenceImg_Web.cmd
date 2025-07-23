@echo off
setlocal enabledelayedexpansion
for /f %%i in (%1) do (
	del %userprofile%\Downloads\%%i.png 2>nul
	"C:\Program Files\Google\Chrome\Application\chrome.exe" "http://localhost:3001/?jsonFile=./MapCore_VTest/%%i/FullViewportDataParams_GLES_Master.json&printViewportPath=%%i.png"
	if NOT EXIST %userprofile%\Downloads\%%i.png (
		echo %%i has not saved
		goto End
	)
	move /Y %userprofile%\Downloads\%%i.png MapCore_VTest\%%i\Reference_GLES_Web.png
)
:End