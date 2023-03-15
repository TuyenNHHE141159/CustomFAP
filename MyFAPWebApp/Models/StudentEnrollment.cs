using System;
using System.Collections.Generic;

namespace MyFAPWebApp.Models;

public partial class StudentEnrollment
{
    public string StudentId { get; set; }

    public string ClassId { get; set; }

    public string SubjectId { get; set; }

    public virtual Class Class { get; set; }

    public virtual Student Student { get; set; }

    public virtual Subject Subject { get; set; }
}
