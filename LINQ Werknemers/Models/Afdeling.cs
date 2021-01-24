using System;

namespace LINQ_Werknemers.Models
{
    class Afdeling
    {
        public string Naam { get; set; }

        public Afdeling(string naam)
        {
            Naam = naam ?? throw new ArgumentNullException(nameof(naam));
        }

        public override bool Equals(object obj)
        {
            return obj is Afdeling afdeling &&
                   Naam == afdeling.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam);
        }

        public override string ToString()
        {
            return Naam;
        }
    }
}
