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
    epsg: [validationTypes.required],
}

export const fieldsInfo = {
    epsg: 'Enter EPSG code or coordinate system name.',
}