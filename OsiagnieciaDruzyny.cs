using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Team_Manager
{
    [Table("osiagniecia_druzyny")]
    public class OsiagnieciaDruzyny
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("nazwa_osiagniecia")]
        public string NazwaOsiagniecia { get; set; }

        [Column("data_osiagniecia")]
        public DateTime DataOsiagniecia { get; set; }
    }
}