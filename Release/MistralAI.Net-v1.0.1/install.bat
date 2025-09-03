@echo off
echo Installing MistralAI.Net Library...
echo.

REM Check if destination directory exists
if not exist "%~dp0lib\" (
    echo Error: lib directory not found!
    pause
    exit /b 1
)

REM Display available DLL files
echo Available library files:
dir /b "%~dp0lib\MistralAI.Net.dll" 2>nul
if errorlevel 1 (
    echo Error: MistralAI.Net.dll not found in lib directory!
    pause
    exit /b 1
)

echo.
echo Installation Options:
echo 1. Copy DLL to your project directory
echo 2. Add reference in Visual Studio
echo 3. View documentation
echo.

choice /c 123 /n /m "Select option (1-3): "

if errorlevel 3 goto :documentation
if errorlevel 2 goto :reference
if errorlevel 1 goto :copy

:copy
set /p projectPath="Enter your project directory path: "
if not exist "%projectPath%" (
    echo Error: Project directory does not exist!
    pause
    exit /b 1
)

copy "%~dp0lib\MistralAI.Net.dll" "%projectPath%\" >nul
if errorlevel 1 (
    echo Error copying file!
    pause
    exit /b 1
)

echo.
echo SUCCESS: MistralAI.Net.dll copied to %projectPath%
echo Don't forget to add a reference to this DLL in your project!
goto :end

:reference
echo.
echo To add reference in Visual Studio:
echo 1. Right-click References in Solution Explorer
echo 2. Select "Add Reference..."
echo 3. Click "Browse..." button
echo 4. Navigate to: %~dp0lib\
echo 5. Select MistralAI.Net.dll and click OK
echo.
echo For .NET Core/5+ projects, you can also use:
echo ^<Reference Include="MistralAI.Net"^>
echo   ^<HintPath^>path\to\MistralAI.Net.dll^</HintPath^>
echo ^</Reference^>
goto :end

:documentation
echo.
echo Opening documentation...
if exist "%~dp0README.md" (
    start notepad.exe "%~dp0README.md"
) else (
    echo README.md not found!
)
goto :end

:end
echo.
echo Installation process completed.
pause
