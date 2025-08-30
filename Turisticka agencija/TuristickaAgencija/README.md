# Turistička Agencija - .NET Windows Forms Aplikacija

## Opis projekta

Ova aplikacija je kreirana za upravljanje turističkim aranžmanima u turističkim agencijama. Aplikacija koristi .NET Windows Forms i podržava rad sa SQLite i MySQL bazama podataka.

## Struktura projekta

```
TuristickaAgencija/
├── Models/                 # Model klase
│   ├── Client.cs          # Model za klijente
│   ├── Package.cs         # Bazna klasa i nasleđene klase za pakete
│   ├── Reservation.cs     # Model za rezervacije
│   └── Destination.cs     # Model za destinacije
├── DataAccess/            # Data Access Layer
│   ├── IRepository.cs     # Generic Repository interface
│   ├── BaseRepository.cs  # Bazna implementacija Repository-ja
│   ├── IDatabaseConnection.cs  # Interface za database konekcije
│   ├── SQLiteConnection.cs     # SQLite implementacija
│   └── MySQLConnection.cs      # MySQL implementacija
├── Patterns/              # Dizajn paternи
│   ├── ConfigurationManager.cs   # Singleton za konfiguraciju
│   ├── DatabaseConnectionFactory.cs  # Factory za baze
│   ├── IObserver.cs       # Observer pattern interfaces
│   └── NotificationService.cs     # Observer pattern service
├── Forms/                 # Windows Forms
│   ├── ClientForm.cs      # Forma za upravljanje klijentima
│   ├── ClientForm.Designer.cs
│   ├── AddClientForm.cs   # Forma za dodavanje/izmenu klijenta
│   └── AddClientForm.Designer.cs
├── Utils/                 # Pomoćne klase
├── Config/                # Konfiguracije
├── Database/              # SQL skripte
│   ├── schema_sqlite.sql  # SQLite schema
│   └── schema_mysql.sql   # MySQL schema
├── config.txt             # Konfiguracijski fajl
└── App.config             # .NET konfiguracija
```

## Implementirani dizajn paternи

### Creational Patterns
1. **Singleton** - `ConfigurationManager` i `NotificationService`
2. **Factory** - `DatabaseConnectionFactory` za kreiranje konekcija

### Structural Patterns
1. **Bridge** - `IDatabaseConnection` omogućava rad sa različitim tipovima baza
2. **Repository** - `IRepository<T>` i `BaseRepository<T>` za data access

### Behavioral Patterns
1. **Observer** - `IObserver<T>` i `ISubject<T>` za notifikacije
2. **Strategy** - Različite implementacije database konekcija

## Funkcionalnosti

### Trenutno implementirane:
- ✅ Osnovne model klase (Client, Package, Reservation, Destination)
- ✅ Data Access Layer sa podrškom za SQLite i MySQL
- ✅ Singleton pattern za konfiguraciju
- ✅ Factory pattern za database konekcije
- ✅ Observer pattern za notifikacije
- ✅ Osnovna Windows Forms struktura
- ✅ Forma za upravljanje klijentima
- ✅ Forma za dodavanje/izmenu klijenata
- ✅ SQL skripte za kreiranje baze

### Planowane funkcionalnosti:
- 🔄 Repository implementacije za sve modele
- 🔄 Forme za upravljanje paketima
- 🔄 Forme za upravljanje rezervacijama
- 🔄 Pretraga i filtriranje
- 🔄 Backup i restore funkcionalnosti
- 🔄 Validacija podataka
- 🔄 Izvoz u Excel/PDF

## Konfiguracija

Aplikacija čita konfiguraciju iz `config.txt` fajla u sledećem formatu:
```
Naziv Agencije
Connection String
Ključ1=Vrednost1
Ključ2=Vrednost2
```

Primer:
```
Turistička Agencija "Sunce i More"
Data Source=turisticka_agencija.db;Version=3;
BackupInterval=24
MaxReservations=1000
Currency=RSD
```

## Tipovi paketa

Aplikacija podržava sledeće tipove turističkih paketa:

1. **Aranžman za more** - destinacija, tip smeštaja, tip prevoza
2. **Aranžman za planine** - destinacija, tip smeštaja, dodatne aktivnosti
3. **Ekskurzije** - destinacija, tip prevoza, vodič
4. **Krstarenja** - brod, ruta, datum polaska

## Pokretanje aplikacije

1. Otvorite solution u Visual Studio
2. Restoreujte NuGet pakete
3. Konfigurirate connection string u `config.txt`
4. Pokrenite aplikaciju

## Baze podataka

### SQLite (default)
Connection string: `Data Source=turisticka_agencija.db;Version=3;`

### MySQL
Connection string: `Server=localhost;Database=turisticka_agencija;Uid=username;Pwd=password;`

## Dependencies

- .NET Framework 4.8+
- System.Data.SQLite
- MySql.Data
- System.Configuration.ConfigurationManager

## Autor

Kreiran za DS projekat - Turistička agencija aplikacija
