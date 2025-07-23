


#add_definitions(-DMAPCORECPPEXAMPLE_EXPORTS)

		
##### Sources and Headers


file(GLOB MapCoreCppExample_HEADERS  
    "${CMAKE_CURRENT_SOURCE_DIR}/ChildView.h"
    "${CMAKE_CURRENT_SOURCE_DIR}/MainFrm.h"
    "${CMAKE_CURRENT_SOURCE_DIR}/MapCoreCppExample.h"
    "${CMAKE_CURRENT_SOURCE_DIR}/Resource.h"
    "${CMAKE_CURRENT_SOURCE_DIR}/stdafx.h"
    "${CMAKE_CURRENT_SOURCE_DIR}/targetver.h")	

file(GLOB MapCoreCppExample_SOURCES
    "${CMAKE_CURRENT_SOURCE_DIR}/ChildView.cpp"
    "${CMAKE_CURRENT_SOURCE_DIR}/MainFrm.cpp"
    "${CMAKE_CURRENT_SOURCE_DIR}/MapCoreCppExample.cpp"
    "${CMAKE_CURRENT_SOURCE_DIR}/stdafx.cpp")

file(GLOB MapCoreCppExample_RESOURCES 
    "${CMAKE_CURRENT_SOURCE_DIR}/MapCoreCppExample.rc"
    "${CMAKE_CURRENT_SOURCE_DIR}/res/MapCoreCppExample.rc2"
    "${CMAKE_CURRENT_SOURCE_DIR}/res/MapCoreCppExample.ico"
    "${CMAKE_CURRENT_SOURCE_DIR}/res/Toolbar.bmp"
    "${CMAKE_CURRENT_SOURCE_DIR}/res/Toolbar256.bmp")
	
set(PCH_HEADER "${CMAKE_CURRENT_SOURCE_DIR}/stdafx.h")
set(MapCoreCppExample_INCLUDES 
        "../Include"
		"../Include/Osm"
		"../Include/Calculations"
		)

#set the output folder to MapCoreCppExample/bin
set(BinOutputDir "${CMAKE_CURRENT_SOURCE_DIR}/bin/${MC_PLATFORM_AND_CONFIGURE}")
message(STATUS "MapCoreCppExample binaries output directory: ${BinOutputDir}")

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${BinOutputDir}")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${BinOutputDir}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${BinOutputDir}")

##### Add MapCoreCppExample library
add_executable(${MapCoreCppExample_NAME} 
             ${MapCoreCppExample_SOURCES} 
             ${MapCoreCppExample_HEADERS}
             ${MapCoreCppExample_RESOURCES} )	
set_property(TARGET ${MapCoreCppExample_NAME} PROPERTY FOLDER "6-Testers")

target_precompile_headers(${MapCoreCppExample_NAME} PRIVATE ${PCH_HEADER})		

#set working directory to root of project
set_property(TARGET ${MapCoreCppExample_NAME} PROPERTY VS_DEBUGGER_WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}")

source_group("Resources" FILES ${MapCoreCppExample_RESOURCES})
target_include_directories(${MapCoreCppExample_NAME}  PRIVATE 
                          ${MapCoreCppExample_INCLUDES})
						  
target_link_libraries(${MapCoreCppExample_NAME} PRIVATE 
                     ${SceneManager_NAME}
                     ${MapTerrain_NAME}
                     ${CommonUtils_NAME}
                     ${GridCoordinateSystems_NAME}
                     ${OverlayManager_NAME}
                     ${EditMode_NAME})

if(MSVC)
    add_compile_options(
        /W3                     # Equivalent to WarningLevel 3
        /fp:fast                # Floating Point Model: Fast
        /permissive-
    )
	
# Linker settings
    set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} /SUBSYSTEM:WINDOWS")
endif()

if(CMAKE_BUILD_TYPE STREQUAL "Debug")
	target_link_options(${MapCoreCppExample_NAME} PRIVATE /INCREMENTAL)
endif()

add_compile_definitions(_MBCS)
set(CMAKE_MFC_FLAG 2)

# Copy .cfg files to output directory
set(CONFIG_FILES
    ${CMAKE_CURRENT_SOURCE_DIR}/fxplugin.cfg
    ${CMAKE_CURRENT_SOURCE_DIR}/plugins.cfg
)

# Ensure the output directory exists
set(OUTPUT_DIR "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/${CMAKE_BUILD_TYPE}")
file(MAKE_DIRECTORY ${OUTPUT_DIR})

file(COPY ${CONFIG_FILES} DESTINATION ${OUTPUT_DIR})

# replace relative paths to absolute paths
file(READ ${CMAKE_CURRENT_SOURCE_DIR}/resources.cfg RSC_CONTENT)
string(REPLACE "../../" "${CMAKE_SOURCE_DIR}/" RSC_CONTENT "${RSC_CONTENT}")
file(WRITE ${OUTPUT_DIR}/resources.cfg ${RSC_CONTENT})

##### Install
install(TARGETS ${MapCoreCppExample_NAME}
        RUNTIME DESTINATION bin
        LIBRARY DESTINATION lib
        ARCHIVE DESTINATION lib)


install(FILES  ${MapCoreCppExample_HEADERS} 
        DESTINATION "include/${MapCoreCppExample_NAME}")
