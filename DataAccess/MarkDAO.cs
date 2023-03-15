using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MarkDAO
    {
        private readonly DbContext _dbContext;
        public MarkDAO(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Mark mark)
        {
            _dbContext.Set<Mark>().Add(mark);
            _dbContext.SaveChanges();
        }
        public int Update(Mark mark)
        {
           
            _dbContext.Entry(mark).State = EntityState.Modified;
           return _dbContext.SaveChanges();
        }
        public int UpdateOrAdd(Mark mark)
        {
            //var existingStudentMark1=_dbContext.Set<Mark>().FirstOrDefault(s=> s.StudentId==mark.StudentId && s.SubjectId==mark.SubjectId);
            var existingStudentMark = GetById(mark.SubjectId, mark.StudentId);
            if (existingStudentMark != null)
            {
                _dbContext.Entry(existingStudentMark).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Add(mark);
            }
          return  _dbContext.SaveChanges();
        }
        public void Delete(Mark mark)
        {
            _dbContext.Set<Mark>().Remove(mark);
            _dbContext.SaveChanges();
        }

        public Mark GetById(string subject_id, string student_id)
        {
            return _dbContext.Set<Mark>().Find(subject_id, student_id);
        }

        public List<Mark> GetAll()
        {
            return _dbContext.Set<Mark>().ToList();
        }
    }
}
