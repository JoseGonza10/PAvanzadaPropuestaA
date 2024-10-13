using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Material
    {
        [Key]
        public int Material_Id { get; set; }

        [Required, MaxLength(32)]
        public string Material_Name { get; set; }

        [Required]
        public bool Material_Status { get; set; }


        public ICollection<LateralDesign> LateralDesigns { get; set; }
        public ICollection<CentralDesign> CentralDesigns { get; set; }
    }
}
