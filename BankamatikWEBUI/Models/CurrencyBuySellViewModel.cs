using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BankamatikWebApp.Models
{
    public class CurrencyBuySellViewModel
    {
        public int CurrentUserID { get; set; }

        [Required(ErrorMessage = "Lütfen gönderen hesabı seçiniz.")]
        public int SelectedFromAccountId { get; set; }

        [Required(ErrorMessage = "Lütfen alıcı hesabı seçiniz.")]
        public int SelectedToAccountId { get; set; }

        // SelectedCurrencyCode artık kaldırılıyor
        // public string SelectedCurrencyCode { get; set; }

        [Required(ErrorMessage = "Lütfen miktarı giriniz.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar pozitif bir değer olmalıdır.")]
        public decimal Amount { get; set; }

        public List<SelectListItem>? FromAccounts { get; set; }
        public List<SelectListItem>? ToAccounts { get; set; }

        // Currencies listesi artık kaldırılıyor
        // public List<SelectListItem>? Currencies { get; set; }

        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}