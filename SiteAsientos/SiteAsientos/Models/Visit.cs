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
        public string? Visit_Code { get; set; }
        [DisplayName("Orden")]
        public int Visit_OrderId { get; set; }
        [DisplayName("Emppleado")]
        public int Visit_EmployeeId { get; set; }
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
    }


}
