FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0 AS runtime-env
WORKDIR /app
EXPOSE 5000
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "App.dll"]

# Run the following command to build and create the runtime
# docker build --target build-env -t app-builder:latest . && docker build --target runtime-env -t app-runtime:latest .

# Run the container
# docker run -d -p 5000:5000 --name app app-runtime:latest
