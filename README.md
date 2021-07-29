# Simple Dockerized .NET 5.0 Microservice Sample

This is just a simple Dockerized .NET 5.0 microservice project.

## Technologies

The following techonologies are used in this sample:

* [.NET 5.0](https://docs.microsoft.com/en-us/dotnet/)
* [Docker](https://www.docker.com/)
* [Cake](https://cakebuild.net) - Build system.
* [Bash](https://www.gnu.org/software/bash/) (POSIX) - Optional bootstrapper
  for Cake.
* [Powershell 7.0 or above](https://docs.microsoft.com/en-us/powershell/)
  (Linux, MacOS or Windows only) - Optional bootstraper for Cake.

## Building the project only

This project is built using [Cake](https://cakebuild.net). You can call
Cake targets directly using `dotnet cake` or use one of these bootstraping scripts:

* [build.sh](./build.sh) (POSIX)
* [build.ps1](,/build.ps1) (Linux, MacOS or Windows only)

The default target is `Test`.

## Building the Docker container image

To build the docker container, execute the Cake target `Docker-Build`:

    ./build.sh --target=Docker-Build
