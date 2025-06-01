FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app

COPY  MDDPlatform.DomainModels.Api/app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "MDDPlatform.DomainModels.Api.dll"]