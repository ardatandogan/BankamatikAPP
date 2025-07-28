using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BankamatikWebApp.Models
{
    public class WithdrawDepositViewModel
    {
        public int CurrentUserID { get; set; }

        [Required(ErrorMessage = "Lütfen bir hesap seçiniz.")]
        [Display(Name = "Hesap Seçin")]
        public int SelectedAccountId { get; set; }

        [Required(ErrorMessage = "Lütfen miktarı giriniz.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar pozitif bir değer olmalıdır.")]
        [Display(Name = "Miktar")]
        public decimal Amount { get; set; }

        public List<SelectListItem>? Accounts { get; set; }

        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}