#!/usr/bin/env bash
set -euo pipefail

pushd ./src/MySimpleService

echo "${0##*/}": Building Docker Image

docker build -t docker-dotnet-5.0-sample:latest .

popd

