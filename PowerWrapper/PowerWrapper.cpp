// PowerWrapper.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "powerbase.h"

__declspec(dllexport)
double Add(double arg1, double arg2)
{
	return arg1 + arg2;
}

ULONGLONG GetLastSleepTime()
{
	ULONGLONG result = 0;
	ULONG length = 0;
	CallNtPowerInformation(LastSleepTime, NULL, 0, &result, length);
	return result;
}

ULONGLONG GetLastWakeTime()
{
	ULONGLONG result = 0;
	ULONG length = 0;
	CallNtPowerInformation(LastWakeTime, NULL, 0, &result, length);
	return result;
}

SYSTEM_BATTERY_STATE GetSystemBatteryState()
{
	ULONG length = 0;
	SYSTEM_BATTERY_STATE batteryState;
	CallNtPowerInformation(SystemBatteryState, NULL, 0, &batteryState, length);
	return batteryState;
}
