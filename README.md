# MVC
This is a demo project to explore the best practices in MVC. 

The solution was inspired from Christo's Sakellarios blog "ASP.NET MVC Solution Architecture – Best Practice"
https://chsakell.com/2015/02/15/asp-net-mvc-solution-architecture-best-practices/ and has been extended to use a more generic version of it

An overview of the application can been seen on the file "SMS Diagram" and can be opened by the web tool www.draw.io

Note: The main solution is the Aurora.SMS.sln
## Overview - Core bussiness requirement
Primary reqirement:An insurance agent wants to notify all his customers when their contracts' are near to expire via SMS. 
Also he wants to notify them when a contract state has been changed like renewed, modified or canceled. The user wants to create SMS templates, select the contracts and send an SMS for every contract by using an SMS template. Last he wants to see the history record for the SMS that he has sent and check its status (delivered or not)

## Technologies & frameworks used:
- MVC 5
- Web API
- EF 6
- [Bogus - fake data generator](https://github.com/bchavez/Bogus)
- Automapper
- Moq
- [Fluent Validation](https://chsakell.com/2015/01/17/web-api-powerful-custom-model-validation-with-fluentvalidation/)
- Memory EF
- [Predicate builder & expresion trees] (http://www.albahari.com/nutshell/predicatebuilder.aspx)


## Architecture & concepts
- Best practices for building an MVC solution
- DTOs: when, why.
- ViewModels: when, why.
- How to create Unit Of work
- How to create a generic repository
- How to create unit tests for
  - WebAPi
  - MVC Controlers
  - EF
  - EF Fluent API EntityTypeConfiguration

## References
- Basic writing and formatting syntax for [GitHub](https://help.github.com/articles/basic-writing-and-formatting-syntax/)
- [Calling a Web API From a .NET Client (C#)](https://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client)
- [Create ForeignKey using fluent API on Property](http://www.entityframeworktutorial.net/code-first/configure-one-to-many-relationship-in-code-first.aspx)
- [Web API powerful Custom Model Validation with FluentValidation](https://chsakell.com/2015/01/17/web-api-powerful-custom-model-validation-with-fluentvalidation/)

## Food for thought
- [Is the Repository pattern useful with Entity Framework?](http://www.thereformedprogrammer.net/is-the-repository-pattern-useful-with-entity-framework/) I've had also this argument in my mind for long time. I decided not to use repositories on my .net Core reimplementation as the dbcontext can use a memory provider and the SQL Lite memory provider for testing

## Notes
- Calling a post method using HttpClient, a strange 404 occured. See my remarks at Aurora.SMS.FakeProvider.Controllers.TestPost
- [Generic Repository and Unit of Work Pattern, Entity Framework, Unit Testing, Autofac IoC Container and ASP.NET MVC](http://techbrij.com/unit-testing-asp-net-mvc-controller-service)


My LinkedIn profile:https://www.linkedin.com/in/sotiris-soto-hatzis-4a578a100
