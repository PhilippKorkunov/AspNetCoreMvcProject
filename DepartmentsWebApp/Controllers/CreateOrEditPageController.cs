using DepartmentsWebApp.Models;
using DepartmentsWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using TestDBLib.Entities;

namespace DepartmentsWebApp.Controllers
{
    public class CreateOrEditPageController : Controller
    {
        private readonly Service service;

        public CreateOrEditPageController(Service service)
        {
            this.service = service;
        }

        public async Task<IActionResult> CreateOrEditDepartment(Guid id, Guid? parentDepartmentID, string name, string? code) //Страница для внесения
                                                                                                                              //данных о конкретном департаменте
        {
            /*ViewBag.Meassage = string.Empty;*/
            await using var departmentRepository = service.dataManager.DepartmentRepository;
            var departments = await departmentRepository.GetAsync(predicate: x => x.ID == id); // департаменты с нужным Id
            Department? department = null;

            if (departments is not null && departments.Any()) // Если департамент уже есть в БД - то Update, иначе Insert 
            {
                department = departments.FirstOrDefault();  
                if (department is not null)
                {
                    department.Name = name;
                    department.Code = code;
                    department.ParentDepartmentID = parentDepartmentID;
                    department.ID = id;

                    await departmentRepository.UpdateAsync(department);
                    return View(department.ToEditModel());
                }

                return View();
            }
            else
            {
                
                department = new()
                {
                    Name = name,
                    Code = code,
                    ParentDepartmentID = parentDepartmentID,
                    ID = id
                };
/*                if (name is null)
                {
                    ViewBag.Message = "Name is null";
                }*/
                if (name is not null)
                {
                    await departmentRepository.InsertAsync(department);
                }
                
                return View(department.ToEditModel());
            }
        }


        public async Task<IActionResult> CreateOrEditEmployee(decimal id, Guid departmentID, string surname, string firstname, 
            string? patronymic, DateTime dateOfBirth, string? docSeries, string? docNumber, string position) //Страница для внесения
                                                                                                             //данных о конкретном работнике
        {
            await using var employeeRepository = service.dataManager.EmployeeRepository;
            var employees = await employeeRepository.GetAsync(predicate: x => x.ID == id);
            if (employees is not null && employees.Any()) // Если работник уже есть в БД - то Update, иначе Insert 
            {
                var employee = employees.FirstOrDefault();
                if (employee is not null)
                {
                    employee.ID = id;
                    employee.DepartmentID = departmentID;
                    employee.Position = position;
                    employee.SurName = surname;
                    employee.FirstName = firstname;
                    employee.Patronymic = patronymic;
                    employee.DateOfBirth = dateOfBirth;
                    employee.DocSeries = docSeries;
                    employee.DocNumber = docNumber;
                    employee.Position = position;


                    await employeeRepository.UpdateAsync(employee);
                    return View(employee.ToEditModel());
                }

                return View();
            }
            else
            {
                if (surname is null) { }
                var employee = new Employee()
                {
                    DepartmentID = departmentID,
                    Position = position,
                    SurName = surname,
                    FirstName = firstname,
                    Patronymic = patronymic,
                    DateOfBirth = dateOfBirth,
                    DocSeries = docSeries,
                    DocNumber = docNumber,
                };

                if (surname is not null)
                {
                    await employeeRepository.InsertAsync(employee);
                }

                return View(employee.ToEditModel());
            }
        }
    }
}
