export const validationMessages = {
    required: 'Required Field',
}

export const validationFunctions = {
    required: (value) => value && value != 0,
}

const validationTypes = {
    required: 'required',
}

export const fieldsValidation = {
    certitficateFileName: [validationTypes.required],
    privateKeyFileName: [validationTypes.required],
    dpKeyExchangeFileName: [validationTypes.required],
}

export const fieldsInfo = {
    // epsg:  'Enter EPSG code or coordinate system name.'
}