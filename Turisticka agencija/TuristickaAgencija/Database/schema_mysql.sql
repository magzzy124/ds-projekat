-- MySQL schema for Turistička Agencija
-- Created: 2025

-- Kreiranje baze ako ne postoji
CREATE DATABASE IF NOT EXISTS turisticka_agencija
    CHARACTER SET utf8mb4 
    COLLATE utf8mb4_unicode_ci;

USE turisticka_agencija;

-- Kreiranje tabele za klijente
CREATE TABLE IF NOT EXISTS Clients (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Ime VARCHAR(50) NOT NULL,
    Prezime VARCHAR(50) NOT NULL,
    BrojPasosa VARCHAR(20) NOT NULL UNIQUE,
    DatumRodjenja DATE NOT NULL,
    Email VARCHAR(100) NOT NULL,
    BrojTelefona VARCHAR(20) NOT NULL,
    DatumRegistracije TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_passport (BrojPasosa),
    INDEX idx_email (Email)
) ENGINE=InnoDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Kreiranje tabele za destinacije
CREATE TABLE IF NOT EXISTS Destinations (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Naziv VARCHAR(100) NOT NULL,
    Zemlja VARCHAR(100) NOT NULL,
    Region VARCHAR(100),
    Opis TEXT,
    Tip VARCHAR(50),
    Popularna BOOLEAN DEFAULT FALSE,
    ProsecnaCena DECIMAL(10,2),
    Valuta VARCHAR(10) DEFAULT 'RSD',
    DatumDodavanja TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Aktivna BOOLEAN DEFAULT TRUE,
    
    INDEX idx_naziv (Naziv),
    INDEX idx_zemlja (Zemlja),
    INDEX idx_tip (Tip)
) ENGINE=InnoDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Kreiranje tabele za pakete
CREATE TABLE IF NOT EXISTS Packages (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Naziv VARCHAR(100) NOT NULL,
    Cena DECIMAL(10,2) NOT NULL,
    VrstaPaketa VARCHAR(50) NOT NULL,
    Opis TEXT,
    Destinacija VARCHAR(100) NOT NULL,
    TipPrevoza VARCHAR(50) NOT NULL,
    BrojDana INT DEFAULT 1,
    DatumKreiranja TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Aktivan BOOLEAN DEFAULT TRUE,
    
    -- Specifična polja za AranzmanZaMore
    VrstaSmestaja VARCHAR(50),
    ZvezdiceHotela INT,
    UkljucenDorucak BOOLEAN DEFAULT FALSE,
    UkljucenRucak BOOLEAN DEFAULT FALSE,
    UkljucenVecera BOOLEAN DEFAULT FALSE,
    
    -- Specifična polja za AranzmanZaPlanine
    DodatneAktivnosti VARCHAR(200),
    VodicUsluge BOOLEAN DEFAULT FALSE,
    SkijaskaOprema BOOLEAN DEFAULT FALSE,
    
    -- Specifična polja za Ekskurziju
    Vodic VARCHAR(100),
    Trajanje INT,
    ObilasciMesta VARCHAR(200),
    UkljucenUlaznice BOOLEAN DEFAULT FALSE,
    
    -- Specifična polja za Krstarenje
    Brod VARCHAR(100),
    Ruta VARCHAR(200),
    DatumPolaska DATE,
    TipKabine VARCHAR(50),
    AllInclusive BOOLEAN DEFAULT FALSE,
    
    INDEX idx_vrsta (VrstaPaketa),
    INDEX idx_destinacija (Destinacija),
    INDEX idx_cena (Cena)
) ENGINE=InnoDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Kreiranje tabele za rezervacije
CREATE TABLE IF NOT EXISTS Reservations (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ClientId INT NOT NULL,
    PackageId INT NOT NULL,
    DatumRezervacije TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    DatumPutovanja DATE NOT NULL,
    BrojPutnika INT DEFAULT 1,
    UkupnaCena DECIMAL(10,2) NOT NULL,
    Status VARCHAR(20) DEFAULT 'Aktivna',
    Napomene TEXT,
    DatumOtkazivanja TIMESTAMP NULL,
    RazlogOtkazivanja VARCHAR(200),
    
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
    FOREIGN KEY (PackageId) REFERENCES Packages(Id) ON DELETE CASCADE,
    
    INDEX idx_client (ClientId),
    INDEX idx_package (PackageId),
    INDEX idx_status (Status),
    INDEX idx_datum_putovanja (DatumPutovanja)
) ENGINE=InnoDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Inicijalny podaci za testiranje
INSERT IGNORE INTO Destinations (Naziv, Zemlja, Region, Opis, Tip, Popularna) VALUES
('Zlatibor', 'Srbija', 'Zapadna Srbija', 'Planinska destinacija sa čistim vazduhom', 'Planine', TRUE),
('Kopaonik', 'Srbija', 'Centralna Srbija', 'Najveći ski centar u Srbiji', 'Planine', TRUE),
('Budva', 'Crna Gora', 'Jadran', 'Najpopularnija morska destinacija', 'More', TRUE),
('Dubrovnik', 'Hrvatska', 'Dalmacija', 'Perla Jadrana - UNESCO baština', 'Grad', TRUE),
('Pariz', 'Francuska', 'Ile-de-France', 'Grad svetlosti i ljubavi', 'Grad', TRUE),
('Santorini', 'Grčka', 'Egejsko more', 'Vulkansko ostrvo sa spektakularnim zalascima', 'More', TRUE),
('Rim', 'Italija', 'Lazio', 'Večni grad sa bogatom istorijom', 'Grad', TRUE);

INSERT IGNORE INTO Packages (Naziv, Cena, VrstaPaketa, Destinacija, TipPrevoza, BrojDana, VrstaSmestaja, ZvezdiceHotela, UkljucenDorucak) VALUES
('Zlatibor - Odmor u prirodi', 15000.00, 'Planine', 'Zlatibor', 'Autobus', 7, 'Hotel', 4, TRUE),
('Budva - Letovanje 2025', 25000.00, 'More', 'Budva', 'Autobus', 10, 'Hotel', 3, TRUE),
('Pariz - Grad ljubavi', 85000.00, 'Ekskurzija', 'Pariz', 'Avion', 5, 'Hotel', 4, TRUE),
('Santorini - Medeni mesec', 120000.00, 'More', 'Santorini', 'Avion', 7, 'Villa', 5, TRUE),
('Kopaonik - Zimska čarolija', 18000.00, 'Planine', 'Kopaonik', 'Autobus', 5, 'Hotel', 4, TRUE),
('Rim - Istorijska tura', 65000.00, 'Ekskurzija', 'Rim', 'Avion', 4, 'Hotel', 3, TRUE);
