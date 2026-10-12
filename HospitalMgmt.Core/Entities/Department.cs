using System.ComponentModel.DataAnnotations;

namespace HospitalMgmt.Core.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Location { get; set; }   // e.g. "Building A, Floor 2"

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        // Navigation: one department has many doctors
        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}