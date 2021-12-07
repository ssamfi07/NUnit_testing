@ECHO OFF  

cd C:\Users\sstef\Documents\Faculta\tas\tema\NUnit_testing

ECHO ========================================================
ECHO build the program and run the tests with coverity enabled
ECHO =========================================================
dotnet test --collect:"XPlat Code Coverage"  

ECHO =========================================================
ECHO generate the HTML file report
ECHO =========================================================
reportgenerator "-reports:C:\Users\sstef\Documents\Faculta\tas\tema\NUnit_testing\Account.Tests\TestResults\c6b344d5-971e-45ff-a6d7-3cd223a7b902\coverage.cobertura.xml" "-targetdir:CoverageReport" -reporttypes:Html

start chrome C:\Users\sstef\Documents\Faculta\tas\tema\NUnit_testing\CoverageReport\index.html

PAUSE