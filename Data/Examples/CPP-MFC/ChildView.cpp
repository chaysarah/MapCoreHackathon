
// ChildView.cpp : implementation of the CChildView class
//

#include "stdafx.h"
#include "MapCoreCppExample.h"
#include "ChildView.h"
#include <math.h>
#include "Calculations/IMcGridCoordinateSystem.h"
#include "Production/IMcMapProduction.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

PCSTR		strRasterDir			= "C:/Maps/Israel/Raster";
PCSTR		strDtmDir				= "C:/Maps/Israel/DTM";
PCSTR		strVector3DExtrusionDir	= NULL;
PCSTR		str3DModelDir			= NULL;
IMcMapCamera::EMapType eMapType		= IMcMapCamera::EMT_3D;

// CChildView

BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_DESTROY()
	ON_WM_SIZE()
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_RBUTTONDOWN()
	ON_WM_RBUTTONUP()
	ON_WM_MBUTTONDOWN()
	ON_WM_MOUSEMOVE()
	ON_WM_MOUSEWHEEL()
	ON_WM_TIMER()
	ON_WM_KEYDOWN()
	ON_WM_PAINT()
	ON_COMMAND(ID_EDIT_USE_EDIT_MODE, OnUseEditMode)
	ON_UPDATE_COMMAND_UI(ID_EDIT_USE_EDIT_MODE, OnUpdateUseEditMode)
END_MESSAGE_MAP()

CChildView::CChildView()
	: m_pMapDevice(NULL), m_pViewport(NULL), m_pEditMode(NULL), m_eMapType(eMapType), 
	  m_bRendering(false), m_bViewportCreated(false), m_bDirty(false), m_eLastError(IMcErrors::SUCCESS)
{
}

CChildView::~CChildView()
{
	if (m_pMapDevice != NULL)
	{
		m_pMapDevice->Release();
	}
}

BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);

	return TRUE;
}

void CChildView::OnPaint() 
{
	CPaintDC dc(this); // device context for painting
	
	// TODO: Add your message handler code here
	
	// Do not call CWnd::OnPaint() for painting messages
}

bool CChildView::CheckMcResult(IMcErrors::ECode eResult)
{
	if (eResult!= IMcErrors::SUCCESS)
	{
		m_eLastError = eResult;
		return false;
	}
	return true;
}

