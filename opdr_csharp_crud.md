**Opdracht: CRUD-systeem met Persoonsgegevens, Login en Gebruikersregistratie in C# WinForms**

**Versie 1: CRUD met CSV-bestand en Login Functionaliteit**

**Doel:**  
Bouw een eenvoudig CRUD-systeem in C# WinForms met persoonsgegevensopslag in een CSV-bestand. Breid dit uit met login functionaliteit en gebruikersregistratie. Alleen een beheerder (admin) mag nieuwe gebruikers aanmaken en beheren.

### Functionaliteiten:

1. **Login Systeem:**  
   Bij het opstarten van de applicatie moet een inlogscherm verschijnen. De gebruikersnaam en het wachtwoord worden gecontroleerd via een CSV-bestand waarin gebruikersgegevens (gebruikersnaam, wachtwoord, rol) zijn opgeslagen.  
   - **Admin:** Heeft toegang tot gebruikersbeheer en kan nieuwe gebruikers aanmaken, bewerken en verwijderen.
   - **Standaard Gebruiker:** Heeft alleen toegang tot de persoonsgegevens-functionaliteit.

2. **Registratie (Alleen voor Admins):**  
   Admin-gebruikers kunnen nieuwe gebruikers registreren door een formulier in te vullen met gebruikersnaam, wachtwoord en rol (admin of standaard gebruiker). De nieuwe gebruikers worden opgeslagen in het CSV-bestand. 
   
3. **Gebruikersbeheer (Alleen voor Admins):**  
   Admins kunnen bestaande gebruikersgegevens bewerken of verwijderen via een apart formulier. Dit formulier toont alle geregistreerde gebruikers, behalve de admin zelf. Deze gegevens worden opgeslagen in hetzelfde CSV-bestand als de login-gegevens.

4. **CRUD Persoonsgegevens:**  
   - **Create (Toevoegen):** Gebruikers kunnen via een formulier persoonsgegevens invoeren. De gegevens worden opgeslagen in een CSV-bestand.
   - **Read (Bekijken):** Gebruikers kunnen persoonsgegevens inzien, met knoppen voor "Vorige" en "Volgende" om door de gegevens te bladeren.
   - **Update (Bewerken):** Gebruikers kunnen bestaande gegevens bewerken.
   - **Delete (Verwijderen):** Gebruikers kunnen persoonsgegevens verwijderen.
   
5. **Opslag in CSV-bestanden:**  
   - Eén CSV-bestand voor **gebruikers** met velden voor gebruikersnaam, wachtwoord (versleuteld), en rol.
   - Eén CSV-bestand voor **persoonsgegevens** met de benodigde velden zoals naam, geboortedatum, e-mailadres, etc.

### Aanwijzingen:
- Bij het inloggen wordt de rol van de gebruiker (admin of standaard) gecontroleerd om te bepalen welke functies beschikbaar zijn.
- Zorg ervoor dat wachtwoorden veilig worden opgeslagen, bijvoorbeeld door hashing te gebruiken.
- Gebruik aparte formulieren voor login, gebruikersbeheer en persoonsgegevensbeheer.

---

**Versie 2: CRUD met MySQL Database, Login Functionaliteit en ASP.NET Core API**

**Doel:**  
Breid de eerste versie uit door de persoonsgegevens- en gebruikersgegevens op te slaan in een MySQL-database. Voeg een ASP.NET Core API toe voor de communicatie tussen de WinForms-applicatie en de database. Het systeem moet login-functionaliteit en gebruikersbeheer behouden, waarbij alleen admins nieuwe gebruikers kunnen aanmaken of beheren.

### Functionaliteiten:

1. **Login Systeem:**  
   Het inlogscherm controleert de gebruikersnaam en het wachtwoord via de API, die verbinding maakt met de MySQL-database. Gebruikersnamen, wachtwoorden (versleuteld), en rollen worden in de database opgeslagen.
   - **Admin:** Kan nieuwe gebruikers aanmaken, bewerken, en verwijderen.
   - **Standaard Gebruiker:** Heeft alleen toegang tot het beheren van persoonsgegevens.

2. **Registratie (Alleen voor Admins):**  
   Admins kunnen nieuwe gebruikers aanmaken door gegevens in te vullen zoals gebruikersnaam, wachtwoord en rol (admin of standaard gebruiker). Deze gegevens worden via de API naar de MySQL-database gestuurd en opgeslagen.

3. **Gebruikersbeheer (Alleen voor Admins):**  
   Admins kunnen bestaande gebruikers beheren (bewerken en verwijderen) via een apart formulier in de WinForms-applicatie. Alle gebruikers worden opgehaald via de API en weergegeven.

4. **CRUD Persoonsgegevens:**  
   De standaard gebruikers kunnen persoonsgegevens beheren. Deze acties (Create, Read, Update, Delete) worden uitgevoerd via de API, die deze gegevens in de MySQL-database opslaat.
   - **Create (Toevoegen):** Persoonsgegevens invoeren en opslaan in de MySQL-database via de API.
   - **Read (Bekijken):** Persoonsgegevens ophalen uit de database via de API en weergeven zonder gebruik van een DataGridView.
   - **Update (Bewerken):** Bestaande persoonsgegevens bewerken en opslaan.
   - **Delete (Verwijderen):** Persoonsgegevens verwijderen uit de database via de API.

5. **MySQL Database:**  
   - Eén tabel voor **gebruikers**, met velden voor gebruikersnaam, wachtwoord (versleuteld), en rol.
   - Eén tabel voor **persoonsgegevens**, met velden zoals naam, geboortedatum, e-mailadres, etc.

6. **ASP.NET Core API:**  
   Maak een API met endpoints voor:
   - **Gebruikersbeheer:** CRUD-functionaliteit voor het toevoegen, lezen, bijwerken en verwijderen van gebruikers.
   - **Persoonsgegevensbeheer:** CRUD-functionaliteit voor het beheren van persoonsgegevens.

7. **Beveiliging:**  
   - Gebruik versleuteling voor het opslaan van wachtwoorden.
   - Implementeer rolgebaseerde toegangscontrole zodat alleen admins bepaalde API-acties (zoals gebruikersbeheer) kunnen uitvoeren.

### Aanwijzingen:
- Gebruik Entity Framework Core voor de communicatie met de MySQL-database in de API.
- Zorg ervoor dat de WinForms-applicatie correcte HTTP-aanvragen stuurt naar de API om de login, registratie en CRUD-functionaliteiten te ondersteunen.
- Implementeer een beveiligde methode voor het opslaan van wachtwoorden, zoals hashing met een salt.

---

Dit project combineert CRUD-functionaliteit met gebruikersbeheer en beveiliging, en leert je werken met zowel lokale CSV-bestanden als een MySQL-database en een ASP.NET Core API. Het geeft inzicht in login-systemen, rolgebaseerde toegang en het gebruik van externe API’s voor gegevensbeheer.
