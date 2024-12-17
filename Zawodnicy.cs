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

        [Column("Klub")]
        public string Klub { get; set; }



    }
}
