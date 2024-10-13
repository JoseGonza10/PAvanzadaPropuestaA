using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class VehicleSeat
    {
        [Key]
        public int VehicleSeat_Id { get; set; }

        [Required]
        public int VehicleSeat_VehicleId { get; set; }

        [Required]
        public int VehicleSeat_SeatId { get; set; }

        [Required]
        public float VehicleSeat_Price { get; set; }

        [Required]
        public float VehicleSeat_Taxable { get; set; }


        [ForeignKey("VehicleSeat_VehicleId")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("VehicleSeat_SeatId")]
        public Seat Seat { get; set; }
    }
}
