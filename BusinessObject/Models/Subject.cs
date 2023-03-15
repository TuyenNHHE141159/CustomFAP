using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Subject
{
    public string SubjectId { get; set; }

    public string SubjectName { get; set; }

    public virtual ICollection<Mark> Marks { get; } = new List<Mark>();

    public virtual ICollection<StudentEnrollment> StudentEnrollments { get; } = new List<StudentEnrollment>();

    public virtual ICollection<TeacherEnrollment> TeacherEnrollments { get; } = new List<TeacherEnrollment>();
}
