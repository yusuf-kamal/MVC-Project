using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepo  EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }
    }
}
