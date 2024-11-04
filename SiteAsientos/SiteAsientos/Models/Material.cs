using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Material
    {
        [Key]
        public int Material_Id { get; set; }

        [Required, MaxLength(32)]
        [Remote("MaterialExists", "Material", ErrorMessage = "Este material ya existe en el sistema")]
        [DisplayName("Material")]
        public string Material_Name { get; set; }

        [Required]
        [DisplayName("Estado")]
        public bool Material_Status { get; set; }


        public ICollection<LateralDesign>? LateralDesigns { get; set; }
        public ICollection<CentralDesign>? CentralDesigns { get; set; }
    }
}
