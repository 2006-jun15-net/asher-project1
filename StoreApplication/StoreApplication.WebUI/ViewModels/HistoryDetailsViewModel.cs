using Microsoft.AspNetCore.Mvc;
using StoreApplication.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.WebUI.ViewModels
{
    public class HistoryDetailsViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }

        [Display(Name = "Location")]
        public string LocationAddress { get; set; }

        public IEnumerable<OrderViewModel> orders { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        [Display(Name = "Date/Time Ordered")]
        public DateTime TimeOrdered { get; set; }
    }
}
