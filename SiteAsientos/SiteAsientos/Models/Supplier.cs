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

        [Required, MaxLength(32)]
        [DisplayName("Nombre")]
        public string Supplier_Name { get; set; }

        [Required, MaxLength(256)]
        [DisplayName("Dirección")]
        public string Supplier_Address { get; set; }

        [Required, MaxLength(8)]
        [RegularExpression(@"^\d{8}$",ErrorMessage = "El numero de telefono es incorrecto")]
        [Remote("PhoneExists", "Supplier", ErrorMessage = "Este telefono ya esta siendo usado en otro proveedor")]
        [DisplayName("Telefono")]
        public string Supplier_Phone { get; set; }

        [Required, MaxLength(256)]
        [EmailAddress]
        [Remote("EmailExists","Supplier",ErrorMessage = "Este correo electrónico ya se encuentra en uso")]
        [DisplayName("Correo Electrónico")]
        public string Supplier_Email { get; set; }
        [DisplayName("Fecha de Ingresión")]
        public DateTime? Supplier_DateAdded { get; set; }

        [Required]
        [DisplayName("Estado")]
        public bool Supplier_Status { get; set; }


        public ICollection<Product>? Products { get; set; }
    }
}
