using Microsoft.AspNetCore.Mvc;
using MyFAPWebApp.Models;

namespace MyFAPWebApp.Controllers
{
    public class StudentClassesController : Controller
    {
        private readonly MyFapContext context = new();
        public IActionResult Index(string class_id, string subject_id)
        {
            string studentid = "HE153453";
            var classes = from s in context.Students
                          join se in context.StudentEnrollments on s.StudentId equals se.StudentId
                          join c in context.Classes on se.ClassId equals c.ClassId
                          join su in context.Subjects on se.SubjectId equals su.SubjectId
                          join te in context.TeacherEnrollments on new { se.ClassId, se.SubjectId } equals new { te.ClassId, te.SubjectId } into gj
                          from subte in gj.DefaultIfEmpty()
                          join t in context.Teachers on subte.TeacherId equals t.TeacherId into gj2
                          from teacher in gj2.DefaultIfEmpty()
                          where s.StudentId == studentid
                          select new { c.ClassId, c.ClassName, su.SubjectId, su.SubjectName, teacher_id = teacher.TeacherId, teacher_name = teacher.TeacherName };
            ViewBag.Classes = classes.ToList();
            if(class_id!= null )
            {
                var member = from se in context.StudentEnrollments
                             join s in context.Students on se.StudentId equals s.StudentId
                             where se.ClassId == class_id && se.SubjectId == subject_id
                             select new {s.StudentId, s.StudentName };
                ViewBag.Member = member.ToList();
                ViewBag.class_id= class_id.Trim();
                ViewBag.subject_id = subject_id.Trim();
            }
            return View();
        }
    }
}
