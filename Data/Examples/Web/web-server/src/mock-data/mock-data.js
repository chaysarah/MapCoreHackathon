export const mapLayersArr = [{
    title: "Israel",
    childNodes: [
      {title: "Israel North 123456789"},
      {title: "suzie", childNodes: [
        {title: "puppy", childNodes: [
          {title: "dog house"}
        ]},
        {title: "cherry tree"}
      ]}
    ]
  },
  {
    title: 'South Afganistan',
    childNodes: [
      {title: 'South Afganistan Raster 100000034'},
      {title: 'South Afganistan Raster 100000034'},
      {title: 'South Afganistan Vector 100000034'},
      {title: 'South Afganistan Vector 100000034'},
      {title: 'South Afganistan DTM 100000034'},
    ]          
  },
  {
    title: 'South Korea',
    childNodes: [
      {title: 'South Korea Raster 100000034'},
      {title: 'South Korea Raster 100000034'},
      {title: 'South Korea Vector 100000034'},
      {title: 'South Korea Vector 100000034'},
      {title: 'South Korea DTM 100000034'},
    ]          
  },
  {
    title: 'North USA',
    childNodes: [
      {title: 'South USA Raster 100000034'},
      {title: 'South USA Raster 100000034'},
      {title: 'South USA Vector 100000034'},
      {title: 'South USA Vector 100000034'},
      {title: 'South USA DTM 100000034'},
    ]          
  }
]

export const mapLayersArr1 = [
  {
    title: 'South Afganistan',
    childNodes: [
      {title: 'South Afganistan Raster 100000034'},
      {title: 'South Afganistan Raster 100000034'},
      {title: 'South Afganistan Vector 100000034'},
      {title: 'South Afganistan Vector 100000034'},
      {title: 'South Afganistan DTM 100000034'},
    ]          
  }];

  export const layersInfoResponse = {
    "SuffixUrl": "/map/opr",
    "MemoryCacheSize": 512,
    "DiskCacheSize": 2048,
    "DiskCacheFolder": "C:\\Maps\\Swiss\\Cache",
    "IntermediateFilesFolder": ".",
    "GpuUsageInSpatialQueries": false,
    "SSLUsage": true,
    "SSLInfo": {
        "CertificateFileName": "C:\\Temp\\Cert.pem",
        "PrivateKeyFileName": "C:\\Temp\\Key.pem",
        "DpKeyExchangeFileName": "C:\\Temp\\dh.pem"
    },
    "MapLayerConfig": [
        {
            "LayerId": "Kerl-100K",
            "Title": "Kerl Topographic map - 1:100K",
            "LayerType": "RAW_RASTER",
            "LayerUUID": "143f836a-40d6-406d-a2bd-e689b6cca0c8",
            "Group": "Swiss",
            "RawLayerInfo": {
                "CoordinateSystem": {
                    "SRIDType": "epsg",
                    "Code": 4326
                },
                "Raster": {
                    "BaseLocationFolder": "C:\\Maps\\Swiss\\Raster\\Kerl\\",
                    "Location": [
                        {
                            "LocationType": "FILE",
                            "Path": "Kerl-100K.tif"
                        }
                    ]
                }
            }
        },
        {
            "LayerId": "Swiss-DTM",
            "Title": "DTED-1 dtm of Kerl and Emmen area",
            "LayerType": "RAW_DTM",
            "LayerUUID": "6ca908fa-7173-446d-8a7c-8e54d45056f7",
            "Group": "Swiss",
            "RawLayerInfo": {
                "CoordinateSystem": {
                    "SRIDType": "epsg",
                    "Code": 4326
                },
                "Dtm": {
                    "Location": [
                        {
                            "LocationType": "FOLDER",
                            "Path": "C:\\Maps\\Swiss\\DTM\\"
                        }
                    ]
                }
            }
        },
        {
            "LayerId": "Swiss-Lakes",
            "Title": "Vector layer of swiss lake area",
            "LayerType": "RAW_VECTOR",
            "LayerUUID": "7ed4dc52-0fa8-476b-ba05-269d2a3b0130",
            "Group": "Swiss",
            "RawLayerInfo": {
                "CoordinateSystem": {
                    "SRIDType": "epsg",
                   "Code": 4326
                },
                "Vector": {
                    "DataSource": "C:\\Maps\\Swiss\\Vector\\Lakes\\VectorRules.xml",
                    "SourceCoordinateSystem": {
                        "SRIDType": "epsg",
                        "Code": 4326
                    }
                }
            }
        },
        {
            "LayerId": "FortHOrtho1",
            "Title": "Orthophoto of FortHaucuca1",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "US-Nevada",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho2",
            "Title": "Orthophoto of FortHaucuca2",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "US-Nevada",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho3",
            "Title": "Orthophoto of FortHaucuca3",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "US-Nevada",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho4",
            "Title": "Orthophoto of FortHaucuca4",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "US-Nevada",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho5",
            "Title": "Orthophoto of FortHaucuca5",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "US-Nevada",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho6",
            "Title": "Orthophoto of FortHaucuca6",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "Greenland",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho7",
            "Title": "Orthophoto of FortHaucuca7",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "Greenland",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho8",
            "Title": "Orthophoto of FortHaucuca8",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "Greenland",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho9",
            "Title": "Orthophoto of FortHaucuca9",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "Israel",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho10",
            "Title": "Orthophoto of FortHaucuca10",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "Israel",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
        {
            "LayerId": "FortHOrtho11",
            "Title": "Orthophoto of FortHaucuca11",
            "LayerType": "NATIVE_RASTER",
            "LayerUUID": "782bd0a7-c244-4571-9008-b58a2bb1e0dd",
            "Group": "Israel",
            "NativeLayerPath": "C:\\Maps\\US\\Fort-Huachuca\\Raster\\ortho_Fort huachuca"
        },
    ]
};