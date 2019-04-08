FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY [".","."]

RUN dotnet restore "DNATrack.Web.Admin/DNATrack.Web.Admin.csproj"
WORKDIR "/src/DNATrack.Web.Admin"
RUN dotnet build "DNATrack.Web.Admin.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DNATrack.Web.Admin.csproj" -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT  ["dotnet", "DNATrack.Web.Admin.dll"]