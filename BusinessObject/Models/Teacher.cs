using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Teacher
{
    public string TeacherId { get; set; }

    public string TeacherName { get; set; }

    public virtual ICollection<TeacherEnrollment> TeacherEnrollments { get; } = new List<TeacherEnrollment>();

    public virtual Account TeacherNavigation { get; set; }
}
