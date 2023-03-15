using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TeacherDAO
    {
        private readonly DbContext _dbContext;
        public TeacherDAO(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Teacher teacher)
        {
            _dbContext.Set<Teacher>().Add(teacher);
            _dbContext.SaveChanges();
        }
        public void Update(Teacher teacher)
        {
            _dbContext.Entry(teacher).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Teacher teacher)
        {
            _dbContext.Set<Teacher>().Remove(teacher);
            _dbContext.SaveChanges();
        }

        public Teacher GetById(string id)
        {
            return _dbContext.Set<Teacher>().Find(id);
        }

        public List<Teacher> GetAll()
        {
            return _dbContext.Set<Teacher>().ToList();
        }
    }
}
