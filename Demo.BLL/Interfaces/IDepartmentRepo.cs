using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepo:IGenericRepo<Department>
    {
        //Task<IEnumerable<Department>> Search(String name);

    }
}
