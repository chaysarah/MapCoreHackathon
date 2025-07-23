//===========================================================================
/// \file CMcTime.h
/// CMcTime class
//===========================================================================

#pragma once

#include <time.h>

//===========================================================================
// CMcTime class
//---------------------------------------------------------------------------
///	Interface for time data.
/// 
//===========================================================================
class CMcTime
{
public:
	
	/// \name Construction & Assignment
	//@{
	CMcTime();
	CMcTime(time_t time);
	CMcTime(int nYear, int nMonth, int nDay, int nHour, int nMin, int nSec);

	CMcTime& operator=(time_t time);
	//@}

	/// \name Assignment Operators
	//@{
	CMcTime& operator+=(int nSeconds);
	CMcTime& operator-=(int nSeconds);

	int operator-(CMcTime time) const;
	CMcTime operator-(int nSeconds) const;
	CMcTime operator+(int nSeconds) const;
	//@}

	/// \name Compare Operators
	//@{
	bool operator==(CMcTime time) const;
	bool operator!=(CMcTime time) const;
	bool operator<(CMcTime time) const;
	bool operator>(CMcTime time) const;
	bool operator<=(CMcTime time) const;
	bool operator>=(CMcTime time) const;
	//@}

	/// \name Get time data
	//@{
	time_t GetTime() const;

	int GetYear() const;
	int GetMonth() const;
	int GetDay() const;
	int GetHour() const;
	int GetMinute() const;
	int GetSecond() const;
	int GetDayOfWeek() const;
	//@}

	/// \name Get current time
	//@{
	static CMcTime GetCurrentTime();
	//@}

private:
	time_t m_time;
};

inline CMcTime CMcTime::GetCurrentTime()
{
	time_t Time;
	::time(&Time);
	tm *pTime = gmtime(&Time);
	return (CMcTime(mktime(pTime)));
}

inline CMcTime::CMcTime() : m_time(0)
{
}

inline CMcTime::CMcTime(time_t time) : m_time(time)
{
}

inline CMcTime::CMcTime(int nYear, int nMonth, int nDay, int nHour, int nMin, int nSec)
{
	struct tm Time;

	Time.tm_sec = nSec;
	Time.tm_min = nMin;
	Time.tm_hour = nHour;
	Time.tm_mday = nDay;
	Time.tm_mon = nMonth - 1;        // tm_mon is 0 based
	Time.tm_year = nYear - 1900;     // tm_year is 1900 based
	Time.tm_isdst = 0;

	m_time = mktime(&Time);
}

inline CMcTime& CMcTime::operator=(time_t time)
{
	m_time = time;

	return(*this);
}

inline CMcTime& CMcTime::operator+=(int nSeconds)
{
	m_time += nSeconds;

	return(*this);
}

inline CMcTime& CMcTime::operator-=(int nSeconds)
{
	m_time -= nSeconds;

	return(*this);
}

inline int CMcTime::operator-(CMcTime time) const
{
	return(int(m_time-time.m_time));
}

inline CMcTime CMcTime::operator-(int nSeconds) const
{
	return(CMcTime(m_time-nSeconds));
}

inline CMcTime CMcTime::operator+(int nSeconds) const
{
	return(CMcTime(m_time+nSeconds));
}

inline bool CMcTime::operator==(CMcTime time) const
{
	return(m_time == time.m_time);
}

inline bool CMcTime::operator!=(CMcTime time) const
{
	return(m_time != time.m_time);
}

inline bool CMcTime::operator<(CMcTime time) const
{
	return(m_time < time.m_time);
}

inline bool CMcTime::operator>(CMcTime time) const
{
	return(m_time > time.m_time);
}

inline bool CMcTime::operator<=(CMcTime time) const
{
	return(m_time <= time.m_time);
}

inline bool CMcTime::operator>=(CMcTime time) const
{
	return(m_time >= time.m_time);
}

inline time_t CMcTime::GetTime() const
{
	return m_time;
}

inline int CMcTime::GetYear() const
{ 
	tm *pTime = localtime(&m_time);
	return (pTime ? pTime->tm_year + 1900 : -1); 
}

inline int CMcTime::GetMonth() const
{ 
	tm *pTime = localtime(&m_time);
	return (pTime ? pTime->tm_mon + 1 : -1);
}

inline int CMcTime::GetDay() const
{
	tm *pTime = localtime(&m_time);
	return (pTime ? pTime->tm_mday : -1); 
}

inline int CMcTime::GetHour() const
{
	tm *pTime = localtime(&m_time);
	return (pTime ? pTime->tm_hour : -1); 
}

inline int CMcTime::GetMinute() const
{
	tm *pTime = localtime(&m_time);
	return (pTime ? pTime->tm_min : -1); 
}

inline int CMcTime::GetSecond() const
{ 
	tm *pTime = localtime(&m_time);
	return (pTime ? pTime->tm_sec : -1);
}

inline int CMcTime::GetDayOfWeek() const
{ 
	tm *pTime = localtime(&m_time);
	return (pTime ? pTime->tm_wday + 1 : -1);
}
