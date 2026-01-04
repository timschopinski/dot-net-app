FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FilmwebApp/FilmwebApp.csproj", "FilmwebApp/"]
RUN dotnet restore "FilmwebApp/FilmwebApp.csproj"

COPY . .
WORKDIR "/src/FilmwebApp"
RUN dotnet build "FilmwebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmwebApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmwebApp.dll"]
