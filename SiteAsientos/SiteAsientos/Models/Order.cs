using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SiteAsientos.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }

        [Required, MaxLength(32)]
        [DisplayName("Numero de Orden")]
        public string? Order_Code { get; set; }

        [Required]
        [DisplayName("Fecha de Creación")]
        public DateTime Order_Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(128)]
        [DisplayName("Cliente")]
        public string? Order_CustomerName { get; set; }
        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "La cedula digitada no es correcta")]
        [DisplayName("Cedula")]
        public string? Order_CustomerId { get; set; }
        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El telefono digitado no es correcto")]
        [DisplayName("Telefono")]
        public string? Order_CustomerPhone { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(256)]
        [DisplayName("Correo Electrónico")]
        public string? Order_CustomerEmail { get; set; }
        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Marca")]
        public string? Order_CarBrand { get; set; }
        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Modelo")]
        public string? Order_CarModel { get; set; }
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El año del modelo no es correcto")]
        [DisplayName("Año de Modelo")]
        public string? Order_ModelYear { get; set; }
        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "El numero de placa no es correcto")]
        [DisplayName("Numero de Placa")]
        public string? Order_CustomerPlateNumber { get; set; }

        [DisplayName("Diseño")]
        public int? Order_DesignId { get; set; }
        [DisplayName("Estado")]
        public bool Order_Status { get; set; } = true;
        [DisplayName("Bordado")]
        public bool Order_Embroidery { get; set; } = false;


        //Relaciones
        public Design? Design { get; set; }
        public ICollection<Visit>? Visits { get; set; }
        public Invoice? Invoice { get; set; }
    }

}
