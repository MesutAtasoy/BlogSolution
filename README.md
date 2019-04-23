# Blog Solution

Blog Solution is simple application which implemented different approaches within each microservice (DDD, CQRS, Simple CRUD). 

## Technologies
- .Net Core 2.2 
- Entity Framework Core 2.2.3
- Mongo
- Mediatr
- Automapper
- Autofac
- FluentValidation
- Ocelot Api Gateway
- Autofac

## Architecture overview
![alt text](https://github.com/MesutAtasoy/BlogSolution/blob/master/Docs/diagram.png)

- **Gateway Api** : The api routes requests to microservices. Ocelot Api gateway is used. 
- **Blog Api** : Simple Crud operations for Blog's post, category. Mediatr is used. It supports commands and queries.
- **Stats Api** : Post's Comments and Favorites Crud Operations.
- **Identity Api** : JWT Authentication and Authorisation is implemented. 

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


## Road Map
- RabbitMQ implemantation
- Logging 
- Monitoring
- Service Registry with Consul
- Health Check

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/MesutAtasoy/BlogSolution/blob/master/LICENSE)
file for details.
