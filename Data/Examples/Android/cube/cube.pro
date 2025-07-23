QT       += core gui widgets opengl androidextras gui-private

ANDROID_PACKAGE_SOURCE_DIR = $$PWD/android-sources

TARGET = cube
TEMPLATE = app

SOURCES += main.cpp openglscene.cpp graphicsview.cpp \
    layersdialog.cpp \
    fps.cpp
HEADERS += openglscene.h graphicsview.h \
    layersdialog.h \
    layerinfo.h \
    fps.h \
    common.h

OTHER_FILES += \
    android-sources/src/org/qtproject/example/cube/CubeActivity.java \
    android-sources/AndroidManifest.xml

schemes.path = /assets/Schemes

schemes.files += Schemes/OnePicScmWorld.m
schemes.files += Schemes/PerpendicularLines.m
schemes.files += Schemes/PointIconCrossThreeMod.m
schemes.files += Schemes/PositionPerpendicularToArrow.m
schemes.files += Schemes/ScreenPictureLegScheme.m
schemes.files += Schemes/SightPresentation.m
schemes.files += Schemes/Symbol.m
schemes.files += Schemes/OnePicScmWorld.m
schemes.files += Schemes/ScreenPictureLegScheme2.m
schemes.files += Schemes/GroundUnit.FRIEND_A.bmp
schemes.files += Schemes/179.png
schemes.files += Schemes/169.png
schemes.files += Schemes/88.bmp
schemes.files += Schemes/189.bmp
schemes.files += Schemes/179.bmp
schemes.files += Schemes/FullRed.png
schemes.files += Schemes/Doron0.m

schemes.depends += FORCE

INSTALLS += schemes


INCLUDEPATH += C:/Prj/MapCore7/Dev/MapCore/Include C:\Prj\MapCore7\Dev\Development\Basis\Calculations\OldCalculations\Include

android:LIBS += -LC:\Prj\MapCore7\Dev\MapCore\Bin\Android-armv7-a\Debug
android:LIBS +=-lMcEditMode7D -lMcOverlayManager7D -lMcSceneManager7D -lMcMapTerrain7D -lMcCommonUtils7D -lMcGeographicCalculations7D -lMcMapUtils7D -lMcCommonCalc7D -lMcGeometricCalculations7D -lMcGridCoordinateSystems7D -lOgreOverlay_d -lOgreRTShaderSystem_d -lRenderSystem_GLES2_d -lOgreMain_d -lfreetype_d -lgdal_d -lgluesD -lEGL -lGLESv2 -landroid -llog -lcpufeatures_d -llibjasper_d -lsqlite3_d -lNCSEcwAndroid -lzziplib_d -lFreeImage_d -lboost_thread-gcc-mt-1_53 -lboost_date_time-gcc-mt-1_53 -lboost_system-gcc-mt-1_53 -lboost_atomic-gcc-mt-1_53 -lTriangleD -lfribidiD -lProj4D
ANDROID_EXTRA_LIBS = C:\Prj\MapCore7\Dev\MapCore\Bin\Android-armv7-a\Debug\libFreeImage_d.so

#android:LIBS += -LC:\Prj\MapCore7\Dev\MapCore\Bin\Android-armv7-a\Release
#android:LIBS +=-lMcEditMode7 -lMcOverlayManager7 -lMcSceneManager7 -lMcMapTerrain7 -lMcCommonUtils7 -lMcGeographicCalculations7 -lMcMapUtils7 -lMcCommonCalc7 -lMcGeometricCalculations7 -lMcGridCoordinateSystems7 -lOgreOverlay -lOgreRTShaderSystem -lRenderSystem_GLES2 -lOgreMain -lfreetype -lgdal -lglues -lEGL -lGLESv2 -landroid -llog -lcpufeatures -llibjasper -lsqlite3 -lNCSEcwAndroid -lzziplib -lFreeImage -lboost_thread-gcc-mt-1_53 -lboost_date_time-gcc-mt-1_53 -lboost_system-gcc-mt-1_53 -lboost_atomic-gcc-mt-1_53 -lTriangle -lfribidi -lProj4
#ANDROID_EXTRA_LIBS = C:\Prj\MapCore7\Dev\MapCore\Bin\Android-armv7-a\Release\libFreeImage.so



