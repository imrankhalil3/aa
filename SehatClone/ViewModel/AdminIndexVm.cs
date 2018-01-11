using System.Collections.Generic;
using SehatClone.Models;

namespace SehatClone.ViewModel
{
    public class AdminIndexVm
    {
        public List<Doctor> Doctors { get; set; }
        public List<Center> Centers { get; set; }
        public List<Donor> Donors { get; set; }

        public AdminIndexVm()
        {
            Doctors = new List<Doctor>();
            Centers = new List<Center>();
            Donors = new List<Donor>();
        }
    }
}