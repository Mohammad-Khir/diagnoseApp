# diagnoseApp

## Om Prosjektet
`diagnoseApp` er en webapplikasjon utviklet for å håndtere og administrere persondata på en sikker og effektiv måte. Applikasjonen tilbyr funksjoner for å lagre, hente, oppdatere og slette personopplysninger, samtidig som den sikrer brukerautentisering og sesjonshåndtering.
Applikasjonen er ment å fungere som et verktøy hvor brukere kan velge symptomer fra en liste, og basert på valgene, vil systemet gi en indikasjon på hvilken sykdom eller tilstand brukeren kan ha. Dette kan hjelpe brukere med å vurdere om de bør kontakte en lege eller spesialist for videre undersøkelse.

## Kjernefunksjoner
- **Brukerautentisering:** sikker innlogging og utlogging av brukere.
- **CRUD Operasjoner:** Tillater brukere å opprette, lese, oppdatere og slette persondata i databasen.
- **Validering:** Implementerer streng validering av inndata for å sikre dataintegritet og overholde databeskyttelsesregler.

## Detaljert oversikt over funksjonene i diagnoseApp

### Påloggingssystem
- **Sikker tilgang:** Systemet krever at brukere registrerer seg og logger på for å sikre personvern og sikkerhet til informasjonen.
- **Brukeradministrasjon:** Brukere kan opprette og vedlikeholde sine profiler, inkludert oppdatering av personlig informasjon og endring av passord.

### Symptomvalg
- **Interaktiv Liste:** Brukerne presenteres med en interaktiv liste over symptomer, der de kan velge de som best beskriver deres nåværende helseproblemer.
- **Flervalgsalternativer:** Brukere kan velge flere symptomer fra listen for å sikre en mer omfattende evaluering.

### Diagnoseforslag
- **Analyseverktøy:** Basert på de innsamlede dataene fra brukerens valg, bruker systemet en bakgrunnsalgoritme for å analysere symptomene og foreslå mulige diagnoser.
- **Tilbakemelding:** Brukeren mottar en umiddelbar tilbakemelding om mulige sykdommer eller tilstander som kan korrespondere med de valgte symptomene.

### Rådgivning
- **Videre tiltak:** Basert på de foreslåtte diagnosene, gir applikasjonen råd om nødvendigheten av å oppsøke medisinsk ekspertise.
- **Ressurslenker:** Systemet kan tilby lenker til relevante medisinske ressurser eller informasjon om lokale helseinstitusjoner for videre undersøkelser eller behandling.

### Brukervennlig grensesnitt
- **Enkel Navigasjon:** Grensesnittet er designet for å være intuitivt og enkelt for alle brukertyper, uansett teknisk kompetanse.
- **Responsive Design:** Applikasjonen er tilgjengelig på tvers av ulike enheter, inkludert desktop, tablet, og mobil, sikrer tilgang uansett hvor brukeren befinner seg.


## Teknologier
Applikasjonen er utviklet med:
- **C#** for backend logikk
- **ASP.NET Core MVC** for rammeverk
- **Entity Framework** for databaseinteraksjoner
- **SQLite** for databaselagring
