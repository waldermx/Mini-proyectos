# Compila engine.c para Linux (.so) y Windows (.dll) usando los compiladores de WSL/Ubuntu,
# y deja los binarios en el proyecto LibraryImport.
$ErrorActionPreference = 'Stop'

# Ruta de esta carpeta traducida a formato WSL (/mnt/d/...)
$dir = wsl -d Ubuntu -e wslpath -a "$PSScriptRoot"

wsl -d Ubuntu -e bash -c "set -e
cd '$dir'
gcc -shared -fPIC -O3 engine.c -o ../LibraryImport/libengine.so -lm
x86_64-w64-mingw32-gcc -shared -O3 engine.c -o ../LibraryImport/engine.dll
ls -l ../LibraryImport/libengine.so ../LibraryImport/engine.dll"

if ($LASTEXITCODE -ne 0) { throw "Fallo la compilacion (exit $LASTEXITCODE)" }
Write-Host "OK: libengine.so y engine.dll generados en LibraryImport" -ForegroundColor Green
