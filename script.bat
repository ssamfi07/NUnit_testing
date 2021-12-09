@ECHO OFF  

cd C:\Users\sstef\Documents\Faculta\tas\tema\NUnit_testing

for /F "delims=" %%a in ('findstr /c:test list.txt') do set var=%%a

echo var

@REM ECHO ========================================================
@REM ECHO build the program and run the tests with coverity enabled
@REM ECHO =========================================================
@REM dotnet test --collect:"XPlat Code Coverage"  

@REM ECHO =========================================================
@REM ECHO generate the HTML file report
@REM ECHO =========================================================
@REM reportgenerator "-reports:C:\Users\sstef\Documents\Faculta\tas\tema\NUnit_testing\Account.Tests\TestResults\c6b344d5-971e-45ff-a6d7-3cd223a7b902\coverage.cobertura.xml" "-targetdir:CoverageReport" -reporttypes:Html

@REM start chrome C:\Users\sstef\Documents\Faculta\tas\tema\NUnit_testing\CoverageReport\index.html

PAUSE