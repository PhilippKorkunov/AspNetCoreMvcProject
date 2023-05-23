using BusinessLayerLib.Implementations;
using DepartmentsWebApp.Models;
using DepartmentsWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestDBLib.Entities;

namespace DepartmentsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Service service;

        public HomeController(Service service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index() //Стартовая страница отображает главные департаменты
        {
            await using var departmentsRepository = service.dataManager.DepartmentRepository;

            var parentDepartments = await departmentsRepository.GetAsync(predicate: x => x.ParentDepartmentID == null);//Главные департаменты
            if (parentDepartments is not null)
            {
                return View(from parent in parentDepartments
                            select parent.ToViewModel(null, null)); //Из представления от БД к View
            }

            return BadRequest();
        }


        public async Task<IActionResult> Department(Guid? id, Guid? targetDepartmentId = null, int? targetEmployeeId = null) // Страница с информацией по департаменту с ID = id
        {
            await using var departmentsRepository = service.dataManager.DepartmentRepository;
            await using var employeeRepository = service.dataManager.EmployeeRepository;

            if (targetDepartmentId is not null) { await DeleteDepartmentAsync((Guid)targetDepartmentId, departmentsRepository); } // Удаление департамента, если запрос содержит Id департамента

            if (targetEmployeeId is not null) { await DeleteEmployeeAsync((int)targetEmployeeId, employeeRepository); } // Удаление сотрудника, если запрос содержит Id сотрудника

            var departments = await departmentsRepository.GetAsync(predicate: x => x.ID == id);
            if (departments is not null && departments.Any())
            {
                var department = departments.FirstOrDefault();

                var childrenDepartments = from children in await departmentsRepository.GetAsync(predicate: x => x.ParentDepartmentID == id)
                                               select children.ToViewModel(null, null); //Дочерние департаменты

                var employees = from employee in await employeeRepository.GetAsync(predicate: x => x.DepartmentID == id)
                                select employee.ToViewModel(); //сотрудники департамента

                if (department is not null)
                {
                    return View(department.ToViewModel(childrenDepartments.ToList(), employees.ToList()));
                }
            }
            else if (id is null)
            {
                return View("Index");
            }

            return BadRequest();
        }

        private async Task DeleteDepartmentAsync(Guid deleteId, EFRepository<Department> departmentsRepository)
        {
            var deleteDepartments = await departmentsRepository.GetAsync(predicate: x => x.ID == deleteId);
            if (deleteDepartments is not null && deleteDepartments.Any())
            {
                var deleteDepartment = deleteDepartments.FirstOrDefault();
                if (deleteDepartment is not null) { _ = await departmentsRepository.DeleteAsync(deleteDepartment); }
            }
        }

        private async Task DeleteEmployeeAsync(int deleteId, EFRepository<Employee> employeeRepository)
        {
            var deleteEmployees = await employeeRepository.GetAsync(predicate: x => x.ID == deleteId);
            if (deleteEmployees is not null && deleteEmployees.Any())
            {
                var deletedEmployee = deleteEmployees.FirstOrDefault();
                if (deletedEmployee is not null) { _ = await employeeRepository.DeleteAsync(deletedEmployee); }
            }
        }
 

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}