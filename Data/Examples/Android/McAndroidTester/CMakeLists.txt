project("McAndroidTester")

# Set the output directory for the AAR
set(APK_OUTPUT_DIR "${CMAKE_CURRENT_SOURCE_DIR}/app/build/outputs/apk")

# Gradle build command
set(GRADLE_BUILD_CMD "${CMAKE_CURRENT_SOURCE_DIR}/gradlew.bat" --offline assembleRelease)
# Output file
set(OUTPUT ${APK_OUTPUT_DIR}/release/app-release.apk)

if(CMAKE_BUILD_TYPE STREQUAL "Debug")
  set(GRADLE_BUILD_CMD "${CMAKE_CURRENT_SOURCE_DIR}/gradlew.bat" --offline assembleDebug)
  set(OUTPUT ${APK_OUTPUT_DIR}/debug/app-debug.apk)
endif()

# Custom command to build the APK
add_custom_command(
    OUTPUT ${OUTPUT}
    COMMAND ${GRADLE_BUILD_CMD}
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
    COMMENT "Building APK with Gradle"
)

# Custom target that depends on the APK
add_custom_target(
    McAndroidTester ALL
    DEPENDS ${OUTPUT}
)



# Function to collect all targets
function(get_all_targets _result _dir)
    get_property(_subdirs DIRECTORY "${_dir}" PROPERTY SUBDIRECTORIES)
    foreach(_subdir IN LISTS _subdirs)
        get_all_targets(${_result} "${_subdir}")
    endforeach()
    get_property(_sub_targets DIRECTORY "${_dir}" PROPERTY BUILDSYSTEM_TARGETS)
    set(${_result} ${${_result}} ${_sub_targets} PARENT_SCOPE)
endfunction()

# Collect all targets
set(PROJECT_TARGETS)
get_all_targets(PROJECT_TARGETS ${CMAKE_SOURCE_DIR})

# Add all targets as dependencies to buildAAR
foreach(_target IN LISTS PROJECT_TARGETS)
    if( NOT ${_target} STREQUAL "McAndroidTester")
          add_dependencies(McAndroidTester ${_target})
    endif()
endforeach()


# Installation step to copy the apk to the install directory
install(FILES ${OUTPUT}
        DESTINATION lib)


