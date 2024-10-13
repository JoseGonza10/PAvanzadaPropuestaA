using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Vehicle
    {
        [Key]
        public int Vehicle_Id { get; set; }

        [Required, MaxLength(32)]
        public string Vehicle_Brand { get; set; }

        [Required, MaxLength(64)]
        public string Vehicle_Model { get; set; }

        [Required]
        public int Vehicle_ModelYear { get; set; }


        public ICollection<VehicleSeat> VehicleSeats { get; set; }
        public ICollection<Design> Designs { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
