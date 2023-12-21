# TodoAPI-xUnit

## Om projektet
Detta är ett projekt som är skapat för att testa ett befintligt API med hjälp av xUnit. Projektet är skapat i .NET Core och använder sig av Entity Framework Core för att kommunicera med en databas. 
Ett simpelt användargränssnitt är skapat med hjälp av HTML, CSS & Javascript för att låta användaren hantera sina "Todos".

## Setup
För att köra projektet behöver du ha .NET Core installerat på din dator. Du behöver även ha en databas som du kan ansluta till. 
I detta projekt används en SQL Server databas. För att ändra databasen som används kan du ändra connection stringen i appsettings.json filen.
Innan du kör projektet behöver du köra följande kommando i Package Manager Console:
```
dotnet ef database update
```

Starta sedan projektet genom att trycka på F5.

## Enhets tester
Enhetstesterna täcker samtlig logik för att hantera todos, och täcker även "edge cases". Testerna använder sig av en `InMemory` databas.
Följande enhetstester finns i projektet:
- Testa att en Todo kan skapas
- Testa att en Todo kan uppdateras
- Testa att en Todo kan tas bort
- Testa att Todos kan hämtas
- Testa att Todos kan hämtas med en specifik status
- Testa att ta bort alla Todos med färdig status
- Testa att växa Todos status ("Toggle Todos")


