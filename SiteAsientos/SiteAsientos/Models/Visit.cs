using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{

    public class Visit
    {
        [Key]
        public int Visit_Id { get; set; }

        [Required, MaxLength(32)]
<<<<<<< Updated upstream
        public string Visit_Code { get; set; }
=======
        [DisplayName("Código de Visita")]
        public string? Visit_Code { get; set; }
>>>>>>> Stashed changes

        [DisplayName("Orden")]
        public int Visit_OrderId { get; set; }

        [DisplayName("Emppleado")]
        public int Visit_EmployeeId { get; set; }

<<<<<<< Updated upstream
        [Required, MaxLength(32)]
        public string Vist_Type { get; set; }

        public DateTime? Visit_Date { get; set; }

        [Required]
        public bool Visit_Status { get; set; }
 
        [ForeignKey("Visit_OrderId")]
        public Order Order { get; set; }

        [ForeignKey("Visit_EmployeeId")]
        public Employee Employee { get; set; }
=======
        [MaxLength(32)]
        [DisplayName("Tipo de Visita")]
        public string? Visit_Type { get; set; }
        [Required(ErrorMessage ="Se requiere este campo")]
        [DisplayName("Fecha de Visita")]
        public DateTime? Visit_Date { get; set; }

        [DisplayName("Estado")]
        public bool Visit_Status { get; set; } = true;
        
        //Relaciones
        public Order? Order { get; set; }
        public Employee? Employee { get; set; }
>>>>>>> Stashed changes
    }


}
