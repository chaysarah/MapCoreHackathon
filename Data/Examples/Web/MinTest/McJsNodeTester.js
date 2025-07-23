const McStartMapCore = require('./MapCore_Calculations.js');

McStartMapCore().then(() =>
{
	var gGeo = MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84);
	var gUTM = MapCore.IMcGridUTM.Create(36, MapCore.IMcGridCoordinateSystem.EDatumType.EDT_ED50_ISRAEL);
	var conv = MapCore.IMcGridConverter.Create(gGeo, gUTM);
	var p1 = conv.ConvertAtoB(new MapCore.SMcVector3D(3483274.003235352, 3221398.702113517, 0));
	console.log(p1);
	var geoCalc = MapCore.IMcGeographicCalculations.Create(gGeo);
	var p2 = geoCalc.LocationFromAzimuthAndDistance(new MapCore.SMcVector3D(3483274.003235352, 3221398.702113517, 0), 45, 10);
	console.log(p2);
	// uncomment the following line if MapCore.js is used instead of MapCore_Calculations.js
	// MapCore.IMcMapDevice.MapNodeJsDirectory("NodeJsFiles", "FS");
	var mag = geoCalc.CalcMagneticElements(new MapCore.SMcVector3D(3483274.003235352, 3221398.702113517, 0), new Date());
	var aLine = MapCore.IMcGeometricCalculations.EGParallelLine([new MapCore.SMcVector3D(3483274.003235352, 3221398.702113517, 0), new MapCore.SMcVector3D(3483284.003235352, 3221398.702113517, 0)], 10);
	console.log(mag);
});