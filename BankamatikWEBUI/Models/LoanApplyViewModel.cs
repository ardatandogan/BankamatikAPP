using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bankamatik.WebApp.Models
{
    public class LoanApplyViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Enter a valid loan amount.")]
        public decimal Amount { get; set; }

        [Required]
        public decimal SelectedInterestRate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public List<decimal> InterestRateOptions { get; set; } = new();
    }
}
