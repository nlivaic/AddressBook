## Project layout:

Stuff is layered out according to the [Onion architecture](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/) principles.

Domain logic is in AddressBook.Core. This project does not have any dependencies on any of the web and API related stuff. I tried to apply some of the [DDD principles](https://dddcommunity.org/library/vernon_2011/), so I made AddressBook the aggregate root, with Contacts as entities. Telephone number and Address are value objects in DDD nomenclature. Also, repository interface is defined here. Service class is the place where use cases are orchestrated, but I tried to keep logic out of it.

Shared kernel is a place where some basic functionalities are. In this case I kept things simple due to time constraints, so this is where basic definitions of Entity and Value classes are found.

Web project houses the API, Dto, input validation and sanitizer logic. This project is also where the front end is happening: React and Redux were my choice. No styling at this point yet.

Unit tests cover only the domain logic.

## Deployment details:

You have to enter your own database server password. Database connection string is in `appsettings.development.json`. This will suffice for getting started, but even for dev purposes, it would be better to use User Secrets, but I felt that would only complicate things at this point.

Database is migrated and seeded on startup. No additional commands neccessary.

To run the app, you first need to install npm packages.

    cd ./AddressWeb/ClientApp; npm install;

Run the app from solution root folder.

    dotnet run

Just in case you want to migrate manually, execute below from solution root folder:
    
    cd ./AddressBook.Data; dotnet ef --startup-project ..\AddressBook.Web\  database update --context AddressBookDbContext; cd ..

## Stuff left to do:

As far as security goes, nothing is implemented at this point due to time constraints. Identity authentication, authorization and appropriate headers for CSP, Xfo, CSRF and HSTS could be added rather quickly.

SignalR was out of reach, perhaps I got too ambitious with the front end and the whole SPA thing.

Styling :)
