using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Design
    {
        [Key]
        public int Design_Id { get; set; }

        [Required]
        public int Design_VehicleId { get; set; }

        [Required]
        public int Design_CentralDesignId { get; set; }

        [Required]
        public int Design_LateralDesignId { get; set; }

        [Required]
        public int Design_ImageId { get; set; }

        [Required]
        public bool Design_Status { get; set; }


        [ForeignKey("Design_VehicleId")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("Design_CentralDesignId")]
        public CentralDesign CentralDesign { get; set; }

        [ForeignKey("Design_LateralDesignId")]
        public LateralDesign LateralDesign { get; set; }

        [ForeignKey("Design_ImageId")]
        public Image Image { get; set; }

        public ICollection<Product> Products { get; set; }
    }

}
