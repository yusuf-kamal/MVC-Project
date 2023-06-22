using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepo :GenericRepo<Department>, IDepartmentRepo
    {
        private readonly MVCDbContext _context;
        public DepartmentRepo(MVCDbContext context):base(context) 
        {
            _context= context;
        }

        //public async Task<IEnumerable<Department>> Search(string name)

        //    => await _context.Departments.Where(e => e.Name.Contains(name)).ToListAsync();


    }
}