bool CChildView::OpenMap()
{
    SetTimer(1, 10, NULL);
	if (!CheckMcResult(IMcMapDevice::Create(&m_pMapDevice, IMcMapDevice::SInitParams())))
	{
		return false;
	}

	m_pMapDevice->AddRef();

	CArray<IMcMapLayer*, IMcMapLayer*> apLayers;

	if (strRasterDir != NULL && strRasterDir[0] != '\0')
	{
		IMcNativeRasterMapLayer *pLayer;
		if (!CheckMcResult(IMcNativeRasterMapLayer::Create(&pLayer, strRasterDir)))
		{
			m_pMapDevice->Release();
			return false;
		}
		apLayers.Add(pLayer);
	}

	if (strDtmDir != NULL && strDtmDir[0] != '\0')
	{
		IMcNativeDtmMapLayer *pLayer;
		if (!CheckMcResult(IMcNativeDtmMapLayer::Create(&pLayer, strDtmDir)))
		{
			for (int i = 0; i < apLayers.GetSize(); ++i)
			{
				apLayers[i]->Release();
			}
			m_pMapDevice->Release();
			return false;
		}
		apLayers.Add(pLayer);
	}

	if (strVector3DExtrusionDir != NULL && strVector3DExtrusionDir[0] != '\0')
	{
		IMcNativeVector3DExtrusionMapLayer *pLayer;
		if (!CheckMcResult(IMcNativeVector3DExtrusionMapLayer::Create(&pLayer, strVector3DExtrusionDir)))
		{
			for (int i = 0; i < apLayers.GetSize(); ++i)
			{
				apLayers[i]->Release();
			}
			m_pMapDevice->Release();
			return false;
		}
		apLayers.Add(pLayer);
	}

	if (str3DModelDir != NULL && str3DModelDir[0] != '\0')
	{
		IMcNative3DModelMapLayer *pLayer;
		if (!CheckMcResult(IMcNative3DModelMapLayer::Create(&pLayer, str3DModelDir)))
		{
			for (int i = 0; i < apLayers.GetSize(); ++i)
			{
				apLayers[i]->Release();
			}
			m_pMapDevice->Release();
			return false;
		}
		apLayers.Add(pLayer);
	}

	IMcGridCoordinateSystem *pCoordSys = NULL;
	SMcVector3D CameraPos(v3Zero);

	if (!apLayers.IsEmpty())
	{
		if (apLayers[0]->GetCoordinateSystem(&pCoordSys) != IMcErrors::SUCCESS)
		{
			return false;
		}
		SMcBox LayerBox;
		apLayers[0]->GetBoundingBox(&LayerBox);
		CameraPos = LayerBox.CenterPoint();
	}

	IMcMapViewport::SCreateData CreateData(m_eMapType);
	CreateData.pCoordinateSystem = pCoordSys;

	IMcMapTerrain *pTerrain;
	if (!CheckMcResult(IMcMapTerrain::Create(&pTerrain, pCoordSys, 
					   apLayers.GetData(), (UINT)apLayers.GetSize(),NULL)))
	{
		for (int i = 0; i < apLayers.GetSize(); ++i)
		{
			apLayers[i]->Release();
		}
		m_pMapDevice->Release();
		return false;
	}

	CreateData.pDevice = m_pMapDevice;

	CreateData.bFullScreen = false;
	CreateData.hWnd = m_hWnd;
	IMcMapCamera *pCamera;

	if (!CheckMcResult(IMcOverlayManager::Create(&CreateData.pOverlayManager, CreateData.pCoordinateSystem)))
	{
		pTerrain->Release();
		return false;
	}
	if (!CheckMcResult(IMcMapViewport::Create(&m_pViewport, &pCamera, CreateData, &pTerrain, 1)))
	{
		CreateData.pOverlayManager->Release();
		pTerrain->Release();
		m_pMapDevice->Release();
		return false;
	}
	m_pViewport->AddRef();
	//m_pViewport->SetDebugOption(/*ELO_BOX_DRAW_MODE*/0, 2);
	m_pViewport->SetBackgroundColor(SMcBColor(140, 140, 140, 255));
	m_pViewport->SetCameraRelativeHeightLimits(1.7, DBL_MAX, true);

	if (m_eMapType == IMcMapCamera::EMT_3D)
	{
		m_pViewport->SetCameraClipDistances(0.1, 0, true);
		bool bHeightFound = false;
		CameraPos.z /*= MC_NO_DTM_VALUE*/;
		IMcSpatialQueries::SQueryParams Params;
		Params.eTerrainPrecision = IMcSpatialQueries::EQP_HIGHEST;
		if (strDtmDir[0] != '\0' && 
			m_pViewport->GetTerrainHeight(CameraPos, &bHeightFound, &CameraPos.z, NULL, &Params) == IMcErrors::SUCCESS &&
			bHeightFound == true)
		{
			CameraPos.z += 10;
		}
		else
		{
			CameraPos.z = 300;
		}
		IMcMapEnvironment *pEnvironment;
		IMcMapEnvironment::Create(&pEnvironment, m_pViewport);
		
		pEnvironment->SetSkyParams(IMcMapEnvironment::EST_SKYBOX, "fxplugin_demo/skybox");
		pEnvironment->SetDefaultAmbientLight(SMcFColor(1.0f, 1.0f, 1.0f, 1.0f));
		pEnvironment->EnableComponents(IMcMapEnvironment::ECT_SKY);
		pEnvironment->ShowComponents(IMcMapEnvironment::ECT_SKY);
	}
	else
	{
		CameraPos.z = 0;
	}

	m_pViewport->SetCameraPosition(CameraPos);
	IMcEditMode::Create(m_pViewport, &m_pEditMode);
	m_pEditMode->StartNavigateMap(true);
	return true;
}

void CChildView::OnDestroy()
{
    m_bRendering = true;
	KillTimer(1);

	if (m_pEditMode != NULL)
	{
		m_pEditMode->Destroy();
	}

	if (m_pViewport != NULL)
	{
		m_pViewport->Release();
		m_pViewport = NULL;
	}

	CWnd::OnDestroy();
}

void CChildView::OnSize(UINT nType, int cx, int cy)
{
	CWnd::OnSize(nType, cx, cy);

	if (cx != 0 && cy != 0 && m_eLastError == IMcErrors::SUCCESS)
	{
		m_bDirty = true;
		if (!m_bViewportCreated)
		{
			m_bViewportCreated = true;
			OpenMap();
		}
		else
		{
			m_pViewport->ViewportResized();
		}
	}
}

