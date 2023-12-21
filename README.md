# TodoAPI-xUnit

## Om projektet
Detta �r ett projekt som �r skapat f�r att testa ett befintligt API med hj�lp av xUnit. Projektet �r skapat i .NET Core och anv�nder sig av Entity Framework Core f�r att kommunicera med en databas. 
Ett simpelt anv�ndargr�nssnitt �r skapat med hj�lp av HTML, CSS & Javascript f�r att l�ta anv�ndaren hantera sina "Todos".

## Setup
F�r att k�ra projektet beh�ver du ha .NET Core installerat p� din dator. Du beh�ver �ven ha en databas som du kan ansluta till. 
I detta projekt anv�nds en SQL Server databas. F�r att �ndra databasen som anv�nds kan du �ndra connection stringen i appsettings.json filen.
Innan du k�r projektet beh�ver du k�ra f�ljande kommando i Package Manager Console:
```
dotnet ef database update
```

Starta sedan projektet genom att trycka p� F5.

## Enhets tester
Enhetstesterna t�cker samtlig logik f�r att hantera todos, och t�cker �ven "edge cases". Testerna anv�nder sig av en `InMemory` databas.
F�ljande enhetstester finns i projektet:
- Testa att en Todo kan skapas
- Testa att en Todo kan uppdateras
- Testa att en Todo kan tas bort
- Testa att Todos kan h�mtas
- Testa att Todos kan h�mtas med en specifik status
- Testa att ta bort alla Todos med f�rdig status
- Testa att v�xa Todos status ("Toggle Todos")


