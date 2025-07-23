#pragma once

//===========================================================================
/// \file IMcMapEnvironment.h
/// Interface for map environment
//===========================================================================
#include "IMcErrors.h"
#include "IMcBase.h"
#include "McCommonTypes.h"
#include "CMcTime.h"

class IMcMapViewport;

//=========================================================================================
// Interface Name: IMcMapEnvironment
//-----------------------------------------------------------------------------------------
/// Interface for environment consisting of several environment components
//=========================================================================================
class IMcMapEnvironment : virtual public IMcBase
{
protected:
	virtual ~IMcMapEnvironment() {}

public:
	/// Types of environment components (for using in bit field)
	enum EComponentType
	{
		ECT_SKY				= 0x0001,	///< \n
		ECT_STARS			= 0x0002,	///< \n
		ECT_SUN				= 0x0004,	///< \n
		ECT_FOG				= 0x0008,	///< \n
		ECT_CLOUDS			= 0x0010,	///< \n
		ECT_RAIN			= 0x0020,	///< \n
		ECT_SNOW			= 0x0040,	///< \n

		ECT_NONE			= 0x0000,	///< \n
		ECT_ALL				= 0x007F	///< \n
	};

	/// Types of sky
	enum ESkyType
	{
		EST_BACKGROUND,		///< \n
		EST_SKYBOX,			///< \n
		EST_SKYDOME,		///< \n
		EST_ANIMATED_SKY	///< \n
	};

	/// Types of sun
	enum ESunType
	{
		EST_LENS_FLARE,		///< \n
		EST_ANIMATED_SUN	///< \n
	};

	/// Types of fog
	enum EFogType
	{
		EFT_LINEAR,				///< \n
		EFT_EXPONENTIAL,		///< \n
		EFT_EXPONENTIAL_SQUARED,///< \n
		EFT_ADVANCED_FOG		///< \n
	};

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Creates an environment.
	///
	/// \param[out]	ppEnvironment	environment created.
	/// \param[in]	pViewport		viewport to contain the environment.
	///
	/// \return
	///     - status result
	//==============================================================================
	static SCENEMANAGER_API IMcErrors::ECode Create(IMcMapEnvironment **ppEnvironment, IMcMapViewport *pViewport);

	//==============================================================================
	// Method Name: EnableComponents()
	//------------------------------------------------------------------------------
	/// Enables components of the environment.
	///
	/// \param[in]	uComponentsBitField		bitfield describing the components to enable,
	///										see #EComponentType.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode EnableComponents(UINT uComponentsBitField) = 0;

	//==============================================================================
	// Method Name: DisableComponents()
	//------------------------------------------------------------------------------
	/// Disables components of the environment.
	///
	/// \param[in]	uComponentsBitField		bitfield describing the components to disable,
	///										see #EComponentType.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode DisableComponents(UINT uComponentsBitField) = 0;

	//==============================================================================
	// Method Name: GetEnabledComponents()
	//------------------------------------------------------------------------------
	/// Gets the enabled components of the environment.
	///
	/// \param[out]	puComponentsBitField	bitfield describing the enabled components
	///										of the environment, see #EComponentType.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEnabledComponents(UINT *puComponentsBitField) = 0;

	//==============================================================================
	// Method Name: ShowComponents()
	//------------------------------------------------------------------------------
	/// Shows components in the environment (provided they are active).
	///
	/// \param[in]	uComponentsBitField		bitfield describing the components to show,
	///										see #EComponentType.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode ShowComponents(UINT uComponentsBitField) = 0;

	//==============================================================================
	// Method Name: HideComponents()
	//------------------------------------------------------------------------------
	/// Hides components from the environment.
	///
	/// \param[in]	uComponentsBitField		bitfield describing the components to hide,
	///										see #EComponentType.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode HideComponents(UINT uComponentsBitField) = 0;

	//==============================================================================
	// Method Name: GetVisibleComponents()
	//------------------------------------------------------------------------------
	/// Gets the visible components of the environment.
	///
	/// \param[out]	puComponentsBitField	bitfield describing the visible components
	///										of the environment, see #EComponentType.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetVisibleComponents(UINT *puComponentsBitField) = 0;

