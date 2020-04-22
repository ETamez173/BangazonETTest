using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonETTest.Data;
using BangazonETTest.Models;
using BangazonETTest.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace BangazonETTest.Controllers
{
    public class EmployeeController : Controller
    {

        // this is called Dependancy Injection (DI) about the 32 min point on the 042020 Entity Framework video
        // _context represent the Database and we now have access to it 
        // DI makes things very testable and flexible
        // Our contollers have a have reference to our database via the context field
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Employee
        public ActionResult Index()
        {
            // This queries the Employee table. Using the `Include` method will join the related entities
            var employees = _context.Employee
                .Include(e => e.Computer)
                .Include(e => e.Department)
                .ToList();

            return View(employees);
        }
            // GET: Employee/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create

            // Adam goes over the Select at 50 min point on 042020 talk
        public ActionResult Create()
        {

            // Entity Framework wil always return data from the DB in the form of data models
            // If data models are no the type object you want for your view then you can use the "SELECT" method
            // Example - we dont want a list of Computer and Dept Objects here. We want a list of 
            // SelectListItems.
            var allComputers = _context.Computer
                .Select(d => new SelectListItem() { Text = d.Model, Value = d.Id.ToString() })
                .ToList();
            var allDepartments = _context.Department
                .Select(d => new SelectListItem() { Text = d.Name, Value = d.Id.ToString() })
                .ToList();

            var viewModel = new EmployeeCreateViewModel();

            viewModel.DepartmentOptions = allDepartments;
            viewModel.ComputerOptions = allComputers;

            return View(viewModel);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeCreateViewModel employeeViewModel)
        {
            try
            {
   
                // Everything we do in EF has to do with Datamodels
                // we have to create a datamodel from a viewmodel (see 1.07 min point on 042020 EF video)
                var employee = new Employee
                {
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Email = employeeViewModel.Email,
                    DepartmentId = employeeViewModel.DepartmentId,
                    ComputerId = employeeViewModel.ComputerId

                };

                _context.Employee.Add(employee);
                // savechanges helps with performance as it opens one connection to DB and commit all of the changes
                // we have made up until that point to the DB
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {


            var allComputers = _context.Computer
                .Select(c => new SelectListItem() { Text = c.Model, Value = c.Id.ToString() })
                .ToList();
            var allDepartments = _context.Department
                .Select(d => new SelectListItem() { Text = d.Name, Value = d.Id.ToString() })
                .ToList();


            var employee = _context.Employee.FirstOrDefault(e => e.Id == id);

            var viewModel = new EmployeeCreateViewModel()
            {
              
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                ComputerId = employee.ComputerId,
                ComputerOptions = allComputers,
                DepartmentOptions = allDepartments

            };

            return View();
        }


        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}