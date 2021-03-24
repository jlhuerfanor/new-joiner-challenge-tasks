FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore


RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80

ENV ConnectionStrings__TaskConnectionString="Server=ec2-54-161-239-198.compute-1.amazonaws.com;Port=5432;Database=d85ivgo3l2gtdr;Username=rvfwarqrbocceh;Password=8f92023983dded18375282c04299e9d1bff6bdd72a2ce610c28585ff767387ea;sslmode=Require;Trust Server Certificate=true;"

COPY --from=build-env /app/Tasks.Application/bin/Release/netcoreapp3.1 .
# ENTRYPOINT ["dotnet", "Tasks.Application.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Tasks.Application.dll