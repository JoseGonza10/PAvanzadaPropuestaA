using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{

    public class Visit
    {
        [Key]
        public int Visit_Id { get; set; }

        [Required, MaxLength(32)]
        public string Visit_Code { get; set; }

        [Required]
        public int Visit_OrderId { get; set; }

        [Required]
        public int Visit_EmployeeId { get; set; }

        [Required, MaxLength(32)]
        public string Vist_Type { get; set; }

        public DateTime? Visit_Date { get; set; }

        [Required]
        public bool Visit_Status { get; set; }
 
        [ForeignKey("Visit_OrderId")]
        public Order Order { get; set; }

        [ForeignKey("Visit_EmployeeId")]
        public Employee Employee { get; set; }
    }


}
