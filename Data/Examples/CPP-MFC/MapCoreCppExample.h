
// MapCoreCppExample.h : main header file for the MapCoreCppExample application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CMapCoreCppExampleApp:
// See MapCoreCppExample.cpp for the implementation of this class
//

class CMapCoreCppExampleApp : public CWinAppEx
{
public:
	CMapCoreCppExampleApp();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	UINT  m_nAppLook;
	BOOL  m_bHiColorIcons;

	virtual void PreLoadState();
	virtual void LoadCustomState();
	virtual void SaveCustomState();

	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CMapCoreCppExampleApp theApp;
