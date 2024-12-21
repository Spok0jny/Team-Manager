using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager
{
    [Table("harmonogram")]
    public class Harmonogram
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("data")]
        public string Data { get; set; }  

        [Column("typ")]
        public string Typ { get; set; }  
    }
}
