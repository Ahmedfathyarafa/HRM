using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Data.Repository;
using HRM.Domain;

namespace HRM.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public void CreateEmployee(Employee employee)
        {
            this._employeeRepository.Insert(employee);
        }

        public IList<Employee> GetAllEmployees()
        {
            return this._employeeRepository.Table.ToList();
        }
    }
}
