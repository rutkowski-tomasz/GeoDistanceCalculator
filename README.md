# GeoDistanceCalculator

- [API base location](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/tree/main/src/Api)
- [Client base location](https://github.com/rutkowski-tomasz/GeoDistanceCalculator/tree/main/src/ClientApp)

## Project requirements
- .NET 6 SDK 6.0.101
- Docker 20.10.11
- Angular CLI 11.2.2
- Node.js SDK 14.16.0

## Project development startup
Web API and client application are separate projects and should be started separately:

1. Client application
   1. Navigate to `src/ClientApp/`
   2. Run `npm install` command
   3. Run `npm run start` command
2. Web API
   1. Navigate to `src/Api/`
   2. Run `dotnet run`

## Backend package dependencies
- [ApiEndpoints](https://github.com/ardalis/ApiEndpoints)
- [FluentAssertions](https://fluentassertions.com/)
- [ValueOf](https://github.com/mcintyre321/ValueOf)

## Frontend package dependencies
- TODO

## Notes
1. Web API and client application are separate projects and can be deployed separately. It would be better to place them in other repositories. They are placed in one repository because of demo purpose.

## Roadmap:

1. ~~Create README.md + roadmap~~
2. ~~Initialize project, install required dependencies~~
3. ~~Define domain models~~
4. ~~Implement logic of distance calculation~~
5. ~~Create frontend application to showcase API endpoint~~
6. ~~Integrate with docker~~
7. Integrate with Github actions
8. Implement multiple methods to calculate the distance
9. Consider different required units
10. Stylize frontend solution (use third party library)
11. Validate form before sending the request
12. Create BDD tests (Specflow)
13. Prepare solution for kubernetes deployment
14. Write starting instructions
15. Enable swagger
16. Implement mappers
17. Integrate with docker-compose for single command project startup
