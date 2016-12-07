# MVC
This is a demo project to explore the best practices in MVC. 
The solution was inspired from Christo's Sakellarios blog "ASP.NET MVC Solution Architecture – Best Practice"
https://chsakell.com/2015/02/15/asp-net-mvc-solution-architecture-best-practices/

An overview of the application can been seen on the file "SMS Diagram" and can be opened by the web tool www.draw.io

##Technologies & frameworks used:
- MVC 5
- Web API
- EF 6
- Bogus
- Automapper
- Moq
- [Fluent Validation](https://chsakell.com/2015/01/17/web-api-powerful-custom-model-validation-with-fluentvalidation/)
- Memory EF


##Architecture & concepts
- Best practices for building an MVC solution
- DTOs: when, why.
- ViewModels: when, why.
- How to create Unit Of work
- How to create a generic repository
- How to create unit tests for
  - WebAPi
  - MVC Controlers
  - EF

##References
- Basic writing and formatting syntax for [GitHub](https://help.github.com/articles/basic-writing-and-formatting-syntax/)
- [Calling a Web API From a .NET Client (C#)](https://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client)
- [Create ForeignKey using fluent API on Property](http://www.entityframeworktutorial.net/code-first/configure-one-to-many-relationship-in-code-first.aspx)
- [Web API powerful Custom Model Validation with FluentValidation](https://chsakell.com/2015/01/17/web-api-powerful-custom-model-validation-with-fluentvalidation/)

##Notes
- Calling a post method using HttpClient, a strange 404 occured. See my remarks at Aurora.SMS.FakeProvider.Controllers.TestPost
- [Generic Repository and Unit of Work Pattern, Entity Framework, Unit Testing, Autofac IoC Container and ASP.NET MVC](http://techbrij.com/unit-testing-asp-net-mvc-controller-service)
