rem @echo off

rem настраиваем пути к утилитам
call %~sdp0_props.bat

rem запускаем сборку
%NANT% -buildfile:%~sdp0main.build build

pause