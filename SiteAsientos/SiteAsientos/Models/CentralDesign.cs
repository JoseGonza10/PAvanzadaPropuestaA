using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class CentralDesign
    {
        [Key]
        public int CentralDesign_Id { get; set; }

        [Required]
        public int CentralDesign_MaterialId { get; set; }

        [Required]
        public int CentralDesign_ColorId { get; set; }

        
        [ForeignKey("CentralDesign_MaterialId")]
        public Material Material { get; set; }

        [ForeignKey("CentralDesign_ColorId")]
        public Color Color { get; set; }

        public ICollection<Design> Designs { get; set; }
    }

}
