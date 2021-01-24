using LINQ_Werknemers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Werknemers
{
    class Program
    {

        static readonly Afdeling AfdelingIT = new Afdeling("IT");
        static readonly Afdeling AfdelingSales = new Afdeling("Sales");
        static readonly Afdeling AfdelingHR = new Afdeling("HR");
        static readonly Afdeling AfdelingDirectie = new Afdeling("Directie");

        static void Main(string[] args)
        {
            var medewerkers = InitMedewerkers();
            
            // we sluiten afdeling Directie uit, want directeurs worden anders 2 keer geteld
            var afdelingen = medewerkers
                .SelectMany(x => x.Afdelingen)
                .Where(x => x.Naam !=  AfdelingDirectie.Naam)
                .Distinct();

            foreach (var afdeling in afdelingen)
            {
                Console.WriteLine($"--- Afdeling {afdeling.Naam} ---");
                var medewerkersPerAfdeling = medewerkers
                    .Where(x => 
                    x.Afdelingen.Any(afd => afd == afdeling)
                    && x.Manager != null // filter managers
                    );
                Console.WriteLine("Afdeling {0} heeft {1} medewerker{2}", afdeling.Naam, medewerkersPerAfdeling.Count(), (medewerkersPerAfdeling.Count() == 1) ? "" : "s");

                var gemiddeldLoon = medewerkersPerAfdeling.Average(x => x.BrutoLoon);
                Console.WriteLine("Gemiddeld loon: " + Math.Round(gemiddeldLoon, 4));

                var maximumLoon = medewerkersPerAfdeling.Max(x => x.BrutoLoon);
                Console.WriteLine("Maximumloon: " + maximumLoon);

                var minimumLoon = medewerkersPerAfdeling.Min(x => x.BrutoLoon);
                Console.WriteLine("Minimumloon: " + minimumLoon);

                Console.WriteLine();
            }

            var meestVerdienendeVijf = medewerkers.OrderByDescending(x => x.BrutoLoon).Take(5);
            Console.WriteLine(string.Join("\n", meestVerdienendeVijf));

            var managers = medewerkers.Where(x => x.Manager == null && medewerkers.Any(y => y.Manager != null && y.Manager.Equals(x)));
            Console.WriteLine($"Aantal managers: {managers.Count()}");
            Console.WriteLine($"Gemiddelde loon managers: {Math.Round(managers.Average(x => x.BrutoLoon), 2)}");
            Console.WriteLine();

            var werknemers = medewerkers.Except(managers);
            Console.WriteLine(string.Join(", ", werknemers.Select(x => x.Achternaam)));
            Console.WriteLine();

            var lonenMetOpslag = managers.Select(x => x.BrutoLoon * 0.1m).Sum();
            Console.WriteLine($"Als alle managers 10% opslag krijgen, zal de bruto loonkost stijgen met {lonenMetOpslag:C}");
        }

        static List<Werknemer> InitMedewerkers()
        {
            #region Directeurs
            //Algemeen directeur (CEO) en hoofd van de sales afdeling
            var Ceo = new Werknemer(
                "Jean-Luc",
                "Picard",
                new DateTime(1990, 7, 13),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                4500M,
                new List<Afdeling> { AfdelingDirectie, AfdelingSales });

            // Hoofd van IT
            var ITManager = new Werknemer(
                "Majel",
                "Barrett",
                new DateTime(1991, 9, 12),
                new Adres("Rector Honoré Waeyenberghplein", "13", "0201", "1000", "Leuven", "Belgium"),
                3500M,
                new List<Afdeling> { AfdelingDirectie, AfdelingIT });

            // Hoofd van HR
            var HRManager = new Werknemer(
                "Deanna", 
                "Troi",
                new DateTime(1992, 11, 14),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                3500M,
                new List<Afdeling> { AfdelingDirectie, AfdelingHR });

            #endregion

            #region IT medewerkers
            var ITWerknemer1 = new Werknemer(
                "Geordi",
                "La Forge",
                new DateTime(1995, 2, 16),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2100m,
                AfdelingIT,
                ITManager);

            var ITWerknemer2 = new Werknemer(
                "Will",
                "Riker",
                new DateTime(1995, 10, 13),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2000m,
                AfdelingIT,
                ITManager);

            var ITWerknemer3 = new Werknemer(
                "Natasha",
                "Yar",
                new DateTime(1994, 1, 16),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2500m,
                AfdelingIT,
                ITManager
                );


            var ITWerknemer4 = new Werknemer(
                "Robin",
                "Lefler",
                new DateTime(1994, 10, 1),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2500m,
                AfdelingIT,
                ITManager
                );

            #endregion

            #region HR medewerkers

            var HRWerknemer1 = new Werknemer(
                "Worf",
                "Rozhenko",
                new DateTime(1994, 12, 15),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2600m,
                AfdelingHR,
                HRManager);

            var HRWerknemer2 = new Werknemer(
                "Beverly",
                "Crusher",
                new DateTime(1993, 1, 16),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2700m,
                AfdelingHR,
                HRManager);

            var HRWerknemer3 = new Werknemer(
                "Katherine",
                "Rozhenko",
                new DateTime(1994, 12, 15),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2600m,
                AfdelingHR,
                HRManager);

            #endregion

            #region Sales medewerkers

            var SalesWerknemer1 = new Werknemer(
                "Wesley",
                "Crusher",
                new DateTime(1993, 12, 15),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2000m,
                AfdelingSales,
                Ceo);

            var SalesWerknemer2 = new Werknemer(
                "Miles",
                "O'Brien",
                new DateTime(1993, 1, 16),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2000m,
                AfdelingSales,
                Ceo);

            var SalesWerknemer3 = new Werknemer(
                "Ro",
                "Laren",
                new DateTime(1994, 01, 17),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2000m,
                AfdelingSales,
                Ceo);


            var SalesWerknemer4 = new Werknemer(
                "Sonya",
                "Gomez",
                new DateTime(1994, 01, 17),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2000m,
                AfdelingSales,
                Ceo);

            var SalesWerknemer5 = new Werknemer(
                "Alynna",
                "Nechayev",
                new DateTime(1991, 11, 1),
                new Adres("Nijverheidskaai", "171", "1070", "Brussel", "Belgium"),
                2000m,
                AfdelingSales,
                Ceo);

            #endregion

            return new List<Werknemer>
            {
                Ceo, ITManager, HRManager,
                ITWerknemer1, ITWerknemer2, ITWerknemer3, ITWerknemer4,
                HRWerknemer1, HRWerknemer2, HRWerknemer3,
                SalesWerknemer1, SalesWerknemer2, SalesWerknemer3, SalesWerknemer4, SalesWerknemer5
            };
        }
    }
}