void CChildView::Render()
{
	if (!m_bRendering)
	{
		m_bRendering = true;
		if (m_eLastError != IMcErrors::SUCCESS)
		{
			PCSTR strError;
			IMcErrors::ErrorCodeToString(m_eLastError, &strError);
			MessageBox(strError, "Error", MB_OK | MB_ICONERROR);
			exit(-1);
		}
		else
		{
			bool bRenderNeeded;
			if (m_bDirty || (m_pViewport->HasPendingUpdates(&bRenderNeeded) == IMcErrors::SUCCESS && bRenderNeeded))
			{
				m_bDirty = false;
				m_pViewport->Render();
			}
		}
		m_bRendering = false;
	}
}

void CChildView::OnLButtonDown(UINT nFlags, CPoint point) 
{
	if (m_pEditMode != NULL)
	{
		bool bRenderNeeded;
		IMcEditMode::ECursorType eCursorType;
		if (m_pEditMode->OnMouseEvent(IMcEditMode::EME_BUTTON_PRESSED, point, false, 0, 
			&bRenderNeeded, &eCursorType) == IMcErrors::SUCCESS && bRenderNeeded)
		{
			Render();
		}
	}
	else
	{
		m_LastMouseMovePixel = SMcVector3D(point.x, point.y, 0);
	}
    CWnd::OnLButtonDown(nFlags, point);
}

void CChildView::OnLButtonUp(UINT nFlags, CPoint point) 
{
	if (m_pEditMode != NULL)
	{
		bool bRenderNeeded;
		IMcEditMode::ECursorType eCursorType;
		if (m_pEditMode->OnMouseEvent(IMcEditMode::EME_BUTTON_RELEASED, point, false, 0, 
			&bRenderNeeded, &eCursorType) == IMcErrors::SUCCESS && bRenderNeeded)
		{
			Render();
		}
	}
	else
	{
		m_LastMouseMovePixel = SMcVector3D(point.x, point.y, 0);
	}
    CWnd::OnLButtonDown(nFlags, point);
}

void CChildView::OnRButtonDown(UINT nFlags, CPoint point) 
{
	if (m_pEditMode != NULL)
	{
		bool bRenderNeeded;
		IMcEditMode::ECursorType eCursorType;
		if (m_pEditMode->OnMouseEvent(IMcEditMode::EME_BUTTON_PRESSED, point, true, 0, 
			&bRenderNeeded, &eCursorType) == IMcErrors::SUCCESS && bRenderNeeded)
		{
			Render();
		}
	}
	else
	{
		m_LastMouseMovePixel = SMcVector3D(point.x, point.y, 0);
	}
	CWnd::OnRButtonDown(nFlags, point);
}

void CChildView::OnRButtonUp(UINT nFlags, CPoint point) 
{
	if (m_pEditMode != NULL)
	{
		bool bRenderNeeded;
		IMcEditMode::ECursorType eCursorType;
		if (m_pEditMode->OnMouseEvent(IMcEditMode::EME_BUTTON_RELEASED, point, true, 0, 
			&bRenderNeeded, &eCursorType) == IMcErrors::SUCCESS && bRenderNeeded)
		{
			Render();
		}
	}
	else
	{
		m_LastMouseMovePixel = SMcVector3D(point.x, point.y, 0);
	}
    CWnd::OnLButtonDown(nFlags, point);
}

void CChildView::OnMButtonDown(UINT nFlags, CPoint point) 
{
	m_LastMouseMovePixel = SMcVector3D(point.x, point.y, 0);
	bool bIntersection;
	if (m_pViewport->ScreenToWorldOnTerrain(m_LastMouseMovePixel, &m_RotateCenter, &bIntersection) != 
		IMcErrors::SUCCESS || !bIntersection)
	{
		if (m_pViewport->ScreenToWorldOnPlane(m_LastMouseMovePixel, &m_RotateCenter, &bIntersection) != 
			IMcErrors::SUCCESS || !bIntersection)
		{
			m_RotateCenter.x = DBL_MAX;
		}
	}

    CWnd::OnRButtonDown(nFlags, point);
}

