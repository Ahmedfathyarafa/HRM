using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Domain;
using HRM.Services;
using HRM.Web.Models;

namespace HRM.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }
        [HttpGet]
        public ActionResult List()
        {
            var employees = this._employeeService.GetAllEmployees();
            var model = new List<EmployeeModel>();
            // Use AutoMapper for object mapping
            foreach (var emp in employees)
            {
                var employeeModel = new EmployeeModel() { Id = emp.Id, Age = emp.Age, FirstName = emp.FirstName, LastName = emp.LastName };
                model.Add(employeeModel);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee() { FirstName = model.FirstName, LastName = model.LastName, Age = model.Age };
                this._employeeService.CreateEmployee(employee);
                return RedirectToAction("List");
            }
            return View(model);
        }

    }
}