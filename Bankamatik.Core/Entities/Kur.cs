using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankamatik.Core.Entities
{
    public class Kur
    {
        public string Kod { get; set; }
        public string Isim { get; set; }
        public decimal Alis { get; set; }
        public decimal Satis { get; set; }
        public DateTime Tarih { get; set; }
    }

}
