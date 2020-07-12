using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace StoreApplication.WebUI.ViewModels
{
    public class OrderHistoryViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        [Display(Name = "Date/Time Ordered")]
        public DateTime TimeOrdered { get; set; }
    }
}
