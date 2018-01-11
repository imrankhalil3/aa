using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SehatClone.Models
{
    public class Center
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CenterType Type { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public List<CenterServices> Services { get; set; }
        public string ForGender { get; set; }
        public string Timings { get; set; }
        public string Description { get; set; }

        public bool IsApproved { get; set; }

        public Center()
        {
            Services = new List<CenterServices>();
            ForGender = "Both Male and Female";
            ImageUrl = "/Content/images/no-image.jpg";
        }
    }
}