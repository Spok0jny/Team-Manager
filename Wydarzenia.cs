using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager
{
    [Table("wydarzenia")]
    public class Wydarzenia
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("tytul")]
        public string tytul { get; set; }
        public byte[] zdjecie { get; set; }
        [Column("autor")]
        public string autor { get; set; }
        [Column("dataWydarzenia")]
        public DateTime dataWydarzenia { get; set; }
    }
}
