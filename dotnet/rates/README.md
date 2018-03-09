# Sandbox - Parking Rates Web API

Pull down the project and cd to the App directory.

If you have docker installed run the following command to build and create the runtime.

    docker build --target build-env -t aspnetcore-builder:2.0 . && \
    docker build --target runtime-env -t rates-api-runtime:latest .

Run the container and navigate to 127.0.0.1:5000 or [::1]:5000.

    docker run -d -p 5000:5000 --name rates-api rates-api-runtime:latest

Logs are stored in Docker /app/logs directory on a persistent volume.

If you have dotnet installed the run the following from the App directory.

    dotnet restore

    dotnet build

    dotnet run

Navigate to 127.0.0.1:5000 or [::1]:5000 and see the swagger documentation for details.

To clean up the docker container and images run the following command. Note: this will delete the container, volume, images, and any dangling images on your system.

    docker stop rates-api

    docker rm -v rates-api && docker rmi rates-api-runtime; docker rmi aspnetcore-builder:2.0; \
    docker images | grep -v REPOSITORY | grep none | awk '{print $3}' | xargs -L1 docker rmi

## Todo

  1. Add ability to query logs, either by shipping logs to a service, or just reading and querying them.
  2. Add more tests
  3. Add more documentation.
