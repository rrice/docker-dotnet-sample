# Comprehensive Dockerized .NET 5.0 Microservice Sample

## Motovation 
This is my personal comprehesive dockerized .NET 5.0 application project. I wanted to be
able to create something that is less of "toy" sample and more of a "real world" 
sample. I see a lot of developers get stuck trying to get from the basics to something
more advanced. Even with myself, I find myself wondering if I can be a better 
developer as technology advances.

Some of my (often lofty) goals for this repository would be:

* Evolve an application from a basic monolithic application something you may
  see at a larger scale enterprise.  
* Leverage modern technology framework seen by the typical full-stack developer, 
  and how each would fit in with a .NET 5.0 ecosystem.
* Hopefully tackle some common application concerns outside typical business logic
  such as user security, authentication / authorization, monitoring, etc.

# Current plans:

* Create a typical toy SPA application, like a todo list management, and implement it 
  in various client UI frameworks, such as ReactJS or Vue 3. This is a fairly common
  sample done by others.
* Implement a common API that can be used by each of the client UI frameworks.
* Create an Open Id Connect identity provider to be used to authorize access to
  the common API.   

## Technologies

The following techonologies are used in this sample:

* [.NET 5.0](https://docs.microsoft.com/en-us/dotnet/)
* [Docker](https://www.docker.com/)
* [Cake](https://cakebuild.net) - Build system.
* [Bash](https://www.gnu.org/software/bash/) (POSIX) - Optional bootstrapper
  for Cake.
* [Powershell 7.0 or above](https://docs.microsoft.com/en-us/powershell/)
  (Linux, MacOS or Windows only) - Optional bootstraper for Cake.

## Building Everything

Ideally, it is optimal to have a top-level process to build everything. However,
like the application architecture, the build process will evolve as time goes on.

Therefore, each project will have individual build scripts initially.
 
### Building a project. 
Each project is built using [Cake](https://cakebuild.net) and will have a common
set of target that can be executed.

Within each project, you can call Cake targets directly using `dotnet cake` or
use one of these bootstraping scripts:

* [build.sh](./build.sh) (POSIX)
* [build.ps1](,/build.ps1) (Linux, MacOS or Windows only)

The default target is `Test`.

To build the docker container, execute the Cake target `Docker-Build`:

    ./build.sh --target=Docker-Build
