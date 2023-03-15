using System;
using System.Collections.Generic;

namespace MyFAPWebApp.Models;

public partial class Class
{
    public string ClassId { get; set; }

    public string ClassName { get; set; }

    public virtual ICollection<StudentEnrollment> StudentEnrollments { get; } = new List<StudentEnrollment>();

    public virtual ICollection<TeacherEnrollment> TeacherEnrollments { get; } = new List<TeacherEnrollment>();
}
