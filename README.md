# bS.Sked 2
Open Source .NET Process Management Scheduler.
## Projects structure
#### bS.Sked2.Structure
Contains all the interfaces and base classes implemented in the other projects.
#### bS.Sked2.Models
Implements Models, View Models, Entities, DTOs and the repository pattern.
For repository we use [bs.Data](https://github.com/babbubba/bs.Data "bs.Data") NHibernate based repository project.
#### bS.Sked2.Service
It implements Service Pattern.
#### bS.Sked2.Extensions.Common
It implements the first extension module. This is loaded dinamically.
#### bS.Sked2.Engine
It implement the Engine logic for the jobs execution.
#### bS.Sked2.WebManagementConsole
Front-end based on Angular 8 single page application.
## How it works
Sked 2 is designed to execute Jobs.
The Job is the highest level entity that owns one or more Task and can be launched by a Trigger.
The Task owns one or more modules and one or more elements.
The Element is the lowest level entity and it owns the logic to execut a specific operation.
#### Job
#### Trigger
#### Task
#### Module
#### Element

