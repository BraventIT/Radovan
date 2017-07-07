@echo off
del *.nupkg
@echo Primero vamos a generar el paquete. Recueda cambiar la variable 'version' de este script
pause
cls
nuget.exe pack Radovan.Plugin.nuspec -Properties version=1.0.0.7

@echo Comprueba que se ha generado correctamente porque vamos a subirlo a nuestro servidor!!!
pause
nuget.exe push Radovan.Plugin.*.nupkg 2aebff46-0995-4acc-9f7a-48261c6c5e96 -Source http://213.37.1.153:5151