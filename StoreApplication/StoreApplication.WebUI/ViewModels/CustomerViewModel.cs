using System.ComponentModel.DataAnnotations;

namespace StoreApplication.WebUI.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
