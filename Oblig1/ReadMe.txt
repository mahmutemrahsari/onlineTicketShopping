Vi har nå oppdatert oppgaven til en versjon som inneholder en innloggingsside for administratorer. 

Påloggingsinformasjon for admin-bruker :

Brukernavnet for dette er: Admin
Samsvarende passord er: Admin123 (A skal være stor)

Når administartor har logget seg inn vil den ta vedkommende inn til en side hvor 
man kan endre, slette eller legge til ulike faktorer angående rutereiser. De ulike variablene som er tilgjengelig i databasen vil for
bussnummer være 230, 150 og 100. Videre vil de tilgjengelige destinasjonene omfatte Oslo, Sandvika, Lillehammer og Lysaker. Når det kommer til
avgangstider vil tidene bestå av 14:00, 17:00, 13:00 og 09:30, mens ankomsttidene tilsvarer 15:00, 19:30 og 16:00. I tillegg vil
er datoene for de tilgjengelige rutene satt 2020-12-05, 2020-11-30 og 2020-12-20.

De tilgjengelige rutene for rutereisene i databasen består av følgende ruter:
1: BussNR = "230", FraRute = "Oslo", TilRute = "Sandvika", AvgangsTid = "14:00", AnkomstTid = "15:00", Dato = "2020-12-05" 
2: BussNR = "150", FraRute = "Oslo", TilRute = "Sandvika", AvgangsTid = "17:00", AnkomstTid = "19:30", Dato = "2020-12-05" 
3: BussNR = "100", FraRute = "Sandvika", TilRute = "Lillehammer", AvgangsTid = "13:00", AnkomstTid = "16:00", Dato = "2020-11-30" 
4: BussNR = "230", FraRute = "Sandvika", TilRute = "Oslo", AvgangsTid = "09:30", AnkomstTid = "15:00", Dato = "2020-12-20"
5: BussNR = "230", FraRute = "Lysaker", TilRute = "Oslo", AvgangsTid = "17:00", AnkomstTid = "19:00", Dato = "2020-11-30"

For betalingsvalidering vår har vi satt det slik at den kun tar inn VISA-kort. Man må dermed fylle inn seksten tall, der de første fire siffrene må
begynne med "4716" for VISA og de første sifrene må starte med "60115" for discover. Uavhengig av dette må kortnummrene ha de samme kjennetegnene som et 
ekte kort og Chrome som nettleser vil gjenkjenne hvilken type kort det er snakk om. Eksempler på de ulike korttypene og kortnumrene er:
VISA:
4716472565149133
4716171878335534

MasterCard:
2720992289645833
5137615931393292

Discover:
6011540576406622
6011515961528602 

Regex koden for kort er hentet fra denne siden:
https://stackoverflow.com/questions/9315647/regex-credit-card-number-tests

Regex koden for epost er hentet fra denne siden:
https://stackoverflow.com/questions/1710505/asp-net-email-validator-regex

Generering av kortnummere er hentet fra denne siden:
https://www.freeformatter.com/credit-card-number-generator-validator.html
