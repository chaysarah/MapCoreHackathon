
##### Sources


file(GLOB WPFMapCoreExample_SOURCES 
   	App.xaml
	App.xaml.cs
	MainWindow.xaml
	MainWindow.xaml.cs

	Properties/AssemblyInfo.cs
	Properties/Resources.Designer.cs
	Properties/Resources.resx
	Properties/Settings.Designer.cs
	Properties/Settings.settings)

	
##### Add WPFMapCoreExample library
#set the output folder to MapCoreCppExample/bin
set(BinOutputDir "${CMAKE_CURRENT_SOURCE_DIR}/bin/${CMAKE_SYSTEM_NAME}")
message(STATUS "WPFMapCoreExample binaries output directory: ${BinOutputDir}")

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${BinOutputDir}")
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${BinOutputDir}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${BinOutputDir}")

##### Add WPFMapCoreExample_NAME executable
add_executable(${WPFMapCoreExample_NAME} 
                            ${WPFMapCoreExample_SOURCES} )	

set_property(TARGET ${WPFMapCoreExample_NAME} PROPERTY FOLDER "6-Testers")

#set_property(TARGET ${WPFMapCoreExample_NAME} PROPERTY WIN32_EXECUTABLE TRUE)
#set_property(TARGET ${WPFMapCoreExample_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.6")

if (MSVC)
    set(debug_type "full")
    if (CMAKE_BUILD_TYPE STREQUAL "Debug")
        set(debug_type "mixed")
    endif()

    set_target_properties(${WPFMapCoreExample_NAME} PROPERTIES
        VS_GLOBAL_EnableUnmanagedDebugging "true"
        VS_GLOBAL_DebugType ${debug_type}
		VS_GLOBAL_RootNamespace ${WPFMapCoreExample_NAME}
		VS_GLOBAL_AssemblyName ${WPFMapCoreExample_NAME}
    )
endif()

set_property(TARGET ${WPFMapCoreExample_NAME} PROPERTY VS_GLOBAL_UseWPF "true")

# Enable WPF support
set_property(TARGET ${WPFMapCoreExample_NAME} PROPERTY VS_DOTNET_REFERENCES
    "PresentationCore"
    "PresentationFramework"
    "WindowsBase"
    "System.Xaml"
)

# Ensure CMake generates WPF-specific .g.cs files
set_target_properties(${WPFMapCoreExample_NAME} PROPERTIES
    VS_GLOBAL_ProduceReferenceAssembly "True"
    VS_GLOBAL_UseWPF "True"
    VS_DOTNET_TARGET_FRAMEWORK "net6.0-windows"  # Adjust as needed
)

# Add XAML processing (this is necessary for WPF)
set_source_files_properties(
    App.xaml #MainWindow.xaml 
    PROPERTIES VS_XAML_TYPE ApplicationDefinition
)

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
	
set_property(TARGET ${WPFMapCoreExample_NAME} PROPERTY
                    VS_DOTNET_REFERENCES  ${VS_DOTNET_REFERENCES})

target_link_libraries(${WPFMapCoreExample_NAME} PRIVATE 
                      ${DNMapCoreTypes_NAME} 
                      ${DNMapCoreWrapper_NAME})
					  
csharp_set_designer_cs_properties(
	"Properties/AssemblyInfo.cs"
	"Properties/Resources.resx"
	"Properties/Settings.settings")

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
                     
install(TARGETS ${WPFMapCoreExample_NAME}
        RUNTIME DESTINATION bin
        LIBRARY DESTINATION lib
        ARCHIVE DESTINATION lib)

install(FILES   ${WPFMapCoreExample_HEADERS}
        DESTINATION include/${WPFMapCoreExample_NAME})




#csharp_set_designer_cs_properties(
#	Properties/AssemblyInfo.cs
#	Properties/Resources.Designer.cs
#	Properties/Resources.resx
#	Properties/Settings.Designer.cs
#	Properties/Settings.settings)
#
#csharp_set_xaml_cs_properties(
#	App.xaml
#	App.xaml.cs
#	MainWindow.xaml
#	MainWindow.xaml.cs)
#
#set_property(SOURCE App.xaml PROPERTY VS_XAML_TYPE "ApplicationDefinition")
#
#set_property(TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.6.1")
#set_property(TARGET ${PROJECT_NAME} PROPERTY WIN32_EXECUTABLE TRUE)
#
#set_property(TARGET ${PROJECT_NAME} PROPERTY VS_DOTNET_REFERENCES
#	"Microsoft.CSharp"
#	"PresentationCore"
#	"PresentationFramework"
#	"System"
#	"System.Core"
#	"System.Data"
#	"System.Data.DataSetExtensions"
#	"System.Net.Http"
#	"System.Xaml"
#	"System.Xml"
#	"System.Xml.Linq"
#	"WindowsBase"
#	"System.Text"
#    "System.Linq"
#    "System.Collections.Generic"
#    "System.Windows"
#    "System.Windows.Controls"
#    #"System.Windows.Data"
#    "System.Windows.Documents"
#    "System.Windows.Input"
#    "System.Windows.Media"
#    "System.Windows.Media.Imaging"
#    "System.Windows.Navigation"
#    "System.Windows.Shapes"
#    "System.Windows.Interop"
#	"UnmanagedWrapper")
#   		



