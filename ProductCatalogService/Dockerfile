FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ProductCatalogService.csproj", ""]
RUN dotnet restore "ProductCatalogService.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ProductCatalogService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductCatalogService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductCatalogService.dll"]