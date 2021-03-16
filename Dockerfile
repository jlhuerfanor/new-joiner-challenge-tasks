FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore


RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/Tasks.Application/bin/Release/netcoreapp3.1 .
ENTRYPOINT ["dotnet", "Tasks.Application.dll"]