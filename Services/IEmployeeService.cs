using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain;

namespace HRM.Services
{
   public interface IEmployeeService
    {
        void CreateEmployee(Employee employee);
        IList<Employee> GetAllEmployees();
    }
}
