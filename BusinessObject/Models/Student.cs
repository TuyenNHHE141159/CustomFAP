using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Student
{
    public string StudentId { get; set; }

    public string StudentName { get; set; }

    public virtual ICollection<Mark> Marks { get; } = new List<Mark>();

    public virtual ICollection<StudentEnrollment> StudentEnrollments { get; } = new List<StudentEnrollment>();

    public virtual Account StudentNavigation { get; set; }
}
