using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
