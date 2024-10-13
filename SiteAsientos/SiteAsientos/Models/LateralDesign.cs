using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class LateralDesign
    {
        [Key]
        public int LateralDesign_Id { get; set; }

        [Required]
        public int LateralDesign_MaterialId { get; set; }

        [Required]
        public int LateralDesign_ColorId { get; set; }


        [ForeignKey("LateralDesign_MaterialId")]
        public Material Material { get; set; }

        [ForeignKey("LateralDesign_ColorId")]
        public Color Color { get; set; }

        public ICollection<Design> Designs { get; set; }
    }
}
