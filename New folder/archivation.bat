rem @echo off

call %~sdp0_props.bat

%NANT% -buildfile:%~sdp0main.build archivation

pause
