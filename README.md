[![Web API](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/actions/workflows/web-api.yml/badge.svg)](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/actions/workflows/web-api.yml)
[![Client Application](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/actions/workflows/client-app.yml/badge.svg)](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/actions/workflows/client-app.yml)

# GeoDistanceCalculator
Example full-stack application demonstrator. It consists of REST API created with .NET and C# and frontend application created with Angular.

![client-app](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/blob/main/showcase/client-app.png)

There are 3 possible ways to start the project:
- [Docker-compose start](https://github.com/rutkowski-tomasz/GeoDistanceCalculator#project-docker-compose-startup) (recommended - single-command start)
- [Development start](https://github.com/rutkowski-tomasz/GeoDistanceCalculator#project-development-startup)
- [Kubernetes start](https://github.com/rutkowski-tomasz/GeoDistanceCalculator#project-kubernetes-startup)

Access the client application at: `http://localhost:4200/`.
Reach the web API swagger at: `https://localhost:7023/swagger/index.html`

- [API base location](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/tree/main/src/Api)
- [Client base location](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/tree/main/src/ClientApp)

## Project requirements
- .NET 6 SDK 6.0.101
- Docker 20.10.11
- Docker-compose 1.29.2
- Angular CLI 11.2.2
- Node.js SDK 14.16.0
- kubectl v1.22.4

## Project development startup
Web API and client application are separate projects and should be started separately:

1. Client application
   1. Navigate to `src/ClientApp/`
   2. Run `npm install` command
   3. Run `npm run start` command
2. Web API
   1. Navigate to `src/Api/`
   2. Run `dotnet run`

## Project docker-compose startup

1. Add temporary SSL cert
```sh
# Windows
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Your_password123
dotnet dev-certs https --trust
# Unix
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p Your_password123
dotnet dev-certs https --trust
```
2. Start docker-compose (`docker-compose up` at root directory)

## Project kubernetes startup

Note: If you only want to start the solution without pushing new images you should omit 2-4 steps from below list

1. Set **DOCKER_REGISTRY** env variable `export DOCKER_REGISTRY=rutkowski/`
2. Login to repository `docker login`
3. Build images `docker-compose build`
4. Push images `docker-compose push`
5. Apply kubernetes configuration `kubectl apply -f ./kubernetes`

Registries: [Web API](https://hub.docker.com/repository/docker/rutkowski/geodistancecalculator-webapi), [Client App](https://hub.docker.com/repository/docker/rutkowski/geodistancecalculator-clientapp)

## Backend package dependencies
- [ApiEndpoints](https://github.com/ardalis/ApiEndpoints)
- [FluentAssertions](https://fluentassertions.com/)
- [ValueOf](https://github.com/mcintyre321/ValueOf)
- [UnitsNet](https://github.com/angularsen/UnitsNet)
- [AutoMapper](https://automapper.org/)

## Frontend package dependencies
- [Angular Material](https://material.angular.io/)
- [Nginx](https://www.nginx.com/)

## Notes
1. Web API and client application are separate projects and can be deployed separately. It would be better to place them in other repositories. They are placed in one repository because of demo purpose.

## Roadmap:

1. ~~Create README.md + roadmap~~
2. ~~Initialize project, install required dependencies~~
3. ~~Define domain models~~
4. ~~Implement logic of distance calculation~~
5. ~~Create frontend application to showcase API endpoint~~
6. ~~Integrate with docker~~
7. ~~Integrate with Github actions~~
8. ~~Implement multiple methods to calculate the distance~~
9. ~~Consider different required units~~
10. ~~Stylize frontend solution (use third party library)~~
11. ~~Validate form before sending the request~~
12. Create BDD tests (Specflow)
13. ~~Prepare solution for kubernetes deployment~~
14. ~~Write starting instructions~~
15. ~~Enable swagger~~
16. ~~Implement mappers~~
17. ~~Integrate with docker-compose for single command project startup~~
18. Generate API client for angular on build
19. Add code coverage tool
20. ~~Add project description~~
21. Add HTTPS support for nginx container
22. Add HTTPS support for kubernetes deployment