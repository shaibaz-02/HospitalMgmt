namespace HospitalMgmt.Application.DTOs
{
     // No Id (database generates it), no CreatedOn (the system sets it)
     public class CreateDepartmentDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; } = true;
    }

}