setlocal enabledelayedexpansion
cd /d "%~dp0"

del /F /Q /S McAndroidTester\app\libs
del /F /Q /S McAndroidTester\app\src\main\jniLibs

mkdir McAndroidTester\app\libs
mkdir McAndroidTester\app\src\main\jniLibs\arm64-v8a

mklink McAndroidTester\app\libs\mapcore7-debug.aar %cd%\..\64-Java\mapcore7-debug.aar

:: Set the destination folder where you want the symbolic links to be created
set destinationDir=McAndroidTester\app\src\main\jniLibs\arm64-v8a

:: Set the source folder where the actual .so files are located
set sourceDir=%cd%\..\Cpp\ARMv8a-64-BinDebug

:: Loop through all .so files in the source directory
for %%f in (%sourceDir%\*.so) do (
    set fileName=%%~nxf
    mklink "%destinationDir%\!fileName!" "%%f"
)
