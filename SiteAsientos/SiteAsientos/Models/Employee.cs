using System.ComponentModel.DataAnnotations;

namespace SiteAsientos.Models
{
    public class Employee
    {
        [Key]
        public int Employee_Id { get; set; }

        [Required, MaxLength(32)]
        public string Employee_Name { get; set; }

        [MaxLength(32)]
        public string Employee_MiddleName { get; set; }

        [Required, MaxLength(32)]
        public string Employee_LastName { get; set; }

        [Required, MaxLength(64)]
        public string Employee_Password { get; set; }

        [Required, MaxLength(256)]
        public string Employee_Email { get; set; }

        [Required, MaxLength(32)]
        public string Employee_Phone { get; set; }

		[Required, MaxLength(32)]
		public string Employee_Type { get; set; }

		[Required]
        public bool Employee_Status { get; set; }

        // Relaciones
        public ICollection<Visit> Visits { get; set; }
    }
}
