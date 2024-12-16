using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SiteAsientos.Models
{

    public class Invoice
    {
        [Key]
        public int Invoice_Id { get; set; }

        [Required, MaxLength(32)]
        public string? Invoice_Code { get; set; }

        [Required]
        public int? Invoice_OrderId { get; set; }

        [DisplayName("Monto Total")]
        public float? Invoice_Total { get; set; }

        [DisplayName("Monto Abonado")]
        public float? Invoice_AmoundPaid { get; set; }

        [DisplayName("Fecha de Facturación")]
        public DateTime? Invoice_Date { get; set; } = DateTime.Now;

        [DisplayName("Nombre del Cliente")]
        public string? Invoice_CustomerName { get; set; }

        [DisplayName("Telefono del Cliente")]
        public string? Invoice_CustomerPhone { get; set; }

        [DisplayName("Correo del Cliente")]
        public string? Invoice_CustomerEmail { get; set; }

        //Relaciones
        public Order? Order { get; set; }
    }
}
