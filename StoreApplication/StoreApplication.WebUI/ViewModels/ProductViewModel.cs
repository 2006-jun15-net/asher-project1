using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApplication.WebUI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [HiddenInput]
        public string Name { get; set; }

        [HiddenInput]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Amount")]
        [Range(0, int.MaxValue)]
        [Required]
        public int AmountOrdered { get; set; }
    }
}
