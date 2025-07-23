#pragma once
//==================================================================================
/// \file IMcPolygonItem.h
/// Interface for polygon item
//==================================================================================

#include "McExports.h"
#include "McCommonTypes.h"
#include "OverlayManager/IMcLineBasedItem.h"

//==================================================================================
// Interface Name: IMcPolygonItem
//----------------------------------------------------------------------------------
///	Interface for polygon item.
/// 
//==================================================================================
class IMcPolygonItem : public virtual IMcClosedShapeItem
{
protected:

	virtual ~IMcPolygonItem() {};

public:

    enum
    {
        //==============================================================================
        /// Node unique ID for this interface
        //==============================================================================
        NODE_TYPE = 63
    };

public:

    /// \name Create
    //@{

    //==============================================================================
    // Method Name: Create(...)
    //------------------------------------------------------------------------------
    /// Create a polygon item.
    ///
	/// The item will be destroyed when its reference count reaches 0.
	/// Therefore -
	/// unless its AddRef() method was specifically called,
	/// it will be destroyed when its Release() method is called,
	/// when its (or its parents) Disconnect() method is called,
	/// or when its object scheme is destroyed.
	///
    /// \param[out] ppItem						The newly created item
	/// \param[in] uItemSubTypeBitField			The item sub type (see IMcObjectSchemeItem::EItemSubTypeFlags)
    /// \param[in] DefaultLineStyle				The default line style
    /// \param[in] DefaultLineColor				The default line color
    /// \param[in] fDefaultLineWidth			The default line width (in meters)
    /// \param[in] pDefaultLineTexture			The default line texture
	/// \param[in] DefaultLineTextureHeightRange The default line texture height range
    /// \param[in] fDefaultLineTextureScale		The default line texture scale
    /// \param[in] eDefaultFillStyle			The default fill style
    /// \param[in] DefaultFillColor				The default fill color
    /// \param[in] pDefaultFillTexture			The default fill texture
    /// \param[in] DefaultFillTextureScale		The default fill texture scale
    ///
    /// \return
    ///     - Status result
    //==============================================================================
    static IMcErrors::ECode OVERLAYMANAGER_API Create(
		IMcPolygonItem **ppItem,
		UINT uItemSubTypeBitField,
        ELineStyle DefaultLineStyle = ELS_SOLID,
        const SMcBColor &DefaultLineColor = bcBlackOpaque,
        float fDefaultLineWidth = 1.0,
        IMcTexture *pDefaultLineTexture = NULL,
		const SMcFVector2D &DefaultLineTextureHeightRange = SMcFVector2D(0.0, -1.0),
        float fDefaultLineTextureScale = 1.0,
        EFillStyle eDefaultFillStyle = EFS_SOLID,
        const SMcBColor &DefaultFillColor = bcBlackOpaque,
        IMcTexture *pDefaultFillTexture = NULL,
        SMcFVector2D DefaultFillTextureScale = SMcFVector2D(1.0, 1.0));
 
    //@}

	/// \name Clone
	//@{

	//==============================================================================
	// Method Name: Clone(...)
	//------------------------------------------------------------------------------
	/// Clone the item.
	///
	/// \param[out] ppClonedItem	The newly cloned item
	/// \param[in]  pObject			Optional object, to take the values of private properties from
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Clone(
		IMcPolygonItem **ppClonedItem, IMcObject *pObject = NULL) = 0;

	//@}
};
