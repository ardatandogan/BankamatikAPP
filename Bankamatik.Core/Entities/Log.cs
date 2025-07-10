using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankamatik.Core.Entities
{
    public class Log
    {
        public int LogID { get; set; }
        public int? UserID { get; set; }  // Logu yapan kullanıcı
        public string ActionType { get; set; } = string.Empty; // e.g. "Insert", "Delete", "Update"
        public string Description { get; set; } = string.Empty; // Detaylar
        public DateTime CreatedAt { get; set; }
    }
    
}
