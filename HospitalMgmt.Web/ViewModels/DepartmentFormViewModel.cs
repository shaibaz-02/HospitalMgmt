using System.ComponentModel.DataAnnotations;
namespace HospitalMgmt.Web.ViewModels
{
    public class DepartmentFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100)]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Location { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}