using System.ComponentModel.DataAnnotations;

namespace SizaApplication.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public decimal WeightInTons { get; set; }
        public int contractorId { get; set; }
    }
}
