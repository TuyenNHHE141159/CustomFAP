using Microsoft.AspNetCore.Mvc;
using MyFAPWebApp.Models;
using MyFAPWebApp.Models.DTO;

namespace MyFAPWebApp.Controllers
{
    public class StudentHomeController : Controller
    {
        private readonly MyFapContext myFapContext= new MyFapContext();

        public IActionResult Index(string student_id,string subject_id)
        {
            string studentId = "HE153453";
            ViewData["StudentID"] = studentId;
            var classesOfStudent = from s in myFapContext.Students
                                   join se in myFapContext.StudentEnrollments on s.StudentId equals se.StudentId
                                   join c in myFapContext.Classes on se.ClassId equals c.ClassId
                                   join su in myFapContext.Subjects on se.SubjectId equals su.SubjectId
                                   where s.StudentId == studentId
                                   select new
                                   {
                                       StudentId = studentId,
                                       StudentName = s.StudentName,
                                       SubjectId = se.SubjectId,
                                       SubjectName = su.SubjectName,
                                       ClassId = c.ClassId,
                                       ClassName = c.ClassName,

                                   };
            List<ClassesOfStudent> classes= new List<ClassesOfStudent>();
            foreach(var c in classesOfStudent)
            {
                ClassesOfStudent cos = new ClassesOfStudent();
                cos.StudentId = c.StudentId;
                cos.StudentName = c.StudentName;
                cos.SubjectId = c.SubjectId;
                cos.SubjectName = c.SubjectName;
                cos.ClassId = c.ClassId;
                cos.ClassName = c.ClassName;
                classes.Add(cos);
            }         
            Mark mark = new Mark();
            if(student_id != null && subject_id!= null)
            {
                mark=myFapContext.Marks.Where(x=>x.SubjectId==subject_id && x.StudentId==student_id).FirstOrDefault();
                if (mark != null)
                {
                    ViewData["Mark"]=mark;
                    double avg=(double) (mark.Lab + mark.Fe + mark.ProgrestTest + mark.Pe) / 4;
                    avg = Math.Round(avg, 1);
                    string status = "Not Passed";
                    if(avg >=5 )
                    {
                        status = "Passed";
                    }
                    ViewData["Avg"] = avg;
                    ViewData["Status"] = status;

                }              
            }                  
            return View(classes);
        }

    }
}
