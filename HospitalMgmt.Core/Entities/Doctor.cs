using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMgmt.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ConsultationFee { get; set; }

        public DateTime DateOfJoining { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department? Department { get; set; }
    }
}