@if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off

:: ----------------------
:: KUDU Deployment Script
:: Version: 1.0.6
:: ----------------------

:: Prerequisites
:: -------------

:: Verify node.js installed
where node 2>nul >nul
IF %ERRORLEVEL% NEQ 0 (
  echo Missing node.js executable, please install node.js, if already installed make sure it can be reached from current environment.
  goto error
)

:: Setup
:: -----

setlocal enabledelayedexpansion

SET ARTIFACTS=%~dp0%..\artifacts

IF NOT DEFINED DEPLOYMENT_SOURCE (
  SET DEPLOYMENT_SOURCE=%~dp0%.
)

IF NOT DEFINED DEPLOYMENT_TARGET (
  SET DEPLOYMENT_TARGET=%ARTIFACTS%\wwwroot
)

IF NOT DEFINED NEXT_MANIFEST_PATH (
  SET NEXT_MANIFEST_PATH=%ARTIFACTS%\manifest

  IF NOT DEFINED PREVIOUS_MANIFEST_PATH (
    SET PREVIOUS_MANIFEST_PATH=%ARTIFACTS%\manifest
  )
)

IF NOT DEFINED KUDU_SYNC_CMD (
  :: Install kudu sync
  echo Installing Kudu Sync
  call npm install kudusync -g --silent
  IF !ERRORLEVEL! NEQ 0 goto error

  :: Locally just running "kuduSync" would also work
  SET KUDU_SYNC_CMD=%appdata%\npm\kuduSync.cmd
)
IF NOT DEFINED DEPLOYMENT_TEMP (
  SET DEPLOYMENT_TEMP=%temp%\___deployTemp%random%
  SET CLEAN_LOCAL_DEPLOYMENT_TEMP=true
)

IF DEFINED CLEAN_LOCAL_DEPLOYMENT_TEMP (
  IF EXIST "%DEPLOYMENT_TEMP%" rd /s /q "%DEPLOYMENT_TEMP%"
  mkdir "%DEPLOYMENT_TEMP%"
)

IF DEFINED MSBUILD_PATH goto MsbuildPathDefined
SET MSBUILD_PATH=%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe
:MsbuildPathDefined

::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:: Deployment
:: ----------

echo Handling .NET Web Application deployment.

:: 1. Restore NuGet packages
IF /I "src\Connect4.sln" NEQ "" (
  call :ExecuteCmd nuget restore "%DEPLOYMENT_SOURCE%\src\Connect4.sln"
  IF !ERRORLEVEL! NEQ 0 goto error
)

:: 2. Build to the temporary path
IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
  call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\src\Connect4.Web\Connect4.Web.csproj" /nologo /verbosity:m /t:Build /t:pipelinePreDeployCopyAllFilesToOneFolder /p:_PackageTempDir="%DEPLOYMENT_TEMP%";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false /p:SolutionDir="%DEPLOYMENT_SOURCE%\src\\" %SCM_BUILD_ARGS%
) ELSE (
  call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\src\Connect4.Web\Connect4.Web.csproj" /nologo /verbosity:m /t:Build /p:AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false /p:SolutionDir="%DEPLOYMENT_SOURCE%\src\\" %SCM_BUILD_ARGS%
)

IF !ERRORLEVEL! NEQ 0 goto error

pushd "%DEPLOYMENT_SOURCE%\src\Connect4.Web"


:: 3. Restore NPM packages
IF /I "packages.json" NEQ "" (  
echo Installing npm packages: Starting %TIME%
call npm install
echo Installing npm packages: Finished %TIME%
IF !ERRORLEVEL! NEQ 0 goto error
)

:: 4. Restore JSPM and Gulp packages and run Gulp tasks
IF /I "gulpfile.js" NEQ "" (  
echo Installing Gulp dependencies: Starting %TIME%
call npm install gulp
echo Installing Gulp dependencies: Finished %TIME%
IF !ERRORLEVEL! NEQ 0 goto error

echo Installing JSPM dependencies: Starting %TIME%
call npm install jspm 
echo Installing JSPM dependencies: Finished %TIME%
IF !ERRORLEVEL! NEQ 0 goto error

echo Running JSPM deployment: Starting %TIME%
call :ExecuteCmd "%DEPLOYMENT_SOURCE%\node_modules\.bin\jspm" -y
echo Running JSPM deployment: Finished %TIME%

echo Running Gulp deployment: Starting %TIME%
call :ExecuteCmd "%DEPLOYMENT_SOURCE%\node_modules\.bin\gulp" build
echo Running Gulp deployment: Finished %TIME%
IF !ERRORLEVEL! NEQ 0 goto error
)

echo Publishing dist folder files to temporary deployment location
call :ExecuteCmd "xcopy" "%DEPLOYMENT_SOURCE%\src\Connect4.Web\dist\*.*" "%DEPLOYMENT_TEMP%\dist" /S /Y /I
call :ExecuteCmd "xcopy" "%DEPLOYMENT_SOURCE%\src\Connect4.Web\node_modules\*.*" "%DEPLOYMENT_TEMP%\node_modules" /S /Y /I
call :ExecuteCmd "xcopy" "%DEPLOYMENT_SOURCE%\src\Connect4.Web\jspm_packages\*.*" "%DEPLOYMENT_TEMP%\jspm_packages" /S /Y /I
echo Done publishing dist folder files to temporary deployment location

popd

:: 5. KuduSync
IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
  call :ExecuteCmd "%KUDU_SYNC_CMD%" -v 50 -f "%DEPLOYMENT_TEMP%" -t "%DEPLOYMENT_TARGET%" -n "%NEXT_MANIFEST_PATH%" -p "%PREVIOUS_MANIFEST_PATH%" -i ".git;.hg;.deployment;deploy.cmd"
  IF !ERRORLEVEL! NEQ 0 goto error
)

::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

:: Post deployment stub
IF DEFINED POST_DEPLOYMENT_ACTION call "%POST_DEPLOYMENT_ACTION%"
IF !ERRORLEVEL! NEQ 0 goto error

goto end

:: Execute command routine that will echo out when error
:ExecuteCmd
setlocal
set _CMD_=%*
call %_CMD_%
if "%ERRORLEVEL%" NEQ "0" echo Failed exitCode=%ERRORLEVEL%, command=%_CMD_%
exit /b %ERRORLEVEL%

:error
endlocal
echo An error has occurred during web site deployment.
call :exitSetErrorLevel
call :exitFromFunction 2>nul

:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
endlocal
echo Finished successfully.