void CChildView::OnMouseMove(UINT nFlags, CPoint point) 
{
	if (m_pEditMode != NULL)
	{
		bool bRenderNeeded;
		IMcEditMode::ECursorType eCursorType;
		if (m_pEditMode->OnMouseEvent(
			(nFlags & (MK_LBUTTON | MK_RBUTTON)) ? IMcEditMode::EME_MOUSE_MOVED_BUTTON_DOWN : IMcEditMode::EME_MOUSE_MOVED_BUTTON_UP, 
			point, (nFlags & MK_RBUTTON) != 0, 0, &bRenderNeeded, &eCursorType) == IMcErrors::SUCCESS && bRenderNeeded)
		{
			Render();
		}
	}

	SMcVector3D MouseMovePixel = SMcVector3D(point.x, point.y, 0);
	SMcVector3D Delta = m_LastMouseMovePixel - MouseMovePixel;
	if (nFlags & MK_CONTROL)
	{
		Delta *= 10;
	}
	if ((nFlags & MK_LBUTTON) && m_pEditMode == NULL)
	{ // left button rotates camera
		if (m_eMapType == IMcMapCamera::EMT_2D)
		{
			float fDeltaYaw = float(fabs(Delta.x) > fabs(Delta.y) ? Delta.x : -Delta.y);
			m_pViewport->SetCameraOrientation(fDeltaYaw / 8, 0, 0, true);
		}
		else
		{
			m_pViewport->RotateCameraRelativeToOrientation(float(Delta.x / 8), float(-Delta.y) / 8, 0);
		}
		Render();
	}
	else if (nFlags & MK_RBUTTON && m_pEditMode == NULL)
	{ // right button changes camera position
		if (m_eMapType == IMcMapCamera::EMT_2D)
		{
			m_pViewport->ScrollCamera(int(Delta.x), int(Delta.y));
		}
		else
		{
			Delta.y = - Delta.y;
			m_pViewport->MoveCameraRelativeToOrientation(Delta);
		}
			Render();
	}
	else if (nFlags & MK_MBUTTON)
	{ // middle button rotates camera around point
		if (m_eMapType == IMcMapCamera::EMT_2D)
		{
			m_pViewport->RotateCameraAroundWorldPoint(m_RotateCenter, float(Delta.x * 0.1), 0.0f, 0.0f, false);
		}
		else
		{
			float fFOV;
			m_pViewport->GetCameraFieldOfView(&fFOV);
			Delta *= 0.04 * (fFOV / 45);

			m_pViewport->RotateCameraAroundWorldPoint(m_RotateCenter, float(Delta.x), 0.0f, 0.0f, false);
			m_pViewport->RotateCameraAroundWorldPoint(m_RotateCenter, 0.0f, float(-Delta.y), 0.0f, true);
		}
		Render();
	}

	m_LastMouseMovePixel = MouseMovePixel;
    CWnd::OnMouseMove(nFlags, point);
}

BOOL CChildView::OnMouseWheel(UINT nFlags, short zDelta, CPoint point)
{
	if (m_pEditMode != NULL && m_eMapType == IMcMapCamera::EMT_3D)
	{
		bool bRenderNeeded;
		IMcEditMode::ECursorType eCursorType;
		
		CWnd::ScreenToClient(&point);
		if (m_pEditMode->OnMouseEvent(IMcEditMode::EME_MOUSE_WHEEL, point, true, zDelta, 
			&bRenderNeeded, &eCursorType) == IMcErrors::SUCCESS && bRenderNeeded)
		{
			Render();
		}
	}
	else
	{
		short nEffectiveDelta = zDelta;
		if (nFlags & MK_CONTROL)
		{
			nEffectiveDelta *= 5;
		}
		if (m_eMapType == IMcMapCamera::EMT_2D)
		{
			float fScale;
			m_pViewport->GetCameraScale(&fScale);
			float fFactor = (1.0f + (float)fabs(nEffectiveDelta/1200.0f));
			if (nEffectiveDelta > 0)
			{
				fScale /= fFactor;
			}
			else
			{
				fScale *= fFactor;
			}
			if (fScale >= 0.01)
			{
				m_pViewport->SetCameraScale(fScale);
			}
		}
		else
		{
			m_pViewport->SetCameraPosition(SMcVector3D(0, 0, nEffectiveDelta), true);
		}
		Render();
	}

	return CWnd::OnMouseWheel(nFlags, zDelta, point);
}

void CChildView::OnTimer(UINT_PTR nIDEvent) 
{
    Render();
	CWnd::OnTimer(nIDEvent);
}

