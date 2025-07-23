del /F /Q /S McAndroidTester\app\libs
del /F /Q /S McAndroidTester\app\src\main\jniLibs

mkdir McAndroidTester\app\libs

mklink McAndroidTester\app\libs\mapcore7-release.aar %cd%\..\64-Java\mapcore7-release.aar
