using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Image
    {
        [Key]
        public int Image_Id { get; set; }

        public byte[] Image_Content { get; set; }


        public ICollection<Design> Designs { get; set; }
    }
}
