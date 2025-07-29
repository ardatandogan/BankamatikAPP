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
        public int? UserID { get; set; }  
        public string ActionType { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; } = new DateTime(1900, 1, 1);
        public DateTime EndDate { get; set; } = new DateTime(1900, 1, 1);



    }
    
}
