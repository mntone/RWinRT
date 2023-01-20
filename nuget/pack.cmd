@echo off
REM Thanks to https://stackoverflow.com/a/25850626
setlocal enableextensions disabledelayedexpansion

set "major="
for /f "tokens=3 delims=<>" %%a in (
	'find /i "<RWinRTVersionMajor>" ^< "../src/version.props"'
) do set "major=%%a"

set "minor="
for /f "tokens=3 delims=<>" %%a in (
	'find /i "<RWinRTVersionMinor>" ^< "../src/version.props"'
) do set "minor=%%a"

set "patch="
for /f "tokens=3 delims=<>" %%a in (
	'find /i "<RWinRTVersionPatch>" ^< "../src/version.props"'
) do set "patch=%%a"

set "prerel="
for /f "tokens=3 delims=<>" %%a in (
	'find /i "<RWinRTVersionPreRelease>" ^< "../src/version.props"'
) do set "prerel=%%a"

if not "%prerel%" == "/AngelVersionPreRelease" (
	set version=%major%.%minor%.%patch%-%prerel%
) else (
	set version=%major%.%minor%.%patch%
)

echo Detect R/WinRT version: %version%
nuget pack Mntone.RWinRT.nuspec -Version %version% -OutputDirectory "../BuildNugetPackages/"
