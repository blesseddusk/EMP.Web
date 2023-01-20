namespace EMP.Web.Models.Data
{
    public class Employee
    {
        public long Id { get; set; }

        public string EmpName { get; set; } = null!;

        public long DeptId { get; set; }

        public string DeptName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
