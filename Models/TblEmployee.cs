using System;
using System.Collections.Generic;

namespace EMP.Web.Models;

public partial class TblEmployee
{
    public long Id { get; set; }

    public string EmpName { get; set; } = null!;

    public long DeptId { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TblDepartment Dept { get; set; } = null!;
}
