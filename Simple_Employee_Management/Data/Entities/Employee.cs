using System;
using System.Collections.Generic;

namespace Simple_Employee_Management.Data.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }
}
