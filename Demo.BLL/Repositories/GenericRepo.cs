using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepo<T>:IGenericRepo<T> where T : class
    {

        private MVCDbContext _context;
        public GenericRepo(MVCDbContext context)
        {
            _context = context;
        }
        public int Add(T item)
        {
            _context.Set<T>().Add(item);
            return _context.SaveChanges();
        }

        public int Delete(T item)
        {
            _context.Set<T>().Remove(item);
            return _context.SaveChanges();
        }

        public T Get(int id)
        {
            //var department= _context.Departments.Local.Where(d=>d.Id==id).FirstOrDefault();
            //if(department is null)
            //    department= _context.Departments.Where(d => d.Id == id).FirstOrDefault();
            //return department;

            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public int Update(T Item)
        {
            _context.Set<T>().Update(Item);
            return _context.SaveChanges();
        }
    }
}
