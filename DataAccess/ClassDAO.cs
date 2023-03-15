using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ClassDAO
    {
        private readonly DbContext _dbContext;
        public ClassDAO(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Class @class)
        {
            _dbContext.Set<Class>().Add(@class);
            _dbContext.SaveChanges();
        }
        public void Update(Class @class)
        {
            _dbContext.Entry(@class).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Class @class)
        {
            _dbContext.Set<Class>().Remove(@class);
            _dbContext.SaveChanges();
        }

        public Class GetById(string id)
        {
            return _dbContext.Set<Class>().Find(id);
        }

        public List<Class> GetAll()
        {
            return _dbContext.Set<Class>().ToList();
        }
    }
}

