using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SehatClone.ViewModel
{
    public class CreateDonorVm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public string Type { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password Dosent Match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Phone { get; set; }

        public List<string> BloodTypes { get; set; }

        public CreateDonorVm()
        {
            BloodTypes = new List<string>();
        }
    }
}