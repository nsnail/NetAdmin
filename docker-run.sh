docker network create netadmin-net
docker run --network netadmin-net -d --name netadmin-redis redis
docker run --network netadmin-net -d -p 55010:8080 --name netadmin-host nsnail/netadmin