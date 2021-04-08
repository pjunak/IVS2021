all:
	run

run:
	.\bin\Release\netcoreapp3.1\win-x64\Calculator.exe

clean:
	del .\Profiling\bin .\Profiling\obj .\Tests\obj .\Tests\bin .\obj\ .\bin\ .\doc\ /q /s
	
help:
	cd Napoveda  & hh.exe ms-its:Napoveda_ver_1_0.chm::instalace.htm

profile:
	.\Profiling\bin\Release\netcoreapp3.1\win-x64\stddev.exe .\Profiling\TestInputs\Input10
	.\Profiling\bin\Release\netcoreapp3.1\win-x64\stddev.exe .\Profiling\TestInputs\Input100
	.\Profiling\bin\Release\netcoreapp3.1\win-x64\stddev.exe .\Profiling\TestInputs\Input1000

doc:
	doxygen.exe

pack:
	cd .. & cd .. & mkdir xcasek01_xhaasp00_xherma33_xjunak01 & cd xcasek01_xhaasp00_xherma33_xjunak01 & mkdir doc install repo
	cd .. & xcopy /E /I /H /R * ..\xcasek01_xhaasp00_xherma33_xjunak01\repo & cd .. & cd xcasek01_xhaasp00_xherma33_xjunak01\repo & del .\doc .\install
	cd .. & xcopy /E /I /H /R doc ..\xcasek01_xhaasp00_xherma33_xjunak01\doc
	cd .. & xcopy /E /I /H /R install ..\xcasek01_xhaasp00_xherma33_xjunak01\install