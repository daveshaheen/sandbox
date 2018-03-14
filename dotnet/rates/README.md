# Sandbox - Parking Rates Web API

This is a dotnet core 2.0 mvc web api project. It's using Swagger for documentation and Serilog for logging. It can run in a docker container and can ship logs to Elasticsearch with Kibana.

## Getting Started

Pull down the project and cd to the App directory.

If you have docker installed run the following command to build and create the runtime.

    docker build --target build-env -t aspnetcore-builder:2.0 . && \
    docker build --target runtime-env -t rates-api-runtime:latest .

Run the container and navigate to 127.0.0.1:5000 or [::1]:5000. Logs are stored in Docker /app/logs directory on a persistent volume. If you would like logs to be sent to Elasticsearch pass the environment variable DOTNET_ELASTICSEARCH_URL=http://rates-elastic:9200. If you need to setup Elasticsearch and Kibana skip down below.

    docker run -d -p 5000:5000 --name rates-api rates-api-runtime:latest

If you have dotnet core 2.0 installed the run the following commands from the App directory.

    dotnet restore
    dotnet build
    dotnet run

Navigate to 127.0.0.1:5000 or [::1]:5000 and see the swagger documentation for details.

If you'd like to run the container on a different url and port you can do so with the environment variable DOTNET_URLS=http://*:5000 or by running:

    dotnet run --urls http://*:5000

Run the following commands to clean up the docker container and the custom built images.

*Note: This will delete the container, volume, images, and any dangling images on your system.*

    docker stop rates-api
    docker rm -v rates-api; \
    docker rmi rates-api-runtime; \
    docker rmi aspnetcore-builder:2.0; \
    docker images | grep -v REPOSITORY | grep none | awk '{print $3}' | xargs -L1 docker rmi

## Elasticsearch and Kibana

If you want to use Elasticsearch and Kibana for logs then create a network and setup the following docker containers.

*Note: These are the open source images not running x-pack and are meant for dev or test use.*

    docker network create -d bridge rates-net

    docker run -d -p 9200:9200 --name rates-elastic --network rates-net -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch-oss:6.2.2

    docker run -d -p 5601:5601 --name rates-kibana --network rates-net -e ELASTICSEARCH_URL=http://rates-elastic:9200 docker.elastic.co/kibana/kibana-oss:6.2.2

    docker run -d -p 5000:5000 --name rates-api --network rates-net -e DOTNET_ELASTICSEARCH_URL=http://rates-elastic:9200 rates-api-runtime:latest

For basic Kibana setup, navigate to http://127.0.0.1:5601 and add the index pattern logstash-* to Kibana and set by @timestamp.

Elasticsearch documentation: https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html

Kibana documentation: https://www.elastic.co/guide/en/kibana/current/_configuring_kibana_on_docker.html

Run the following commands to clean up the docker containers and images.

    docker stop rates-api; \
    docker stop rates-kibana; \
    docker stop rates-elastic; \
    docker rm -v rates-api; \
    docker rm -v rates-kibana; \
    docker rm -v rates-elastic; \
    docker network rm rates-net; \
    docker rmi rates-api-runtime; \
    docker rmi aspnetcore-builder:2.0; \
    docker images | grep -v REPOSITORY | grep none | awk '{print $3}' | xargs -L1 docker rmi

### Enjoy!

## Todo

  1. Protobuf could use some polish
  2. Logging could be added inside classes
  3. Add more tests
  4. Add more documentation
  5. Add bash script to setup and tear down
