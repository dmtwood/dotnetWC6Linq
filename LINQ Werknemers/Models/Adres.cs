using System;
using System.Text;

namespace LINQ_Werknemers.Models
{
    class Adres
    {
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Busnummer { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Land { get; set; }

        public Adres(string straatnaam, string huisnummer, string busnummer, string postcode, string gemeente, string land)
        {
            Straatnaam = straatnaam ?? throw new ArgumentNullException(nameof(straatnaam));
            Huisnummer = huisnummer ?? throw new ArgumentNullException(nameof(huisnummer));
            Busnummer = busnummer;
            Postcode = postcode ?? throw new ArgumentNullException(nameof(postcode));
            Gemeente = gemeente ?? throw new ArgumentNullException(nameof(gemeente));
            Land = land ?? throw new ArgumentNullException(nameof(land));
        }

        public Adres(string straatnaam, string huisnummer, string postcode, string gemeente, string land) :
            this(straatnaam, huisnummer, null, postcode, gemeente, land)
        {
           
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{Straatnaam} {Huisnummer}");

            if (!string.IsNullOrWhiteSpace(Busnummer))
                stringBuilder.Append($" bus {Busnummer}");

            stringBuilder.Append($" / {Postcode} {Gemeente}");
            stringBuilder.Append($" / {Land}");

            return stringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Adres adres &&
                   Straatnaam == adres.Straatnaam &&
                   Huisnummer == adres.Huisnummer &&
                   Busnummer == adres.Busnummer &&
                   Postcode == adres.Postcode &&
                   Gemeente == adres.Gemeente &&
                   Land == adres.Land;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Straatnaam, Huisnummer, Busnummer, Postcode, Gemeente, Land);
        }
    }
}
