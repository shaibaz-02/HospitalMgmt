namespace HospitalMgmt.Application.DTOs
{
     // Has Id (to find the record), but no CreatedOn (it must never change)
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
    }
}