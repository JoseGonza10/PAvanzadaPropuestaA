using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Employee
    {
        [Key]
        public int Employee_Id { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [DisplayName("Nombre")]
        public string? Employee_Name { get; set; }
        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [DisplayName("Primer Apellido")]
        public string? Employee_MiddleName { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [DisplayName("Segundo Apellido")]
        public string? Employee_LastName { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(64)]
        [DisplayName("Contraseña")]
        public string? Employee_Password { get; set; }

        [Required, MaxLength(256)]
        [EmailAddress]
        [Remote("EmailExists", "Employee", AdditionalFields = "Employee_Id", HttpMethod = "POST", ErrorMessage = "Este correo electrónico ya se encuentra en uso")]
        [DisplayName("Correo Electrónico")]
        public string? Employee_Email { get; set; }

        [Required(ErrorMessage = "Se requiere este campo"), MaxLength(8)]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El numero de telefono es incorrecto")]
        [Remote("PhoneExists", "Employee", AdditionalFields = "Employee_Id", HttpMethod = "POST", ErrorMessage = "Este telefono ya esta siendo usado en otro empleado")]
        [DisplayName("Telefono")]
        public string? Employee_Phone { get; set; }

		[Required(ErrorMessage = "Se requiere este campo"), MaxLength(32)]
        [DisplayName("Rol")]
        public string[]? Employee_Type { get; set; }

		[Required]
        [DisplayName("Estado")]
        public bool Employee_Status { get; set; }

        // Relaciones
        public ICollection<Visit>? Visits { get; set; }
    }
}
