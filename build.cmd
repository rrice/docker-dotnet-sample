@ECHO OFF

echo Building...
dotnet build --configuration Release --nologo

echo Testing...
dotnet test --configuration Release --no-build --nologo

