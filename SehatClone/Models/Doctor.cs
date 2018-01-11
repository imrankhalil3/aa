using System.Collections.Generic;

namespace SehatClone.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<Speciality> Speciality { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public byte ExperienceInYears { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Education { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }

        public Doctor()
        {
            Speciality = new List<Speciality>();
            ImageUrl = "/Content/images/no-user.png";
        }

    }
}