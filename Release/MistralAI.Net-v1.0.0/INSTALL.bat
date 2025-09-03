@echo off
echo.
echo ================================================
echo    MistralAI.Net v1.0.0 - Installation Guide
echo ================================================
echo.
echo This package contains the standalone MistralAI.Net library
echo for VB.NET projects targeting multiple .NET frameworks.
echo.
echo INSTALLATION OPTIONS:
echo.
echo 1. DIRECT REFERENCE:
echo    - Copy lib\MistralAI.Net.dll to your project folder
echo    - Copy lib\Newtonsoft.Json.dll to your project folder  
echo    - Add references to both DLLs in your VB.NET project
echo.
echo 2. NUGET PACKAGE:
echo    - Use the included MistralAI.Net.1.0.0.nupkg file
echo    - Install via: Install-Package MistralAI.Net -Source [path-to-this-folder]
echo.
echo SUPPORTED FRAMEWORKS:
echo    - .NET Framework 4.8
echo    - .NET 6.0
echo    - .NET 8.0
echo.
echo QUICK START:
echo    1. Get your API key from https://console.mistral.ai/
echo    2. Initialize: Dim client As New MistralClient("your-api-key")
echo    3. Use any endpoint: client.Chat, client.Models, etc.
echo    4. Don't forget: client.Dispose() when done
echo.
echo DOCUMENTATION:
echo    - Full API docs: docs\MistralAI.Net.xml
echo    - Complete guide: README.md
echo    - Version history: CHANGELOG.md
echo.
echo Need help? Visit: https://github.com/catozan/MistralAI.Net
echo.
echo ================================================
echo Ready to integrate Mistral AI into your VB.NET apps!
echo ================================================
echo.
pause
