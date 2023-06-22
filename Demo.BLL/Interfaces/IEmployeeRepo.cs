using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepo:IGenericRepo<Employee>
    {
        Task<IEnumerable<Department>> Search(String name);

    }
}
