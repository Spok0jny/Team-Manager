using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Team_Manager
{
    [Table("zawodnicy")]
    public class Zawodnicy
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("imie")]
        public string Imie { get; set; }

        [Column("nazwisko")]
        public string Nazwisko { get; set; }

        [Column("numer")]
        public int Numer { get; set; } 

        [Column("pozycja")]
        public string Pozycja { get; set; } 

        [Column("wiek")]
        public int Wiek { get; set; } 

        [Column("koniec_kontraktu")]
        public string KoniecKontraktu { get; set; }

        [Column("bramki")]
        public int Bramki { get; set; } = 0;
        
        [Column("asysty")]
        public int Asysty { get; set; } = 0;

        [Column("zolteKartki")]
        public int ZolteKartki { get; set; } = 0;

        [Column("czerwoneKartki")]
        public int CzerwoneKartki { get; set; } = 0;

        public string ImieNazwisko => $"{Imie} {Nazwisko}";

        
        
    }
}
