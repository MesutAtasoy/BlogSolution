# Blog Solution

Blog Solution is container based application which implemented different approaches within each microservice (DDD, CQRS, Simple CRUD). 

## Technologies
- .Net Core 2.2 
- Entity Framework Core 2.2.3
- Mongo
- Mediatr
- Automapper
- Autofac
- FluentValidation
- Ocelot Api Gateway
- RabbitMQ
- Consul 

## Architecture overview
![alt text](https://github.com/MesutAtasoy/BlogSolution/blob/Develop/Docs/Architecture%20.png)

- **Gateway Api** : The api routes requests to microservices. Ocelot Api gateway is used. 
- **Blog Api** : Simple Crud operations for Blog's post, category. Mediatr is used. It supports commands and queries.
- **Stats Api** : Post's Comments and Favorites Crud Operations.
- **Identity Api** : JWT Authentication and Authorisation is implemented. 
- **Notification Api** : Mail service.

## Get Started

Use these instructions to run the project.

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)

### Setup
1. Clone Repository with following code 
```
git clone https://github.com/MesutAtasoy/BlogSolution.git
```

2. At the root directory, build docker containers.
```
docker-compose build
```

3. Run containers in background
```
docker-compose up -d
```
Launch Gateway Api with http://localhost:5000 


## Project Settings

```
"app": {
    "name": "identity-api",
    "useCustomizationData": false,
    "applyDbMigrations": true
  },
```

**applyDbMigrations** : If sets true, All migrations are applied to database.

**useCustomizationData** : If sets true, Custom initial data is added to database.

## Service Discovery
![alt text](https://github.com/MesutAtasoy/BlogSolution/blob/master/Docs/consul-service-discovery.PNG)

1. At the root directory, run the docker container.

```
docker-compose -f docker-compose-consul.yml up -d
```

```
 "serviceDiscovery": {
    "enabled": true,
    "serviceName": "identity-api",
    "healthCheckTemplates": [],
    "endpoints": [],
    "consul": {
      "httpEndpoint": "http://consul:8500",
      "port": "5001",
      "address": "localhost"
    }
  },
```
**enabled** : If sets true, The service is added to service discovery.

**httpEndpoint** : Consul's Url

**address** : Application's Url

**port** : Application's Port

## Road Map
- Logging 
- Monitoring
- UI implemantation

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/MesutAtasoy/BlogSolution/blob/master/LICENSE)
file for details.
