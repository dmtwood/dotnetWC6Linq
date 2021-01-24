using System;
using System.Collections.Generic;

namespace LINQ_Werknemers.Models
{
    class Werknemer
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public Adres Adres { get; set; }
        public List<Afdeling> Afdelingen { get; set; }
        public decimal BrutoLoon { get; set; }
        public Werknemer Manager { get; set; }

        public Werknemer(string voornaam, string achternaam, DateTime geboortedatum, Adres adres, decimal brutoLoon, List<Afdeling> afdelingen, Werknemer manager = null)
        {
            Voornaam = voornaam ?? throw new ArgumentNullException(nameof(voornaam));
            Achternaam = achternaam ?? throw new ArgumentNullException(nameof(achternaam));
            Geboortedatum = geboortedatum;
            Adres = adres ?? throw new ArgumentNullException(nameof(adres));
            Afdelingen = afdelingen ?? throw new ArgumentNullException(nameof(afdelingen));
            BrutoLoon = brutoLoon;
            Manager = manager;
        }

        public Werknemer(string voornaam, string achternaam, DateTime geboorteDatum, Adres adres, Decimal brutoloon, Afdeling afdeling, Werknemer manager = null) : 
            this(voornaam, achternaam, geboorteDatum, adres, brutoloon, new List<Afdeling> { afdeling }, manager)
        {

        }

        public override bool Equals(object obj)
        {
            return obj is Werknemer werknemer &&
                   Voornaam == werknemer.Voornaam &&
                   Achternaam == werknemer.Achternaam &&
                   Geboortedatum == werknemer.Geboortedatum &&
                   BrutoLoon == werknemer.BrutoLoon &&
                   Adres.Equals(werknemer.Adres);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Voornaam, Achternaam, Geboortedatum, BrutoLoon, Adres);
        }



        public override string ToString()
        {
            string output = $"{Achternaam.ToUpper()}, {Voornaam}";
            if (Afdelingen != null)
            {
                output += $"\nAfdeling(en): {string.Join(", ", Afdelingen)}";
            }
            if (Adres != null)
            {
                output += $"\nAdres: {Adres}";
            }

            output += "\n";

            return output;
        }
    }
}
