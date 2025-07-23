#pragma once
//==================================================================================
/// \file IMcAnimationState.h
/// Interface for the state of an animation and the weight of it's influence
//==================================================================================

#include "McExports.h"
#include "IMcErrors.h"
#include "IMcBase.h"
#include "CMcDataArray.h"

class IMcObject;
class IMcMeshItem;

//==================================================================================
// Interface Name: IMcAnimationState
//----------------------------------------------------------------------------------
/// Interface for the state of an animation and the weight of it's influence.
//==================================================================================
class IMcAnimationState : public virtual IMcBase
{
protected:

	virtual ~IMcAnimationState() {};

public:

	/// \name Create and Remove
	//@{

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates an animation state interface for the given object and mesh item
	///
	/// \param[out] ppAnimationState	The newly created animation state interface
	/// \param[in]	pObject				The object
	/// \param[in]	pMeshItem			The mesh item
	/// \param[in]  strAnimationName	The name of the animation to attach to (optional)
	/// \param[in]  bLoop				Whether to play the animation in loop (optional)
	/// \param[in]  fTimePoint			The animation time point (optional)
	/// \param[in]	fTimeDelay			The animation time delay (optional)
	/// \param[in]  fSpeedFactor		The animation speed factor (optional)
	/// \param[in]  fWeight				The animation weight (optional)
	/// \param[in]  fLength				The animation length (optional)
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcAnimationState **ppAnimationState, 
		IMcObject *pObject, IMcMeshItem *pMeshItem,
		PCSTR strAnimationName = NULL,
		bool bLoop = true, float fTimePoint = 0.0f, float fTimeDelay = 0.0f, float fSpeedFactor = 1.0f, float fWeight = 1.0f, float fLength = 0.0f);

	//==============================================================================
	// Method Name: Create(...)
	//------------------------------------------------------------------------------
	/// Creates an animation state interface for the given object and mesh item ID
	///
	/// \param[out] ppAnimationState	The newly created animation state interface
	/// \param[in]	pObject				The object
	/// \param[in]	uMeshItemID			The mesh item ID
	/// \param[in]  strAnimationName	The name of the animation to attach to (optional)
	/// \param[in]  bLoop				Whether to play the animation in loop (optional)
	/// \param[in]  fTimePoint			The animation time point (optional)
	/// \param[in]	fTimeDelay			The animation time delay (optional)
	/// \param[in]  fSpeedFactor		The animation speed factor (optional)
	/// \param[in]  fWeight				The animation weight (optional)
	/// \param[in]  fLength				The animation length (optional)
	///
	/// \return
	///     - Status result
	//==============================================================================
	static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcAnimationState **ppAnimationState, 
		IMcObject *pObject, UINT uMeshItemID,
		PCSTR strAnimationName = NULL,
		bool bLoop = true, float fTimePoint = 0.0f, float fTimeDelay = 0.0f, float fSpeedFactor = 1.0f, float fWeight = 1.0f, float fLength = 0.0f);

	//==============================================================================
	// Method Name: Remove(...)
	//------------------------------------------------------------------------------
	/// Removes the animation state from its object and mesh item, and destroys it.
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Remove() = 0;

	//@}

	/// \name Animation Attachment
	//@{

	//==============================================================================
	// Method Name: AttachToAnimation(...)
	//------------------------------------------------------------------------------
	/// Attach the animation state to an animation identified by its name.
	/// 
	/// Attaching to a new animation disables the current animation and enables the new one.
	///
	/// \param[in] strAnimationName		The name of the animation to attach to (NULL or empty 
	///									mean disabling the current animation and detaching from it)
	/// \param[in] bLoop				Whether to play the animation in loop (optional)
	/// \param[in] fTimePoint			The animation time point (optional)
	/// \param[in]	fTimeDelay			The animation time delay (optional)
	/// \param[in] fSpeedFactor			The animation speed factor (optional)
	/// \param[in] fWeight				The animation weight (optional)
	/// \param[in] fLength				The animation length (optional)
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode AttachToAnimation(
		PCSTR strAnimationName, bool bLoop = true, float fTimePoint = 0.0f, float fTimeDelay = 0.0f, float fSpeedFactor = 1.0f, float fWeight = 1.0f, float fLength = 0.0f) = 0;

	//==============================================================================
	// Method Name: GetAttachedAnimation(...)
	//------------------------------------------------------------------------------
	/// Retrieves the name of the animation to which this state attached
	/// 
	/// \param[out]	pstrAnimationName	The animation name
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachedAnimation(PCSTR *pstrAnimationName) const = 0;

	//@}

	/// \name Object and MeshItem
	//@{

	//==============================================================================
	// Method Name: GetObject(...)
	//------------------------------------------------------------------------------
	/// Retrieves the object
	/// 
	/// \param[out]	ppObject	The object
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetObject(IMcObject **ppObject) const = 0;

	//==============================================================================
	// Method Name: GetMeshItem(...)
	//------------------------------------------------------------------------------
	/// Retrieves the mesh item
	/// 
	/// \param[out]	ppMeshItem	The mesh item
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetMeshItem(IMcMeshItem **ppMeshItem) const = 0;

	//@}

	/// \name Animation Enabling
	//@{

	//==============================================================================
	// Method Name: SetEnabled(...)
	//------------------------------------------------------------------------------
	/// Sets whether the animation is enabled
	///
	/// \param[in] bEnabled		Whether the animation is enabled
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetEnabled(bool bEnabled) = 0;

	//==============================================================================
	// Method Name: GetEnabled(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether the animation is currently enabled
	/// 
	/// \param[out]	pbEnabled	Whether the animation is currently enabled
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetEnabled(bool *pbEnabled) const = 0;

	//@}

	/// \name Animation Time Point
	//@{

	//==============================================================================
	// Method Name: SetTimePoint(...)
	//------------------------------------------------------------------------------
	/// Sets the animation time point to jump
	///
	/// \param[in] fTimePoint	The animation time point
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetTimePoint(float fTimePoint) = 0;

	//==============================================================================
	// Method Name: GetTimePoint(...)
	//------------------------------------------------------------------------------
	/// Retrieves the current time point of the animation
	/// 
	/// \param[out]	pfTimePoint		The animation time point
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetTimePoint(float *pfTimePoint) const = 0;

	/// \name Animation Parameters
	//@{

	//==============================================================================
	// Method Name: SetWeight(...)
	//------------------------------------------------------------------------------
	/// Sets the animation weight (influence)
	///
	/// \param[in] fWeight			The animation weight
	/// \param[in] fChangeDuration	The duration of the animation weight change
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetWeight(float fWeight, float fChangeDuration = 0) = 0;

	//==============================================================================
	// Method Name: GetWeight(...)
	//------------------------------------------------------------------------------
	/// Retrieves the animation weight (influence)
	/// 
	/// \param[out]	pfWeight	The animation weight
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetWeight(float *pfWeight) const = 0;

	//==============================================================================
	// Method Name: SetAttachPointsWeights(...)
	//------------------------------------------------------------------------------
	/// Sets weights of the mesh's all attach points to be used when applying this animation
	///
	/// \param[in] afWeights		The weights of the mesh's all attach points according to their indices
	///								retrieved by IMcNativeMesh::GetAttachPointIndexByName(); array length 
	///								should be the value retrieved by IMcNativeMesh::GetNumAttachPoints()
	/// \param[in] fChangeDuration	The duration of the animation weight change
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetAttachPointsWeights(const float afWeights[], float fChangeDuration = 0) = 0;

	//==============================================================================
	// Method Name: GetAttachPointsWeights(...)
	//------------------------------------------------------------------------------
	/// Retrieves weights of the mesh's all attach points to be used when applying this animation
	///
	/// \param[out] pafWeights		The weights of the mesh's all attach points according to their indices
	///								retrieved by IMcNativeMesh::GetAttachPointIndexByName(); array length 
	///								is the value retrieved by IMcNativeMesh::GetNumAttachPoints()
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetAttachPointsWeights(CMcDataArray<float> *pafWeights) const = 0;

	//==============================================================================
	// Method Name: SetLength(...)
	//------------------------------------------------------------------------------
	/// Sets the total length of the animation (may be shorter than whole animation)
	///
	/// \param[in] fLength	The animation length
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetLength(float fLength) = 0;

	//==============================================================================
	// Method Name: GetLength(...)
	//------------------------------------------------------------------------------
	/// Retrieves the total length of the animation (may be shorter than whole animation)
	/// 
	/// \param[out]	pfLength	The animation length
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLength(float *pfLength) const = 0;

	//==============================================================================
	// Method Name: SetLoop(...)
	//------------------------------------------------------------------------------
	/// Sets whether the animation loops at the start and end if the time continues to be altered
	///
	/// \param[in] bLoop		Whether to play the animation in loop
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetLoop(bool bLoop) = 0;

	//==============================================================================
	// Method Name: GetLoop(...)
	//------------------------------------------------------------------------------
	/// Retrieves whether the animation loops at the start and end if the time continues to be altered
	/// 
	/// \param[out]	pbLoop		Whether to InLoop the animation in loop
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetLoop(bool *pbLoop) const = 0;

	//==============================================================================
	// Method Name: SetSpeedFactor(...)
	//------------------------------------------------------------------------------
	/// Sets the animation speed factor
	///
	/// \param[in] fSpeedFactor		The animation speed factor
	///        
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode SetSpeedFactor(float fSpeedFactor) = 0;

	//==============================================================================
	// Method Name: GetSpeedFactor(...)
	//------------------------------------------------------------------------------
	/// Retrieves the animation speed factor
	/// 
	/// \param[out]	pfSpeedFactor	The animation speed factor
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode GetSpeedFactor(float *pfSpeedFactor) const = 0;

	//==============================================================================
	// Method Name: HasEnded(...)
	//------------------------------------------------------------------------------
	/// Checks whether the animation has reached the end and is not looping
	/// 
	/// \param[out]	pbEnded		Whether the animation has reached the end and is not looping
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode HasEnded(bool *pbEnded) const = 0;

	//@}
};
