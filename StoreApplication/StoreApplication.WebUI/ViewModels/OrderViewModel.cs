using System.ComponentModel.DataAnnotations;

namespace StoreApplication.WebUI.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        [Display(Name = "Amount Purchased")]
        public int amountOrdered { get; set; }
        public decimal TotalSum { get; set; }
    }
}
