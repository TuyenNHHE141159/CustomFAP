using System;
using System.Collections.Generic;

namespace MyFAPWebApp.Models;

public partial class Mark
{
    public string StudentId { get; set; }

    public string SubjectId { get; set; }

    public double? Lab { get; set; }

    public double? ProgrestTest { get; set; }

    public double? Pe { get; set; }

    public double? Fe { get; set; }

    public virtual Student Student { get; set; }

    public virtual Subject Subject { get; set; }
}
