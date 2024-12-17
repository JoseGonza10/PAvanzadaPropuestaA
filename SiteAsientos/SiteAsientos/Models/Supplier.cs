using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Supplier
    {
        [Key]
        public int Supplier_Id { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [DisplayName("Nombre")]
        public string? Supplier_Name { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(256)]
        [DisplayName("Dirección")]
        public string? Supplier_Address { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(8)]
        [RegularExpression(@"^\d{8}$",ErrorMessage = "El numero de telefono es incorrecto")]
        [Remote("PhoneExists", "Supplier", AdditionalFields = "Supplier_Id", HttpMethod = "POST", ErrorMessage = "Este telefono ya esta siendo usado en otro proveedor")]
        [DisplayName("Telefono")]
        public string? Supplier_Phone { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(256)]
        [EmailAddress]
        [Remote("EmailExists","Supplier", AdditionalFields = "Supplier_Id", HttpMethod = "POST",ErrorMessage = "Este correo electrónico ya se encuentra en uso")]
        [DisplayName("Correo Electrónico")]
        public string? Supplier_Email { get; set; }
        [DisplayName("Fecha de Ingresión")]
        public DateTime? Supplier_DateAdded { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Estado")]
        public bool Supplier_Status { get; set; }

        //Relaciones
        public ICollection<Material>? Materials { get; set; }
    }
}
