
##### Sources


file(GLOB MCMsoPluginTester_SOURCES 
    	Program.cs
    	DbConnectionParams.cs
    	Form1.cs
    	Form1.Designer.cs
    	Form1.resx
    	ShowCoords.cs
    	ShowCoords.Designer.cs
    	ShowCoords.resx
		App.xaml
	    App.xaml.cs
	    MainWindow.xaml
	    MainWindow.xaml.cs
        
    	Properties/AssemblyInfo.cs
    	Properties/Resources.Designer.cs
    	Properties/Resources.resx
    	Properties/Settings.Designer.cs
    	Properties/Settings.settings)



##### Add MCMsoPluginTester executable

add_executable(${MCMsoPluginTester_NAME} 
               ${MCMsoPluginTester_SOURCES} )	
		

set_target_properties(${MCMsoPluginTester_NAME} PROPERTIES VS_DOTNET_REFERENCES
                     "${DNMapCoreTypes_NAME};
                     ${DNMapCoreWrapper_NAME};
                     System;
                     System.Drawing;
                     System.Data;
                     System.Windows.Forms;
                     System.Xml;
                     UnmanagedWrapper;
                     MapCore")

target_link_libraries(${MCMsoPluginTester_NAME} PRIVATE 
                      ${DNMapCoreTypes_NAME} 
                      ${DNMapCoreWrapper_NAME})


#####  Install
                     
install(TARGETS ${MCMsoPluginTester_NAME}
        RUNTIME DESTINATION bin
        LIBRARY DESTINATION lib
        ARCHIVE DESTINATION lib)

install(FILES   ${MCMsoPluginTester_HEADERS}
        DESTINATION include/${MCMsoPluginTester_NAME})	


