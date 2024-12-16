using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Image
    {
        [Key]
        public int Image_Id { get; set; }

        public byte[]? Image_Content { get; set; }

        [Required]
        public int? Image_DesignId { get; set; }

        //Relaciones
        public Design? Design { get; set; }
    }
}
