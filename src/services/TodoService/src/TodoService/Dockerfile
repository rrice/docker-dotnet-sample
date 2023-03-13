# escape=\

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the source over to the container
# image first.
COPY . /src/

# Get the dependencies from NuGet
RUN dotnet restore

# Build / test / publish the
# service.
RUN dotnet publish -c release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_URLS=http://*:6000
EXPOSE 6000
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TodoService.dll"]
