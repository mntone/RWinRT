@echo off
set MSBUILD=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\msbuild
set SOLUTION_FILE=../RWinRT.sln
set PROJECTS=RWinRT
set TARGET_PLATFORMS=x86
set TARGET_CONFIGURATION=Release
set CLEAN_AFTER_BUILD=true

goto MAIN

:ERROR
	echo "Error"
	exit /b %!%errorlevel%

:MAIN
	REM Build
	for %%f in (%PROJECTS%) do (
		echo +-------------------------------------------
		echo ^| Build %%f
		echo +-------------------------------------------
		"%MSBUILD%" "%SOLUTION_FILE%" -fl -flp:logfile=msbuild_projection.log -m -t:"%%f":Clean;"%%f":Rebuild -p:Configuration=%TARGET_CONFIGURATION%;Platform=x86
		if errorlevel 1 goto ERROR
		if "%CLEAN_AFTER_BUILD%" == "true" (
			rd /s /q "../obj/%TARGET_CONFIGURATION%/%%f"
		)
	)

	REM Pack NuGet
	echo Pack NuGet package
	./pack.cmd
