FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FakeUserGenerator.MVC/FakeUserGenerator.MVC.csproj", "FakeUserGenerator.MVC/"]
COPY ["FakeUserGenerator.Application/FakeUserGenerator.Application.csproj", "FakeUserGenerator.Application/"]
COPY ["FakeUserGenerator.Domain/FakeUserGenerator.Domain.csproj", "FakeUserGenerator.Domain/"]
COPY ["FakeUserGenerator.Persistence/FakeUserGenerator.Persistence.csproj", "FakeUserGenerator.Persistence/"]
RUN dotnet restore "FakeUserGenerator.MVC/FakeUserGenerator.MVC.csproj"
RUN dotnet restore "FakeUserGenerator.Application/FakeUserGenerator.Application.csproj"
RUN dotnet restore "FakeUserGenerator.Domain/FakeUserGenerator.Domain.csproj"
RUN dotnet restore "FakeUserGenerator.Persistence/FakeUserGenerator.Persistence.csproj"
COPY . .
WORKDIR "/src/FakeUserGenerator.MVC"
RUN dotnet build "FakeUserGenerator.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FakeUserGenerator.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeUserGenerator.MVC.dll"]
