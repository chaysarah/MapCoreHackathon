project(web-library)

# Find all React Source Files
file(GLOB_RECURSE REACT_FILES
    "*.js"
    "*.jsx"
    "*.ts"
    "*.tsx"
    "*.html"
    "*.css"
    "*.json"
)

# Group React Files Inside Solution Explorer
source_group(TREE ${CMAKE_CURRENT_SOURCE_DIR} PREFIX "React Source Files" FILES ${REACT_FILES})

if(WIN32)
	set(SHELL_CMD "cmd" "/C")
    set(ADMIN_PREFIX )
else()
	set(SHELL_CMD "sh" "-c")
    set(ADMIN_PREFIX "sudo")
endif()

execute_process(
    COMMAND npm root -g
    OUTPUT_VARIABLE NODE_MODULES_GLOBAL
    OUTPUT_STRIP_TRAILING_WHITESPACE
)

message(STATUS "Global node_modules: ${NODE_MODULES_GLOBAL}")

if(NOT EXISTS ${NODE_MODULES_GLOBAL}/mapcore-lib)
	execute_process(
		COMMAND ${SHELL_CMD} "${ADMIN_PREFIX} npm link"
		WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
	)
endif()
	
# Target to Build React App
add_custom_target(web-library ALL SOURCES ${REACT_FILES}
    COMMAND ${SHELL_CMD} "npm run build-vs"
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
    COMMENT "Building web-library..."
    VERBATIM
)

set_target_properties(web-library PROPERTIES EXCLUDE_FROM_ALL TRUE)

set_target_properties(web-library PROPERTIES FOLDER "7-Web")
