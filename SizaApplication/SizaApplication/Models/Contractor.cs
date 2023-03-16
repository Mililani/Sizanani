using System.ComponentModel.DataAnnotations;

namespace SizaApplication.Models
{
    public class Contractor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int contractorID { get; set; }
       
    }
}
