using System;
using System.Collections.Generic;

namespace EMP.Web.Models;

public partial class TblDepartment
{
    public long Id { get; set; }

    public string DeptName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<TblEmployee> TblEmployees { get; } = new List<TblEmployee>();
}
