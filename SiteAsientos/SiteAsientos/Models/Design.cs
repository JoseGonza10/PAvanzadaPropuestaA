using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SiteAsientos.Models
{
    public class Design
    {
        [Key]
        public int Design_Id { get; set; } 

        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Descripción")]
        public string? Design_Description { get; set; }

        [DisplayName("Material")]
        public int? Design_MaterialId { get; set; }

        [DisplayName("Color")]
        public string? Design_Color { get; set; }

        [DisplayName("Estado")]
        public bool Design_Status { get; set; } = true;

        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Precio")]
        public float Design_Price { get; set; }

        [DisplayName("Impuesto")]
        [Required(ErrorMessage = "Se requiere este campo")]
        public float Design_Taxable { get; set; }

        [DisplayName("Servicio")]
        public string? Design_Service {  get; set; }

        //Relaciones
        public Material? Material { get; set; }
        public ICollection<Image>? Images { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }

}
