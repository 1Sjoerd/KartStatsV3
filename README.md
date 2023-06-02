# Welkom bij KartStats!

KartStats is een webapplicatie waarin je jou kart resultaten kunt bijhouden en kunt vergelijken met de resultaten van anderen.

## Inleiding
KartStats is een karting resultaten tracker waarin je je resultaten kunt opslaan en jezelf kunt vergelijken met andere gebruikers.<br>
Doormiddel van groepen die gebruikers zelf aan kunnen maken is het mogelijk de resultaten van iedereen in de groep te categorieseren per circuit.<br>
Om wat meer inzicht te creëren over hoe deze webapplicatie ongeveer gaat werken is deze schets gemaakt waarin de requirements verwerkt zijn.<br>

![Conceptueel Model](KartStatsV3/wwwroot/Images/Schets.png)

## Requirements

Requirements voor KARTER:

FR-01: De gebruiker moet kunnen inloggen.
B-01.1: Het inlogproces moet beveiligd zijn met een gebruikersnaam en wachtwoord.

FR-02: De gebruiker moet een account kunnen aanmaken.
B-02.1: Het registratieproces moet persoonlijke gegevens bevatten, zoals naam, e-mailadres en wachtwoord.
B-02.2: Het wachtwoord moet beveiligd opgeslagen worden.

FR-03: De gebruiker moet zijn resultaten (snelste ronde) kunnen invoeren.
B-03.1: Er moet een functionaliteit zijn om resultaten in te voeren(snelste ronde).
B-03.2: De ingevoerde resultaten moeten gekoppeld zijn aan de specifieke gebruiker.

FR-04: De gebruiker moet de resultaten van gereden heats (snelste rondes) kunnen inzien.
B-04.1: Er moet een overzicht zijn van de resultaten(snelste rondes) van alle gereden heats.

FR-05: De gebruiker moet de resultaten kunnen zien in een grafiek.
B-05.1: Er moet een grafische weergave zijn van de resultaten, bijvoorbeeld een grafiek of diagram.

FR-06: De gebruiker moet zijn resultaten per circuit kunnen inzien.
B-06.1: Er moet een mogelijkheid zijn om resultaten per circuit te filteren en te bekijken.

FR-07: De gebruiker moet de snelste tijd per circuit kunnen zien.
B-07.1: Het systeem moet de snelste tijd per circuit berekenen en weergeven.

FR-08: De gebruiker moet een groep kunnen aanmaken en gebruikers kunnen uitnodigen/verwijderen uit de groep.
B-08.1: Er moet een functionaliteit zijn om een groep aan te maken.
B-08.2: De gebruiker moet andere gebruikers kunnen uitnodigen om lid te worden van de groep.
B-08.3: De gebruiker moet leden kunnen verwijderen uit de groep.

Requirements voor BEHEERDER:

FR-09: De beheerder moet circuits kunnen toevoegen.
B-09.1: Er moet een functionaliteit zijn om nieuwe circuits toe te voegen aan het systeem.

## Use Cases

Naam UC01: Inloggen
Samenvatting De gebruiker voert de benodigde gegevens in om in te loggen op het systeem.
Actors Karter
Aannamen Geen
Scenario <ol><li>De Karter geeft aan dat hij wil inloggen.</li><li>Het systeem toont een inlogpagina.</li><li>De Karter voert zijn gebruikersnaam en wachtwoord in en bevestigt.</li><li>Het systeem controleert de ingevoerde gegevens en verifieert de identiteit van de Karter. [1]</li></ol>
Uitzonderingen <ol><li>De ingevoerde gebruikersnaam of het wachtwoord is onjuist. Toon een foutmelding en ga terug naar stap 3.</li></ol>
Resultaat De Karter is succesvol ingelogd op het systeem.

## Contextdiagram en Conceptueel model

Om een idee te geven hoe de applicatie eruit komt te zien, heb ik een context diagram gemaakt. Ook heb ik een Conceptueel Model gemaakt die de structuur van de applicatie weergeeft.<br> Beide staan hieronder:<br>

Context Diagram             |  Conceptueel Model
:-------------------------:|:-------------------------:
![Context Diagram](KartStatsV3/wwwroot/Images/ContextDiagram.png)  |  ![Conceptueel Model](KartStatsV3/wwwroot/Images/ConceptueelModel.png)
