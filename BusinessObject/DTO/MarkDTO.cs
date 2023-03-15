using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class MarkDTO:Mark
    {
        public string StudentName { get; set; }
        public MarkDTO() { }

    }
}
