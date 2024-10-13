using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Seat
    {
        [Key]
        public int Seat_Id { get; set; }

        [Required, MaxLength(256)]
        public string Seat_Description { get; set; }


        public ICollection<VehicleSeat> VehicleSeats { get; set; }
    }
}
