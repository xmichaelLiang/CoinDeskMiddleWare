# Stage 1: Build and Test the Application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file and restore dependencies
COPY CoinDeskMiddleWare.sln ./
COPY CoinDeskMiddleWareAPI/*.csproj ./CoinDeskMiddleWareAPI/
COPY Utility/*.csproj ./Utility/
COPY CoinDeskMiddleWareAPI.Tests/*.csproj ./CoinDeskMiddleWareAPI.Tests/
RUN dotnet restore

# Copy remaining source files
COPY CoinDeskMiddleWareAPI ./CoinDeskMiddleWareAPI
COPY Utility ./Utility
COPY CoinDeskMiddleWareAPI.Tests ./CoinDeskMiddleWareAPI.Tests

# Build the solution
WORKDIR /app/CoinDeskMiddleWareAPI
RUN dotnet build --configuration Release

# Run unit tests
WORKDIR /app/CoinDeskMiddleWareAPI.Tests
RUN dotnet test --configuration Release --verbosity normal

# Publish the main application
WORKDIR /app/CoinDeskMiddleWareAPI
RUN dotnet publish -c Release -o out

# Stage 2: Prepare Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/CoinDeskMiddleWareAPI/out ./

# Expose port 80
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "CoinDeskMiddleWareAPI.dll"]
