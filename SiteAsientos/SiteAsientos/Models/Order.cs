using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }

        [Required, MaxLength(32)]
        public string Order_Code { get; set; }

        [Required]
        public DateTime Order_Date { get; set; }

        [Required, MaxLength(128)]
        public string Order_CustomerName { get; set; }

        [Required, MaxLength(32)]
        public string Order_CustomerId { get; set; }

        [Required, MaxLength(32)]
        public string Order_CustomerPhone { get; set; }

        [Required, MaxLength(256)]
        public string Order_CustomerEmail { get; set; }

        [Required]
        public int Order_CustomerCar { get; set; }

        [Required, MaxLength(32)]
        public string Order_CustomerPlateNumber { get; set; }

        public DateTime? Order_FirstVisitDate { get; set; }

        public DateTime? Order_SecondVisitDate { get; set; }

        [Required]
        public bool Order_Status { get; set; }

        // Navegaciones
        [ForeignKey("Order_CustomerCar")]
        public Vehicle CustomerCar { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Visit> Visits { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }

}
