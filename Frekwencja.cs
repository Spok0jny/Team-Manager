//Frekwencja do obecnosci jest jak cos pozdro
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager
{
    public class Frekwencja
    {
        public int Id { get; set; }
        public string ZawodnikImieNazwisko { get; set; }  
        public int HarmonogramId { get; set; }
        public bool CzyObecny { get; set; }  
    }
}
