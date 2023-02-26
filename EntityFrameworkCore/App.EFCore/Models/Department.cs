using System;
using System.Collections.Generic;

namespace App.EFCore.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
