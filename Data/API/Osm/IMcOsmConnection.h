//===========================================================================
/// \file IMcOsmConnection.h
/// Geo-coding, and routing database connectivity
//===========================================================================

#pragma once

#include "IMcBase.h"
#include "McOsmCommon.h"

///////////////////////////////////////////////////////////////////////////////////
/// MCOsmConnection - Maintains the database connection objects
class IMcOsmConnection : public virtual IMcBase
{
public:
	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Create
	///
	/// Constructs a new connection object based upon connection string.
	/// An example of a valid connection string is 
	/// postgresql://localhost:5432/mydb?user=myuser&password=mypassword
	/// 
	/// \param[out]	ppMcOsmConnection		newly created connection object
	/// \param[in]	strConnectionString		Connection string
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode MC_OSM_PLUGIN_API Create(IMcOsmConnection **ppMcOsmConnection, PCSTR strConnectionString);

	//==============================================================================
	// Method Name: Create()
	//------------------------------------------------------------------------------
	/// Constructs a new connection object based upon host, port, database, user and password
	///
	/// \param[out]	ppMcOsmConnection		newly created connection object
	/// \param[in]	strHost					The hosting computer name or ip-address 
	/// \param[in]	uPort					TCP port for used for the connection
	/// \param[in]	strDatabase				The database name
	/// \param[in]	strUser					User who logs in to the database
	/// \param[in]	strPassword				Login password
	///
	/// \return
	///     - status result
	//==============================================================================
	static IMcErrors::ECode MC_OSM_PLUGIN_API Create(IMcOsmConnection **ppMcOsmConnection, PCSTR strHost, UINT uPort, 
		PCSTR strDatabase, PCSTR strUser, PCSTR strPassword);

	//==============================================================================
	// Method Name: Clone()
	//------------------------------------------------------------------------------
	/// Clones the connection object based upon an existing connection object
	/// The database connection object is duplicated.
	///
	/// \param[out]	ppMcOsmConnection		Created connection
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode Clone(IMcOsmConnection **ppMcOsmConnection) = 0;

	//==============================================================================
	// Method Name: IsDatabaseConnected()
	//------------------------------------------------------------------------------
	/// Checks the database connection status
	///
	/// \param[out]	pbDataBaseConnected		whether or not the database is connected
	///
	/// \return
	///     - status result
	//==============================================================================
	virtual IMcErrors::ECode IsDataBaseConnected(bool *pbDataBaseConnected) = 0;
};
