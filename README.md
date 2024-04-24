# MMORPG

Project started as simple RPG Kata evolved into full functional real time MMORPG server

Original Kata specification:
https://www.slideshare.net/DanielOjedaLoisel/rpg-combat-kata


Porting into c# .net from https://github.com/andreacrispo/simple-rpg-kata


## Technologies

* [.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr)
* [NUnit](https://nunit.org/)
* [Moq](https://github.com/moq)

## Script


```
dotnet ef migrations add "Init" --project .\src\MMORPG.Infrastructure --startup-project .\src\MMORPG.Api --output-dir Data\Migrations
```

```
dotnet ef database update --project .\src\MMORPG.Infrastructure --startup-project .\src\MMORPG.Api
```