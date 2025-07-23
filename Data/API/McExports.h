#pragma once

#ifndef OVERLAYMANAGER_API
	#ifndef _WIN32 
		#ifdef OVERLAYMANAGER_EXPORTS
			#define OVERLAYMANAGER_API  __attribute__ ((visibility("default")))
		#else
			#define OVERLAYMANAGER_API
		#endif
	#else
	   #ifdef OVERLAYMANAGER_EXPORTS
			#define OVERLAYMANAGER_API __declspec(dllexport)
		#else
			#define OVERLAYMANAGER_API __declspec(dllimport)
		#endif
	#endif
#endif


#ifndef SCENEMANAGER_API
	#ifndef _WIN32 
		#ifdef SCENEMANAGER_EXPORTS
			#define SCENEMANAGER_API  __attribute__ ((visibility("default")))
		#else
			#define SCENEMANAGER_API
		#endif
	#else
		#ifdef SCENEMANAGER_EXPORTS
			#define SCENEMANAGER_API __declspec(dllexport)
		#else
			#define SCENEMANAGER_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef MAPLAYER_API
	#ifndef _WIN32 
		#ifdef MAPTERRAIN_EXPORTS
			#define MAPLAYER_API  __attribute__ ((visibility("default")))
		#else
			#define MAPLAYER_API
		#endif
	#else
	   #ifdef MAPTERRAIN_EXPORTS
			#define MAPLAYER_API __declspec(dllexport)
		#else
			#define MAPLAYER_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef MAPTERRAIN_API
	#ifndef _WIN32 
		#ifdef MAPTERRAIN_EXPORTS
			#define MAPTERRAIN_API  __attribute__ ((visibility("default")))
		#else
			#define MAPTERRAIN_API
		#endif
	#else
	   #ifdef MAPTERRAIN_EXPORTS
			#define MAPTERRAIN_API __declspec(dllexport)
		#else
			#define MAPTERRAIN_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef COMMONUTILS_API
	#ifndef _WIN32 
		#ifdef COMMONUTILS_EXPORTS
			#define COMMONUTILS_API  __attribute__ ((visibility("default")))
		#else
			#define COMMONUTILS_API
		#endif
	#else
		#ifdef COMMONUTILS_EXPORTS
			#define COMMONUTILS_API __declspec(dllexport)
		#else
			#define COMMONUTILS_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef VIDEOUTILS_API
	#ifndef _WIN32 
		#ifdef VIDEOUTILS_EXPORTS
			#define VIDEOUTILS_API  __attribute__ ((visibility("default")))
		#else
			#define VIDEOUTILS_API
		#endif
	#else
		#ifdef VIDEOUTILS_EXPORTS
			#define VIDEOUTILS_API __declspec(dllexport)
		#else
			#define VIDEOUTILS_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef GRIDCOORDINATESYSTEMS_API
	#ifndef _WIN32 
		#ifdef GRIDCOORDINATESYSTEMS_EXPORTS
			#define GRIDCOORDINATESYSTEMS_API  __attribute__ ((visibility("default")))
		#else
			#define GRIDCOORDINATESYSTEMS_API
		#endif
	#else
		#ifdef GRIDCOORDINATESYSTEMS_EXPORTS
			#define GRIDCOORDINATESYSTEMS_API __declspec(dllexport)
		#else
			#define GRIDCOORDINATESYSTEMS_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef MAPPRODUCTION_API
	#ifndef _WIN32 
		#ifdef MAPPRODUCTION_EXPORTS
			#define MAPPRODUCTION_API  __attribute__ ((visibility("default")))
		#else
			#define MAPPRODUCTION_API
		#endif
	#else
		#ifdef MAPPRODUCTION_EXPORTS
			#define MAPPRODUCTION_API __declspec(dllexport)
		#else
			#define MAPPRODUCTION_API __declspec(dllimport)
		#endif
	#endif
#endif


