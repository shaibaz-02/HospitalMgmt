using System.ComponentModel.DataAnnotations;


namespace HospitalMgmt.Web.ViewModels
{
     public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Location { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
    }
}