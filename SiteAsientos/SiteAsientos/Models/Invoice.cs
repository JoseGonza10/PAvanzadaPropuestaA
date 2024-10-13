using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{

    public class Invoice
    {
        [Key]
        public int Invoice_Id { get; set; }

        [Required, MaxLength(32)]
        public string Invoice_Code { get; set; }

        [Required]
        public int Invoice_OrderId { get; set; }

        public float? Invoice_Total { get; set; }

        public DateTime? Invoice_Date { get; set; }

        // Relaciones
        [ForeignKey("Invoice_OrderId")]
        public Order Order { get; set; }
    }
}
