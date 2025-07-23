
##### Sources
file(GLOB CS_FILES 
    "${CMAKE_CURRENT_SOURCE_DIR}/MapCore1.cs"
    "${CMAKE_CURRENT_SOURCE_DIR}/MapCore1.Designer.cs"
    "${CMAKE_CURRENT_SOURCE_DIR}/Program.cs"
    "${CMAKE_CURRENT_SOURCE_DIR}/Properties/AssemblyInfo.cs"
    "${CMAKE_CURRENT_SOURCE_DIR}/Properties/Resources.Designer.cs"
    "${CMAKE_CURRENT_SOURCE_DIR}/Properties/Settings.Designer.cs")

file(GLOB RESX_FILES 
		"MapCore1.resx"
		"Properties/Resources.resx")
	
#set the output folder to MapCoreWinFormExample/bin
set(BinOutputDir "${CMAKE_CURRENT_SOURCE_DIR}/bin/${CMAKE_SYSTEM_NAME}")
message(STATUS "MapCoreWinFormExample binaries output directory: ${BinOutputDir}")

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${BinOutputDir}")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${BinOutputDir}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${BinOutputDir}")

##### Add MapCoreWinFormExample executable
add_executable(${MapCoreWinFormExample_NAME} 
                            ${CS_FILES} 
							${RESX_FILES} )	

set_property(TARGET ${MapCoreWinFormExample_NAME} PROPERTY FOLDER "6-Testers")

set_property(TARGET ${MapCoreWinFormExample_NAME} PROPERTY WIN32_EXECUTABLE TRUE)
set_property(TARGET ${MapCoreWinFormExample_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.6")

if (MSVC)
    set(debug_type "full")
    if (CMAKE_BUILD_TYPE STREQUAL "Debug")
        set(debug_type "mixed")
    endif()

    set_target_properties(${MapCoreWinFormExample_NAME} PROPERTIES
        VS_GLOBAL_EnableUnmanagedDebugging "true"
        VS_GLOBAL_DebugType ${debug_type}
		VS_GLOBAL_RootNamespace ${MapCoreWinFormExample_NAME}
		VS_GLOBAL_AssemblyName ${MapCoreWinFormExample_NAME}
    )
endif()

csharp_set_windows_forms_properties(
   ${CS_FILES}
   ${RESX_FILES}
)

csharp_set_designer_cs_properties(
     "*.Designer.cs"
     "Properties/*.cs"
     "Properties/*.settings"
     "*.resx"
     "Properties/*.resx"
	 )


csharp_set_xaml_cs_properties(
     "*.xaml"
     "*.xaml.cs")


LIST(APPEND VS_DOTNET_REFERENCES "Microsoft.CSharp")
LIST(APPEND VS_DOTNET_REFERENCES "PresentationCore")
LIST(APPEND VS_DOTNET_REFERENCES "PresentationFramework")
LIST(APPEND VS_DOTNET_REFERENCES "System")
LIST(APPEND VS_DOTNET_REFERENCES "System.Core")
LIST(APPEND VS_DOTNET_REFERENCES "System.Data")
LIST(APPEND VS_DOTNET_REFERENCES "System.Design")
LIST(APPEND VS_DOTNET_REFERENCES "System.DirectoryServices")
LIST(APPEND VS_DOTNET_REFERENCES "System.drawing")
LIST(APPEND VS_DOTNET_REFERENCES "System.Runtime.Serialization")
LIST(APPEND VS_DOTNET_REFERENCES "System.ServiceProcess")
LIST(APPEND VS_DOTNET_REFERENCES "System.Windows.Forms")
LIST(APPEND VS_DOTNET_REFERENCES "System.Xaml")
LIST(APPEND VS_DOTNET_REFERENCES "System.Xml")
LIST(APPEND VS_DOTNET_REFERENCES "WindowsBase")
LIST(APPEND VS_DOTNET_REFERENCES "WindowsFormsIntegration")
	
set_property(TARGET ${MapCoreWinFormExample_NAME} PROPERTY
                    VS_DOTNET_REFERENCES  ${VS_DOTNET_REFERENCES})

target_link_libraries(${MapCoreWinFormExample_NAME} PRIVATE 
                      ${DNMapCoreTypes_NAME} 
                      ${DNMapCoreWrapper_NAME})


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
	
#####  Install
                     
install(TARGETS ${MapCoreWinFormExample_NAME}
        RUNTIME DESTINATION bin
        LIBRARY DESTINATION lib
        ARCHIVE DESTINATION lib)

install(FILES   ${MapCoreWinFormExample_HEADERS}
        DESTINATION include/${MapCoreWinFormExample_NAME})