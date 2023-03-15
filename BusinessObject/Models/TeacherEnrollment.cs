using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class TeacherEnrollment
{
    public string TeacherId { get; set; }

    public string ClassId { get; set; }

    public string SubjectId { get; set; }

    public virtual Class Class { get; set; }

    public virtual Subject Subject { get; set; }

    public virtual Teacher Teacher { get; set; }
}
