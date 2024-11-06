using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Employee
    {
        [Key]
        public int Employee_Id { get; set; }

        [DisplayName("Nombre")]
        [Required, MaxLength(32)]
        public string Employee_Name { get; set; }

        [DisplayName("1° Apellido")]
        [MaxLength(32)]
        public string Employee_MiddleName { get; set; }

        [DisplayName("2° Apellido")]
        [Required, MaxLength(32)]
        public string Employee_LastName { get; set; }

        [DisplayName("Contraseña")]
        [Required, MaxLength(64)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[!.*\-])[A-Za-z\d!.*\-]{8,}$",
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, una letra mayúscula y un carácter especial (!, *, ., -).")]
        public string Employee_Password { get; set; }

        [DisplayName("Email")]
        [Required, MaxLength(256), EmailAddress]
        [Remote("EmailExists", "Employee", ErrorMessage = "Este correo electrónico ya se encuentra en uso")]
        public string Employee_Email { get; set; }

        [DisplayName("Teléfono")]
        [Required, MaxLength(32)]
        public string Employee_Phone { get; set; }

        [DisplayName("Rol")]
        [Required, MaxLength(32)]
		public string Employee_Type { get; set; }

        [DisplayName("Estado")]
		[Required]
        public bool Employee_Status { get; set; }

        // Relaciones
        public ICollection<Visit>? Visits { get; set; }
    }
}
