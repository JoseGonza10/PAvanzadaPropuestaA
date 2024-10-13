using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Supplier
    {
        [Key]
        public int Supplier_Id { get; set; }

        [Required, MaxLength(32)]
        public string Supplier_Name { get; set; }

        [Required, MaxLength(256)]
        public string Supplier_Address { get; set; }

        [Required, MaxLength(32)]
        public string Supplier_Phone { get; set; }

        [Required, MaxLength(256)]
        public string Supplier_Email { get; set; }

        public DateTime? Supplier_DateAdded { get; set; }

        [Required]
        public bool Supplier_Status { get; set; }


        public ICollection<Product> Products { get; set; }
    }
}
