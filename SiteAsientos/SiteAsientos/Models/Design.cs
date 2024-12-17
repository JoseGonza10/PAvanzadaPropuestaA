using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Humanizer;

namespace SiteAsientos.Models
{
    public class Design
    {
        [Key]
        public int Design_Id { get; set; } 

        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Descripción")]
        public string? Design_Description { get; set; }

        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Material")]
        public int? Design_MaterialId { get; set; }

        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Color")]
        public string? Design_Color { get; set; }

        [DisplayName("Estado")]
        public bool Design_Status { get; set; } = true;

        [Required(ErrorMessage = "Se requiere este campo")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Cifra digitada no valida")]
        [DisplayName("Precio")]
        public float Design_Price { get; set; }
        [Required(ErrorMessage = "Se requiere este campo")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Cifra digitada no valida")]
        [Range(1,100,ErrorMessage = "El porcentaje de impuesto no puede ser mayor a 100% ")]
        [DisplayName("Impuesto")]

        public float Design_Taxable { get; set; }
        [Required(ErrorMessage = "Se requiere este campo")]
        [DisplayName("Servicio")]
        public string? Design_Service {  get; set; }

        //Relaciones
        public Material? Material { get; set; }
        public ICollection<Image>? Images { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }

}