void CChildView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags) 
{
	int nDelta = 10;
	if (GetKeyState(VK_CONTROL) < 0)
	{
		nDelta *= 10;
	}

	switch(nChar)
	{
		case VK_LEFT: 
			if (m_pEditMode != NULL)
			{
				bool bRenderNeeded;
				if (m_pEditMode->OnKeyEvent(IMcEditMode::EKE_MOVE_RIGHT, &bRenderNeeded) == IMcErrors::SUCCESS && 
					bRenderNeeded)
				{
					Render();
				}
			}
			else if (m_eMapType == IMcMapCamera::EMT_2D)
			{
				m_pViewport->ScrollCamera(-nDelta, 0);
				Render();
			}
			else
			{
				m_pViewport->MoveCameraRelativeToOrientation(SMcVector3D(-nDelta, 0, 0));
				Render();
			}
			break;
		case VK_RIGHT:
			if (m_pEditMode != NULL)
			{
				bool bRenderNeeded;
				if (m_pEditMode->OnKeyEvent(IMcEditMode::EKE_MOVE_LEFT, &bRenderNeeded) == IMcErrors::SUCCESS && 
					bRenderNeeded)
				{
					Render();
				}
			}
			else if (m_eMapType == IMcMapCamera::EMT_2D)
			{
				m_pViewport->ScrollCamera(nDelta, 0);
				Render();
			}
			else
			{
				m_pViewport->MoveCameraRelativeToOrientation(SMcVector3D(nDelta, 0, 0));
				Render();
			}
			break;
		case VK_UP: 
			if (m_pEditMode != NULL)
			{
				bool bRenderNeeded;
				if (m_pEditMode->OnKeyEvent(IMcEditMode::EKE_MOVE_DOWN, &bRenderNeeded) == IMcErrors::SUCCESS && 
					bRenderNeeded)
				{
					Render();
				}
			}
			else if (m_eMapType == IMcMapCamera::EMT_2D)
			{
				m_pViewport->ScrollCamera(0, -nDelta);
				Render();
			}
			else
			{
				m_pViewport->MoveCameraRelativeToOrientation(SMcVector3D(0, nDelta, 0));
				Render();
			}
			break;
		case VK_DOWN: 
			if (m_pEditMode != NULL)
			{
				bool bRenderNeeded;
				if (m_pEditMode->OnKeyEvent(IMcEditMode::EKE_MOVE_UP, &bRenderNeeded) == IMcErrors::SUCCESS && 
					bRenderNeeded)
				{
					Render();
				}
			}
			else if (m_eMapType == IMcMapCamera::EMT_2D)
			{
				m_pViewport->ScrollCamera(0, nDelta);
				Render();
			}
			else
			{
				m_pViewport->MoveCameraRelativeToOrientation(SMcVector3D(0, -nDelta, 0));
				Render();
			}
			break;
		case VK_PRIOR: 
			if (m_pEditMode != NULL && m_eMapType == IMcMapCamera::EMT_3D)
			{
				bool bRenderNeeded;
				if (m_pEditMode->OnKeyEvent(IMcEditMode::EKE_RAISE, &bRenderNeeded) == IMcErrors::SUCCESS && 
					bRenderNeeded)
				{
					Render();
				}
			}
			else if (m_eMapType == IMcMapCamera::EMT_2D)
			{
				m_pViewport->SetCameraOrientation(float(-nDelta) / 10, 0, 0, true);
				Render();
			}
			else
			{
				m_pViewport->SetCameraPosition(SMcVector3D(0, 0, nDelta), true);
				Render();
			}
			break;
		case VK_NEXT: 
			if (m_pEditMode != NULL && m_eMapType == IMcMapCamera::EMT_3D)
			{
				bool bRenderNeeded;
				if (m_pEditMode->OnKeyEvent(IMcEditMode::EKE_LOWER, &bRenderNeeded) == IMcErrors::SUCCESS && 
					bRenderNeeded)
				{
					Render();
				}
			}
			else if (m_eMapType == IMcMapCamera::EMT_2D)
			{
				m_pViewport->SetCameraOrientation(float(nDelta) / 10, 0, 0, true);
				Render();
			}
			else
			{
				m_pViewport->SetCameraPosition(SMcVector3D(0, 0, -nDelta), true);
				Render();
			}
			break;
	}
   CWnd::OnKeyDown(nChar, nRepCnt, nFlags);
}

void CChildView::OnUseEditMode()
{
	if (m_pEditMode != NULL)
	{
		m_pEditMode->ExitCurrentAction(0);
		m_pEditMode->Destroy();
		m_pEditMode = NULL;
	}
	else
	{
		IMcEditMode::Create(m_pViewport, &m_pEditMode);
		m_pEditMode->StartNavigateMap(true);
	}
}

void CChildView::OnUpdateUseEditMode(CCmdUI *pCmdUI)
{
	pCmdUI->SetCheck(m_pEditMode != NULL);
}
