var wrapper = WScript.CreateObject("PowerWrapperCom.PowerWrapperCom");

var result = wrapper.GetLastSleepTime();
WScript.Echo("last sleep time: " + result);
WScript.Echo(result);
WScript.Echo("!!!");
