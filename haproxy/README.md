# README

Modify haproxy.cfg before using. At least change ${ADDRESS}:${PORT} in haproxy.cfg or pass as environment variables.

To build and start the container.

    docker build -t local/haproxy . && docker run -d -p 9000:9000 -p 9001:9001 --name proxy --restart unless-stopped local/haproxy

To stop and remove the container and image.

    docker stop proxy & docker rm proxy & docker rmi local/haproxy
