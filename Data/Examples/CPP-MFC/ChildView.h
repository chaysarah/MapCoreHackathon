
// ChildView.h : interface of the CChildView class
//

#pragma once

#include "Map/McMapManagement.h"
#include "IMcEditMode.h"

// CChildView window

class CChildView : public CWnd
{
// Construction
public:
	CChildView();

// Attributes
public:

// Operations
public:

// Overrides
protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// Implementation
public:
	virtual ~CChildView();

	// Generated message map functions
	//{{AFX_MSG(CChildView)
	afx_msg void OnDestroy( );
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnRButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg BOOL OnMouseWheel(UINT nFlags, short zDelta, CPoint point);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnUseEditMode();
	afx_msg void OnUpdateUseEditMode(CCmdUI *pCmdUI);
	//}}AFX_MSG
protected:
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()

	void Render();
	bool CheckMcResult(IMcErrors::ECode eResult);
	bool OpenMap();

	SMcVector3D				m_RotateCenter;
	SMcVector3D				m_LastMouseMovePixel;

	IMcMapDevice			*m_pMapDevice;
	IMcMapViewport			*m_pViewport;
	IMcEditMode				*m_pEditMode;
	IMcMapCamera::EMapType	m_eMapType;
	bool					m_bRendering;
	bool					m_bDirty;
	bool					m_bViewportCreated;
	IMcErrors::ECode		m_eLastError;
};

