using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankamatik.Core.Entities
{
    public class Loan
    {
        public int LoanID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }  // Active, Paid, vb.
        public DateTime CreatedAt { get; set; }

    }
}
