using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IEmployeeRepo employeeRepo, IDepartmentRepo departmentRepo)
        {
            EmployeeRepo = employeeRepo;
            DepartmentRepo = departmentRepo;

        }

        public IEmployeeRepo EmployeeRepo { get; set ; }
        public IDepartmentRepo DepartmentRepo { get  ; set ; }
    }
}
