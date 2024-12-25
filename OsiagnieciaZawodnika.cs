using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Team_Manager
{
    [Table("osiagniecia_zawodnika")]
    public class OsiagnieciaZawodnika
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("zawodnik_id")]
        public int ZawodnikId { get; set; }

        [Column("nazwa_osiagniecia")]
        public string NazwaOsiagniecia { get; set; }

        [Column("data_osiagniecia")]
        public DateTime DataOsiagniecia { get; set; }
    }
}