	//==============================================================================
	// Method Name: SetSkyParams()
	//------------------------------------------------------------------------------
	/// Sets the sky parameters to use.
	///
	/// \param[in]	eType				type of sky to use.
	/// \param[in]	strMaterial			name of the sky material to use, relevant
	///									only if \a eType == #EST_SKYBOX or #EST_SKYDOME.
	/// \param[in]	BackgroundColor		color of the background, relevant only if 
	///									\a eType == #EST_BACKGROUND.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSkyParams(ESkyType eType, PCSTR strMaterial = NULL, 
		const SMcFColor &BackgroundColor = SMcFColor(0.4f, 0.75f, 1.0f, 1.0f)) = 0;

	//==============================================================================
	// Method Name: GetSkyParams()
	//------------------------------------------------------------------------------
	/// Gets the sky parameters in use.
	///
	/// \param[out]	peType				type of sky in use.
	/// \param[out]	pstrMaterial		name of the sky material in use, relevant
	///									only if \a peType == #EST_SKYBOX or #EST_SKYDOME.
	/// \param[out]	pBackgroundColor	color of the background, relevant only if 
	///									\a peType == #EST_BACKGROUND.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSkyParams(ESkyType *peType, PCSTR *pstrMaterial, SMcFColor *pBackgroundColor) = 0;

	//==============================================================================
	// Method Name: SetSunParams()
	//------------------------------------------------------------------------------
	/// Sets the sun parameters to use.
	///
	/// \param[in]	eType				type of sun to use.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSunParams(ESunType eType) = 0;

	//==============================================================================
	// Method Name: GetSunParams()
	//------------------------------------------------------------------------------
	/// Gets the sun parameters in use.
	///
	/// \param[out]	peType				type of sun in use.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSunParams(ESunType *peType) = 0;

	//==============================================================================
	// Method Name: SetFogParams()
	//------------------------------------------------------------------------------
	/// Sets the fog parameters to use.
	///
	/// \param[in]	eType			type of sun to use.
	/// \param[in]	Color			fog color, relevant if \a eType != #EFT_ADVANCED_FOG.
	/// \param[in]	fExpDensity		exponential density [0..1], relevant if \a eType == #EFT_EXPONENTIAL
	///								or #EFT_EXPONENTIAL_SQUARED.
	/// \param[in]	fLinearStart	linear fog start distance (in meters), relevant if \a eType == #EFT_LINEAR.
	/// \param[in]	fLinearEnd		linear fog end distance (in meters), relevant if \a eType == #EFT_LINEAR.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetFogParams(EFogType eType, const SMcFColor &Color = fcWhiteOpaque, 
		float fExpDensity = 0.001f, float fLinearStart = 0.0f, float fLinearEnd = 50000.0f) = 0;

	//==============================================================================
	// Method Name: GetFogParams()
	//------------------------------------------------------------------------------
	/// Gets the fog parameters in use.
	///
	/// \param[out]	peType			type of sun in use.
	/// \param[out]	pColor			fog color, relevant if \a eType != #EFT_ADVANCED_FOG.
	/// \param[out]	pfExpDensity	exponential density [0..1], relevant if \a eType == #EFT_EXPONENTIAL
	///								or #EFT_EXPONENTIAL_SQUARED.
	/// \param[out]	pfLinearStart	linear fog start distance (in meters), relevant if \a eType == #EFT_LINEAR.
	/// \param[out]	pfLinearEnd		linear fog end distance (in meters), relevant if \a eType == #EFT_LINEAR.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetFogParams(EFogType *peType, SMcFColor *pColor, 
		float *pfExpDensity, float *pfLinearStart, float *pfLinearEnd) = 0;
	
	//==============================================================================
	// Method Name: SetCloudsParams()
	//------------------------------------------------------------------------------
	/// Sets the clouds parameters to use.
	///
	/// \param[in]	fCloudCover		percentage of sky to cover with clouds [0..1]
	/// \param[in]	CloudSpeed		vector describing the speed and direction of clouds.
	///								(vector sets offset speed in meters per second
	///								when SetTimeAutoUpdateFactor equals 1.0)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetCloudsParams(float fCloudCover = 0.3f, 
		const SMcFVector2D &CloudSpeed = SMcFVector2D(0.000005f, -0.000009f)) = 0;

	//==============================================================================
	// Method Name: GetCloudsParams()
	//------------------------------------------------------------------------------
	/// Gets the clouds parameters in use.
	///
	/// \param[out]	pfCloudCover	percentage of sky covered by clouds [0..1]
	/// \param[out]	pCloudSpeed		vector describing the speed and direction of clouds.
	///								(vector sets offset speed in meters per second
	///								when SetTimeAutoUpdateFactor equals 1.0)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetCloudsParams(float *pfCloudCover, SMcFVector2D *pCloudSpeed) = 0;

	//==============================================================================
	// Method Name: SetRainParams()
	//------------------------------------------------------------------------------
	/// Sets the rain parameters in use.
	///
	/// \param[in]	fRainSpeed			speed of the rain (meters per second)
	/// \param[in]	RainDirection		direction of rain (normalized vector)
	/// \param[in]	fRainAngleDegrees	angle of the rain [0..360] (deviation of
	///									particles will be a random number from 0
	///									to fRainAngleDegrees chosen).
	/// \param[in]	fRainIntensity		intensity of the rain (number of 
	///									particles emitted per second)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetRainParams(float fRainSpeed = 500.0f, 
		const SMcFVector3D &RainDirection = SMcFVector3D(0.0f, 0.0f, -1.0f), 
		float fRainAngleDegrees = 0.0f, float fRainIntensity = 500.0f) = 0;
	
	//==============================================================================
	// Method Name: GetRainParams()
	//------------------------------------------------------------------------------
	/// Gets the rain parameters.
	///
	/// \param[out]	pfRainSpeed			speed of the rain (meters per second)
	/// \param[out]	pRainDirection		direction of rain (normalized vector)
	/// \param[out]	pfRainAngleDegrees	angle of the rain [0..360] (deviation of
	///									particles will be a random number from 0
	///									to fRainAngleDegrees chosen).
	/// \param[out]	pfRainIntensity		intensity of the rain (number of  
	///									particles emitted per second)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetRainParams(float *pfRainSpeed, 
		SMcFVector3D *pRainDirection, float *pfRainAngleDegrees, float *pfRainIntensity) = 0;

	//==============================================================================
	// Method Name: SetSnowParams()
	//------------------------------------------------------------------------------
	/// Sets the snow parameters.
	///
	/// \param[in]	fSnowSpeed			speed of the snow (meters per second)
	/// \param[in]	SnowDirection		direction of the snow (normalized vector)
	/// \param[in]	fSnowAngleDegrees	angle of the snow [0..360] (deviation of
	///									particles will be a random number from 0
	///									to fRainAngleDegrees chosen).
	/// \param[in]	fSnowIntensity		intensity of the snow (number of 
	///									particles emitted per second)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSnowParams(float fSnowSpeed = 50.0f, 
		const SMcFVector3D &SnowDirection = SMcFVector3D(0.0f, 0.0f, -1.0f), 
		float fSnowAngleDegrees = 0.0f, float fSnowIntensity = 100.0f) = 0;

	//==============================================================================
	// Method Name: GetSnowParams()
	//------------------------------------------------------------------------------
	/// Gets the snow parameters.
	///
	/// \param[out]	pfSnowSpeed			speed of the snow (meters per second)
	/// \param[out]	pSnowDirection		direction of the snow (normalized vector)
	/// \param[out]	pfSnowAngleDegrees	angle of the snow [0..360] (deviation of
	///									particles will be a random number from 0
	///									to fRainAngleDegrees chosen).
	/// \param[out]	pfSnowIntensity		intensity of the snow (number of 
	///									particles emitted per second)
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSnowParams(float *pfSnowSpeed, 
		SMcFVector3D *pSnowDirection, float *pfSnowAngleDegrees, float *pfSnowIntensity) = 0;

	//==============================================================================
	// Method Name: SetDefaultAmbientLight()
	//------------------------------------------------------------------------------
	/// Sets the default ambient light color and intensity of the scene.
	///
	/// \param[in]	Color	color and intensity of the ambient light.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetDefaultAmbientLight(const SMcFColor &Color = fcWhiteOpaque) = 0;

	//==============================================================================
	// Method Name: GetDefaultAmbientLight()
	//------------------------------------------------------------------------------
	/// Gets the default ambient light color and intensity of the scene.
	///
	/// \param[out]	pColor	color and intensity of the ambient light.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetDefaultAmbientLight(SMcFColor *pColor) = 0;

	//==============================================================================
	// Method Name: SetAbsoluteTime()
	//------------------------------------------------------------------------------
	/// Sets the absolute time of the scene.
	///
	/// \param[in]	Time	absolute time of the scene.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAbsoluteTime(CMcTime Time) = 0;

	//==============================================================================
	// Method Name: GetAbsoluteTime()
	//------------------------------------------------------------------------------
	/// Gets the absolute time of the scene.
	///
	/// \param[out]	pTime	absolute time of the scene.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAbsoluteTime(CMcTime *pTime) const = 0;

	//==============================================================================
	// Method Name: IncrementTime()
	//------------------------------------------------------------------------------
	/// Increment the scene time.
	///
	/// \param[in]	nSeconds	delta by which the scene time must be incremented.
	///							Use a negative value to decrement time.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode IncrementTime(int nSeconds) = 0;

	//==============================================================================
	// Method Name: SetTimeAutoUpdate()
	//------------------------------------------------------------------------------
	/// Sets the time auto update mode.
	///
	/// \param[in]	bEnabled	true if auto update mode must be enabled.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTimeAutoUpdate(bool bEnabled) = 0;

	//==============================================================================
	// Method Name: GetTimeAutoUpdate()
	//------------------------------------------------------------------------------
	/// Gets the time auto update mode.
	///
	/// \param[out]	pbEnabled	true if auto update mode is enabled.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTimeAutoUpdate(bool *pbEnabled) const = 0;

	//==============================================================================
	// Method Name: SetTimeAutoUpdateFactor()
	//------------------------------------------------------------------------------
	/// Sets the time auto update factor.
	///
	/// \param[in]	fFactor		factor by which the time must be auto updated. Useful
	///							to speed up the animated time of day.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTimeAutoUpdateFactor(float fFactor) = 0;

	//==============================================================================
	// Method Name: GetTimeAutoUpdateFactor()
	//------------------------------------------------------------------------------
	/// Gets the time auto update factor.
	///
	/// \param[out]	pfFactor	factor by which the time is auto updated.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTimeAutoUpdateFactor(float *pfFactor) const = 0;
};
