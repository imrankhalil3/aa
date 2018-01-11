using System.ComponentModel.DataAnnotations;
using SehatClone.Models;

namespace SehatClone.ViewModel
{
    public class CreateDoctorVm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password Dosent Match")]
        public string ConfirmPassword { get; set; }

        public string Speciality { get; set; }

        public string City { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }
    }
}