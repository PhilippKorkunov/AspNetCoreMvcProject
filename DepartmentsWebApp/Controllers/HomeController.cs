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


        public async Task<IActionResult> Department(Guid? id) // Страница с информацией по департаменту с ID = id
        {
            await using var departmentsRepository = service.dataManager.DepartmentRepository;
            await using var employeeRepository = service.dataManager.EmployeeRepository;

            IQueryable<Department>? departments = await departmentsRepository.GetAsync(predicate: x => x.ID == id);
            if (departments is not null && departments.Any())
            {
                var childrenDepartmentsViews = from children in await departmentsRepository.GetAsync(predicate: x => x.ParentDepartmentID == id)
                                               select children.ToViewModel(null, null); //Дочерние департаменты


                var employees = from employee in await employeeRepository.GetAsync(predicate: x => x.DepartmentID == id)
                                select employee.ToViewModel(); //сотрудники департамента

                return View(departments.FirstOrDefault().ToViewModel(childrenDepartmentsViews.ToList(), employees.ToList()));
            }
            else if (id is null)
            {
                return View("Index");
            }

            return BadRequest();
        }

        /*[HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteDepartmentAsync(Guid id)
        {
            await using var departmentsRepository = service.dataManager.DepartmentRepository;
            
            var departments = await departmentsRepository.GetAsync(predicate: x => x.ID == id);

            if (departments is not null)
            {
                var department = departments.FirstOrDefault();
                if (department is not null)
                {
                    await departmentsRepository.DeleteAsync(department);
                    return RedirectToAction("Department", department.ParentDepartmentID);
                }
            }
            return BadRequest();
        }*/




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