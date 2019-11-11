# bS.Sked 2
Open Source .NET Process Management Scheduler.

[TOCM]

[TOC]

## Projects
### Structure
Contains all the interfaces implemented in the other projects.
### Models
Implements Models, View Models, Entities, DTOs and the repository pattern.
For repository we use [bs.Data](https://github.com/babbubba/bs.Data "bs.Data") NHibernate based repository project.
### CompositionRoot
It implements the Composition Root pattern (usefull in unit testing).
### Service
It implements Service Pattern.
### CommonModule
It implements the base extension module.
### Engine
It implement the Engine logic for the elements execution.
### Main
It exposes the web api functionality.
