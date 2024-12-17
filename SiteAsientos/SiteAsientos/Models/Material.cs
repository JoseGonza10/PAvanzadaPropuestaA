using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Material
    {
        [Key]
        public int Material_Id { get; set; }
        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [Remote("MaterialExists", "Material", AdditionalFields = "Material_Id", HttpMethod = "POST", ErrorMessage = "Este material ya existe en el sistema")]
        [DisplayName("Material")]
        public string? Material_Name { get; set; }

        [DisplayName("Estado")]
        public bool Material_Status { get; set; } = true;
        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Proveedor")]
        public int? Material_SupplierId { get; set; }

        //Relaciones
        public ICollection<Design>? Designs { get; set; }

        public Supplier? Supplier { get; set; }
    }
}
