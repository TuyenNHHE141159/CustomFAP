using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string Title { get; set; }

    public string Desciption { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedBy { get; set; }
}
