FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Vladrega.LadyMariaBot/Vladrega.LadyMariaBot.csproj", "Vladrega.LadyMariaBot/"]
COPY ["Vladrega.LadyMariaBot.Core/Vladrega.LadyMariaBot.Core.csproj", "Vladrega.LadyMariaBot.Core/"]
RUN dotnet restore "Vladrega.LadyMariaBot/Vladrega.LadyMariaBot.csproj"
COPY . .
WORKDIR "/src/Vladrega.LadyMariaBot"
RUN dotnet build "Vladrega.LadyMariaBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vladrega.LadyMariaBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vladrega.LadyMariaBot.dll"]
