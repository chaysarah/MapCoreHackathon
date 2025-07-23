project(web-tester)

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

if(MSVC)
    # Target to Build React App
    add_custom_target(web-tester ALL SOURCES ${REACT_FILES}
        COMMAND cmd /C "npm run build-vs"
        WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
        COMMENT "Building React app..."
        VERBATIM
    )

    # Find npm.cmd dynamically
    find_program(NPM_EXECUTABLE NAMES npm.cmd PATHS ENV PATH NO_DEFAULT_PATH)

    if(NPM_EXECUTABLE)
        message(STATUS "Found npm: ${NPM_EXECUTABLE}")
    else()
        message(FATAL_ERROR "npm.cmd not found! Ensure Node.js is installed and available in PATH.")
    endif()

    # add_executable(web-tester IMPORTED)
    set_target_properties(web-tester PROPERTIES
        VS_DEBUGGER_COMMAND ${NPM_EXECUTABLE}
        VS_DEBUGGER_COMMAND_ARGUMENTS "run start-vs"
        VS_DEBUGGER_WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
    )
else()
    add_custom_target(web-tester
        COMMAND  cmd /c "start \"\" npm run start-vs"
        WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
        COMMENT "🚀 Starting web-tester..."
        VERBATIM
    )
endif()

add_custom_target(web-tester-config
    COMMAND npm run wasm-config-vs -- ${MC_PLATFORM_AND_CONFIGURE}
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
    COMMENT "Starting web-tester configuration..."
    VERBATIM
)

set_target_properties(web-tester PROPERTIES EXCLUDE_FROM_ALL TRUE)
set_target_properties(web-tester-config PROPERTIES EXCLUDE_FROM_ALL TRUE)

set_target_properties(web-tester PROPERTIES FOLDER "7-Web")
set_target_properties(web-tester-config PROPERTIES FOLDER "7-Web")
