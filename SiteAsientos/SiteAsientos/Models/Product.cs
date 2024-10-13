using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        [Required]
        public int Product_OrderId { get; set; }

        [Required]
        public int Product_DesignId { get; set; }

        [Required]
        public int Product_SupplierId { get; set; }

        [Required]
        public bool Product_Embroidery { get; set; }


        [ForeignKey("Product_OrderId")]
        public Order Order { get; set; }

        [ForeignKey("Product_DesignId")]
        public Design Design { get; set; }

        [ForeignKey("Product_SupplierId")]
        public Supplier Supplier { get; set; }
    }

}
