namespace NavCore.Net
{
    public enum ECode
    {
        /// General Errors:
        SUCCESS = 1,                                // No Errors.
        FAILURE,                                    // Unspecified error.
        INVALID_ARGUMENT,                           // An invalid argument is passed to the method.
        OUT_OF_RANGE_PARAMETER,                     // Input parameter has value which is out of pre-defined range.
        INPUT_FILE_NOT_FOUND,                       // Input file not found.
        INPUT_DIR_NOT_FOUND,                        // Input directory not found.

        /// Init Errors:
        TRAV_LAYER_METADATA_FILE_NOT_EXIST = 101,   // TravLayer metadata file does not exist.
        TRAV_LAYER_METADATA_FILE_UNKNOWN_FORMAT,    // TravLayer metadata file is corrupted or does not contain all required data.

        /// Load Errors:
        ROI_AREA_IS_TOO_BIG = 201,                  // Specified area is too big.

        /// FindPath Errors:
        FIND_PATH_NOT_READY = 301,                  // FindPath is called but traversability data has not been loaded yet.
        INVALID_INPUT_PATH                          // Input path is empty.
    };

    public enum ELoadStatus
    {
        ELS_NOT_LOADED,
        ELS_LOADED_TRAV_DATA_FOUND_IN_ROI,
        ELS_LOADED_TRAV_DATA_NOT_FOUND_IN_ROI
    };

    public enum EFindPathStatus
    {
        EFPS_UNKNOWN,
        EFPS_FULL_PATH_FOUND,
        EFPS_PARTIAL_PATH_FOUND,
        EFPS_PATH_FOUND_FOR_SINGLE_POINT_INPUT,
        EFPS_PATH_NOT_FOUND
    };

    public enum EShortPathStatus
    {
        ESPS_UNKNOWN,
        ESPS_FREE,
        ESPS_PARTIALLY_BLOCKED,
        ESPS_BLOCKED
    };

    public enum ENavCoreRtMode
    {
        ENCRM_BASIC,
        ENCRM_OBSTACLE_DETECTION,
        ENCRM_OBSTACLE_AVOIDANCE,
    };

    public enum EControlStatus
    {
        ECS_NO_MESSAGE,
        ECS_PREPARE_TO_PIVOT,
        ECS_ON_PIVOT
    };

    public enum EControlCommands
    {
        ECC_UNKNOWN,
        ECC_START,
        ECC_CONTINUE,
        ECC_SLOW,
        ECC_FAST,
        ECC_STOP,
        ECC_EMERGENCY_STOP,
        ECC_CONTINUE_STOP,
    };

    public enum ENavCoreRtStatus
    {
        ENCRS_UNKNOWN,
        ENCRS_READY_TO_GET_PATH,
        ENCRS_STOP_CURRENT_PATH,
        ENCRS_STOP_READY_TO_RIDE_CURRENT_PATH,
        ENCRS_PAUSE_CURRENT_PATH_TRY_TO_RECOVER,
        ENCRS_PAUSE_CURRENT_PATH_CANT_RECOVER,
        ENCRS_RIDE_CURRENT_PATH,
        ENCRS_COMPLETED_CURRENT_PATH,
        ENCRS_CURRENTLY_DISABLED_FOR_MANUAL_CONTROL,
    };
}
