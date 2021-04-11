### Estágio 1 - Obter o source e gerar o Build ###
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dotnet-builder
WORKDIR /app
COPY . /app
RUN dotnet restore TCC.INSPECAO.Api/TCC.INSPECAO.Api.csproj
RUN dotnet publish TCC.INSPECAO.Api/TCC.INSPECAO.Api.csproj -c Release -o /app/publish 

### Estágio 2 - Subir a aplicação através dos binários ###
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=dotnet-builder /app/publish .
ENTRYPOINT ["dotnet", "TCC.INSPECAO.Api.dll"]