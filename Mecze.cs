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
        
        [Column("druzyna2")]
        public string druzyna2 { get; set; }
        
        [Column("dataMeczu")]
        public DateTime dataMeczu { get; set; }
    }
}
