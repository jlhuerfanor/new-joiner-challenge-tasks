## Build and deploy to docker

**Build docker image**

```console
foo@bar:~$ docker build -t njc/tasks:latest .
```

**Deploy container**

```console
docker run -d --name=tasks --network="postgres_default" -p 5000:80 \
    -e ConnectionStrings__TaskConnectionString=$TASK_CONNECTION_STRING \
    njc/tasks:latest
```
