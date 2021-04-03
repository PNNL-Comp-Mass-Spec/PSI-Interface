@echo off

pushd Debug

CV_Generator.exe

popd

"C:\Program Files\Beyond Compare 4\BCompare.exe" Debug ..\..\PSI_Interface\CV

pause
