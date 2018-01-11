using System.Collections.Generic;

namespace SehatClone.Models
{

    public class CenterServices
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Center> Centers { get; set; }

        public CenterServices()
        {
            Centers = new List<Center>();
        }
    }
}