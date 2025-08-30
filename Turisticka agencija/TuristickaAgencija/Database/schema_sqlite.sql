-- SQLite schema for Turistička Agencija
-- Created: 2025

-- Kreiranje tabele za klijente
CREATE TABLE IF NOT EXISTS Clients (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Ime TEXT NOT NULL,
    Prezime TEXT NOT NULL,
    BrojPasosa TEXT NOT NULL UNIQUE,
    DatumRodjenja TEXT NOT NULL,
    Email TEXT NOT NULL,
    BrojTelefona TEXT NOT NULL,
    DatumRegistracije TEXT DEFAULT CURRENT_TIMESTAMP
);

-- Kreiranje tabele za destinacije
CREATE TABLE IF NOT EXISTS Destinations (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Naziv TEXT NOT NULL,
    Zemlja TEXT NOT NULL,
    Region TEXT,
    Opis TEXT,
    Tip TEXT,
    Popularna INTEGER DEFAULT 0,
    ProsecnaCena REAL,
    Valuta TEXT DEFAULT 'RSD',
    DatumDodavanja TEXT DEFAULT CURRENT_TIMESTAMP,
    Aktivna INTEGER DEFAULT 1
);

-- Kreiranje tabele za pakete
CREATE TABLE IF NOT EXISTS Packages (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Naziv TEXT NOT NULL,
    Cena REAL NOT NULL,
    VrstaPaketa TEXT NOT NULL,
    Opis TEXT,
    Destinacija TEXT NOT NULL,
    TipPrevoza TEXT NOT NULL,
    BrojDana INTEGER DEFAULT 1,
    DatumKreiranja TEXT DEFAULT CURRENT_TIMESTAMP,
    Aktivan INTEGER DEFAULT 1,
    
    -- Specifična polja za AranzmanZaMore
    VrstaSmestaja TEXT,
    ZvezdiceHotela INTEGER,
    UkljucenDorucak INTEGER DEFAULT 0,
    UkljucenRucak INTEGER DEFAULT 0,
    UkljucenVecera INTEGER DEFAULT 0,
    
    -- Specifična polja za AranzmanZaPlanine
    DodatneAktivnosti TEXT,
    VodicUsluge INTEGER DEFAULT 0,
    SkijaskaOprema INTEGER DEFAULT 0,
    
    -- Specifična polja za Ekskurziju
    Vodic TEXT,
    Trajanje INTEGER,
    ObilasciMesta TEXT,
    UkljucenUlaznice INTEGER DEFAULT 0,
    
    -- Specifična polja za Krstarenje
    Brod TEXT,
    Ruta TEXT,
    DatumPolaska TEXT,
    TipKabine TEXT,
    AllInclusive INTEGER DEFAULT 0
);

-- Kreiranje tabele za rezervacije
CREATE TABLE IF NOT EXISTS Reservations (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ClientId INTEGER NOT NULL,
    PackageId INTEGER NOT NULL,
    DatumRezervacije TEXT DEFAULT CURRENT_TIMESTAMP,
    DatumPutovanja TEXT NOT NULL,
    BrojPutnika INTEGER DEFAULT 1,
    UkupnaCena REAL NOT NULL,
    Status TEXT DEFAULT 'Aktivna',
    Napomene TEXT,
    DatumOtkazivanja TEXT,
    RazlogOtkazivanja TEXT,
    
    FOREIGN KEY (ClientId) REFERENCES Clients(Id),
    FOREIGN KEY (PackageId) REFERENCES Packages(Id)
);

-- Kreiranje indeksa za bolje performanse
CREATE INDEX IF NOT EXISTS idx_clients_passport ON Clients(BrojPasosa);
CREATE INDEX IF NOT EXISTS idx_clients_email ON Clients(Email);
CREATE INDEX IF NOT EXISTS idx_reservations_client ON Reservations(ClientId);
CREATE INDEX IF NOT EXISTS idx_reservations_package ON Reservations(PackageId);
CREATE INDEX IF NOT EXISTS idx_reservations_status ON Reservations(Status);
CREATE INDEX IF NOT EXISTS idx_packages_type ON Packages(VrstaPaketa);
CREATE INDEX IF NOT EXISTS idx_packages_destination ON Packages(Destinacija);

-- Trigger za ažuriranje datuma izmene
CREATE TRIGGER IF NOT EXISTS update_client_timestamp 
    AFTER UPDATE ON Clients
BEGIN
    UPDATE Clients SET DatumRegistracije = CURRENT_TIMESTAMP WHERE Id = NEW.Id;
END;

-- Inicijalny podaci za testiranje
INSERT OR IGNORE INTO Destinations (Naziv, Zemlja, Region, Opis, Tip, Popularna) VALUES
('Zlatibor', 'Srbija', 'Zapadna Srbija', 'Planinska destinacija', 'Planine', 1),
('Kopaonik', 'Srbija', 'Centralna Srbija', 'Ski centar', 'Planine', 1),
('Budva', 'Crna Gora', 'Jadran', 'Morska destinacija', 'More', 1),
('Dubrovnik', 'Hrvatska', 'Dalmacija', 'Istorijski grad', 'Grad', 1),
('Pariz', 'Francuska', 'Ile-de-France', 'Grad svetlosti', 'Grad', 1);

INSERT OR IGNORE INTO Packages (Naziv, Cena, VrstaPaketa, Destinacija, TipPrevoza, BrojDana, VrstaSmestaja, ZvezdiceHotela) VALUES
('Zlatibor - Odmor u prirodi', 15000, 'Planine', 'Zlatibor', 'Autobus', 7, 'Hotel', 4),
('Budva - Letovanje', 25000, 'More', 'Budva', 'Autobus', 10, 'Hotel', 3),
('Pariz - Grad ljubavi', 85000, 'Ekskurzija', 'Pariz', 'Avion', 5, 'Hotel', 4);
