rem @echo off

rem ����ࠨ���� ��� � �⨫�⠬
call %~sdp0_props.bat

rem ����᪠�� ᡮ��
%NANT% -buildfile:%~sdp0main.build build

pause