using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SiteAsientos.Models
{

    public class Visit
    {
        [Key]
        public int Visit_Id { get; set; }

        [Required, MaxLength(32)]
        [DisplayName("Código de Visita")]
        public string Visit_Code { get; set; }

        [Required]
        public int Visit_OrderId { get; set; }

        [Required]
        public int Visit_EmployeeId { get; set; }

        [Required, MaxLength(32)]
        [DisplayName("Tipo de Visita")]
        public string Visit_Type { get; set; }
        [DisplayName("Fecha de Visita")]
        public DateTime? Visit_Date { get; set; }

        [Required]
        [DisplayName("Estado")]
        public bool Visit_Status { get; set; }
 
        [ForeignKey("Visit_OrderId")]
        public Order Order { get; set; }

        [ForeignKey("Visit_EmployeeId")]
        public Employee Employee { get; set; }
    }


}
