@ECHO OFF

pushd .\src\MySimpleService

echo Building Docker Image

docker build -t docker-dotnet-5.0-sample:latest .

popd
