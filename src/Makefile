all: run

run:
	.\bin\Release\Calculator.exe

clean:
	if exist .\Profiling\bin rmdir .\Profiling\bin /s /q
	if exist .\Profiling\obj rmdir .\Profiling\obj /s /q
	if exist .\Tests\bin rmdir .\Tests\bin /s /q
	if exist .\Tests\obj rmdir .\Tests\obj /s /q
	if exist .\bin\ rmdir .\bin\ /s /q
	if exist .\obj\ rmdir .\obj\ /s /q
	if exist ..\doc\ rmdir ..\doc\ /s /q
	if exist ..\.vs\ rmdir ..\.vs\ /s /q
	if exist .\.vs\ rmdir .\.vs\ /s /q
	
help:
	cd Napoveda  & hh.exe ms-its:Napoveda_ver_1_0.chm::04_instalace.htm

profile:
	.\Profiling\bin\Release\netcoreapp3.1\win-x64\stddev.exe .\Profiling\TestInputs\Input10
	.\Profiling\bin\Release\netcoreapp3.1\win-x64\stddev.exe .\Profiling\TestInputs\Input100
	.\Profiling\bin\Release\netcoreapp3.1\win-x64\stddev.exe .\Profiling\TestInputs\Input1000

doc:
	doxygen.exe

pack: clean doc
	if exist ..\..\xcasek01_xhaasp00_xherma33_xjunak01 rmdir ..\..\xcasek01_xhaasp00_xherma33_xjunak01 /s /q
	if exist ..\..\xcasek01_xhaasp00_xherma33_xjunak01.zip rmdir ..\..\xcasek01_xhaasp00_xherma33_xjunak01.zip /s /q
	cd .. & cd .. & mkdir xcasek01_xhaasp00_xherma33_xjunak01 & cd xcasek01_xhaasp00_xherma33_xjunak01 & mkdir doc
	cd .. & xcopy /E /I /H /R * ..\xcasek01_xhaasp00_xherma33_xjunak01\repo & cd .. & cd xcasek01_xhaasp00_xherma33_xjunak01\repo & rmdir .\doc .\install /s /q
	cd .. & xcopy /E /I /H /R doc\* ..\xcasek01_xhaasp00_xherma33_xjunak01\doc
	cd .. & xcopy /E /I /H /R install\* ..\xcasek01_xhaasp00_xherma33_xjunak01\install
	cd .. & cd .. & tar.exe -a -c -f xcasek01_xhaasp00_xherma33_xjunak01.zip xcasek01_xhaasp00_xherma33_xjunak01