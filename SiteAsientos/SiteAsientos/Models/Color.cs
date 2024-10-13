using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Color
    {
        [Key]
        public int Color_Id { get; set; }

        [Required, MaxLength(32)]
        public string Color_Name { get; set; }

        public ICollection<LateralDesign> LateralDesigns { get; set; }
        public ICollection<CentralDesign> CentralDesigns { get; set; }
    }
}
