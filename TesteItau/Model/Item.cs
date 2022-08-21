using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteItau.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Moeda { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
