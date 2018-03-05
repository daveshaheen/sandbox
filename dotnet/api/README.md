# Sandbox - Parking Rates Web API

Pull down the project and if you have docker installed run the following command to build and create the runtime.

    docker build --target build-env -t aspnetcore-builder:2.0 . && docker build --target runtime-env -t rates-api-runtime:latest .

Run the container and navigate to 127.0.0.1:5000 or [::1]:5000.

    docker run -d -p 5000:5000 --name rates-api rates-api-runtime:latest

If you have dotnet installed the run the following from the csproj directory.

    dotnet restore

    dotnet build

    dotnet run

See the swagger documentation for details.
