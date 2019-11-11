# bS.Sked 2
Open Source .NET Process Management Scheduler.
## Projects structure
#### bS.Sked2.Structure
Contains all the interfaces implemented in the other projects.
#### bS.Sked2.Models
Implements Models, View Models, Entities, DTOs and the repository pattern.
For repository we use [bs.Data](https://github.com/babbubba/bs.Data "bs.Data") NHibernate based repository project.
#### bS.Sked2.CompositionRoot
It implements the Composition Root pattern (usefull in unit testing).
#### bS.Sked2.Service
It implements Service Pattern.
#### bS.Sked2.Extensions.Common
It implements the base extension module.
#### bS.Sked2.Engine
It implement the Engine logic for the elements execution.
#### bS.Sked2.Main
It exposes the web api functionality.