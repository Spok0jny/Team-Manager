using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager
{
    [Table("mecz")]
    public class Mecze
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("druzyna1")]
        public string druzyna1 { get; set; }

        [Column("gole1")]
        public int gole1 { get; set; }

        [Column("druzyna2")]
        public string druzyna2 { get; set; }

        [Column("gole2")]
        public int gole2 { get; set; }

        [Column("dataMeczu")]
        public DateTime dataMeczu { get; set; }
    }

    [Table("przebiegMeczu")]

    public class przebiegMeczu
    {
        [Column("idMeczu")]
        public int idMeczu { get; set; }

        [Column("KtoraDruzyna")]
        public int ktoraDruzyna { get; set; }

        [Column("KtoraMinuta")]
        public int ktoraMinuta { get; set; }

        [Column("akcja")]
        public string akcja { get; set; }

        [Column("idZawodnika")]
        public int idZawodnika { get;set; }

        [Column("nazwaZawodnika")]
        public string nazwaZawodnika { get; set; }
    }
}