#ifndef GEOGRAPHICCALCULATIONS_API
	#ifndef _WIN32 
		#ifdef GEOGRAPHICCALCULATIONS_EXPORTS
			#define GEOGRAPHICCALCULATIONS_API  __attribute__ ((visibility("default")))
		#else
			#define GEOGRAPHICCALCULATIONS_API
		#endif
	#else
		#ifdef GEOGRAPHICCALCULATIONS_EXPORTS
			#define GEOGRAPHICCALCULATIONS_API __declspec(dllexport)
		#else
			#define GEOGRAPHICCALCULATIONS_API __declspec(dllimport)
		#endif
	#endif
#endif


#ifndef GEOMETRICCALCULATIONS_API
	#ifndef _WIN32 
		#ifdef GEOMETRICCALCULATIONS_EXPORTS
			#define GEOMETRICCALCULATIONS_API  __attribute__ ((visibility("default")))
		#else
			#define GEOMETRICCALCULATIONS_API
		#endif
	#else
		#ifdef GEOMETRICCALCULATIONS_EXPORTS
			#define GEOMETRICCALCULATIONS_API __declspec(dllexport)
		#else
			#define GEOMETRICCALCULATIONS_API __declspec(dllimport)
		#endif
	#endif
#endif


#ifndef PHOTOGRAMMETRICCALC_API
	#ifndef _WIN32 
		#ifdef PHOTOGRAMMETRICCALC_EXPORTS
			#define PHOTOGRAMMETRICCALC_API  __attribute__ ((visibility("default")))
		#else
			#define PHOTOGRAMMETRICCALC_API
		#endif
	#else
		#ifdef PHOTOGRAMMETRICCALC_EXPORTS
			#define PHOTOGRAMMETRICCALC_API __declspec(dllexport)
		#else
			#define PHOTOGRAMMETRICCALC_API __declspec(dllimport)
		#endif
	#endif
#endif


#ifndef IPPWRAPPER_API
	#ifndef _WIN32 
		#ifdef IPPWRAPPER_EXPORTS
			#define IPPWRAPPER_API  __attribute__ ((visibility("default")))
		#else
			#define IPPWRAPPER_API
		#endif
	#else
		#ifdef IPPWRAPPER_EXPORTS
			#define IPPWRAPPER_API __declspec(dllexport)
		#else
			#define IPPWRAPPER_API __declspec(dllimport)
		#endif
	#endif
#endif


#ifndef FILEPRODUCTIONS_API
	#ifndef _WIN32 
		#ifdef FILEPRODUCTIONS_EXPORTS
			#define FILEPRODUCTIONS_API  __attribute__ ((visibility("default")))
		#else
			#define FILEPRODUCTIONS_API
		#endif
	#else
		#ifdef FILEPRODUCTIONS_EXPORTS
			#define FILEPRODUCTIONS_API __declspec(dllexport)
		#else
			#define FILEPRODUCTIONS_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef IMAGES_CORRELATOR_API
	#ifndef _WIN32 
		#ifdef IMAGES_CORRELATOR_EXPORTS
			#define IMAGES_CORRELATOR_API  __attribute__ ((visibility("default")))
		#else
			#define IMAGES_CORRELATOR_API
		#endif
	#else
		#ifdef IMAGES_CORRELATOR_EXPORTS
			#define IMAGES_CORRELATOR_API __declspec(dllexport)
		#else
			#define IMAGES_CORRELATOR_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef MC_OSM_API
	#ifndef _WIN32 
		#ifdef MC_OSM_PLUGIN_EXPORTS
			#define MC_OSM_PLUGIN_API  __attribute__ ((visibility("default")))
		#else
			#define MC_OSM_PLUGIN_API
		#endif
	#else
		#ifdef MC_OSM_PLUGIN_EXPORTS
			#define MC_OSM_PLUGIN_API __declspec(dllexport)
		#else
			#define MC_OSM_PLUGIN_API __declspec(dllimport)
		#endif
	#endif
#endif

#ifndef LICENSEMGR_API
	#ifndef _WIN32
		#define LICENSEMGR_API
	#else
		#ifdef LICENSEMGR_EXPORTS
			#define LICENSEMGR_API __declspec(dllexport)
		#else
			#define LICENSEMGR_API __declspec(dllimport)
		#endif
	#endif
#endif