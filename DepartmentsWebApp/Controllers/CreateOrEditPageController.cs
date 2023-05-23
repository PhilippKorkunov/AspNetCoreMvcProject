using DepartmentsWebApp.Models;
using DepartmentsWebApp.Models.DepartmentModel;
using DepartmentsWebApp.Models.EmployeeModel;
using DepartmentsWebApp.Services;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> CreateOrEditDepartment(Guid id, Guid? parentDepartmentID, string name, string? code,
                                                                bool isFromDepartment) //Страница для внесения данных о конкретном департаменте
        {
            ViewBag.Message = null;
            await using var departmentsRepository = service.dataManager.DepartmentRepository;

            var headDepartments = await departmentsRepository.GetAsync(predicate: x => x.ParentDepartmentID == null);
            List<Guid> departmnetsIDs = parentDepartmentID == null && headDepartments is not null && headDepartments.Count() == 1 ?
                new List<Guid>() : (from x in await departmentsRepository.GetAsync() select x.ID).ToList();

            DepartmentEditModel departmentEditModel = new(id, parentDepartmentID, code, name, departmnetsIDs);
            if (isFromDepartment) { return View(departmentEditModel); } //если перешли со страницы, то просто отображается форма

            var department = await departmentsRepository.GetFirstOrDefault(id.ToString());
            var newDepartment = (Department?)CreateNewEntity(department, departmentEditModel);

            if (newDepartment is null) { return View(departmentEditModel); } // Валидация не пройдена

            var isEqual = departmentEditModel.Equals(DepartmentEditModel.FromEntity(newDepartment)); // проверка на равенство

            int affectedRows = isEqual ? await departmentsRepository.UpdateAsync(newDepartment): 
                                         await departmentsRepository.InsertAsync(newDepartment);

            departmentEditModel.ID = newDepartment.ID; //обновление ID как в БД

            GenerateMessage(affectedRows, isEqual); //Сообщения пользователю
            return View(departmentEditModel);
        }

        public async Task<IActionResult> CreateOrEditEmployee(int id, Guid departmentID, string surname, string firstname,
            string? patronymic, DateTime dateOfBirth, string? docSeries, string? docNumber, string position, bool isFromDepartment = false) //Страница для внесения
                                                                                                             //данных о конкретном работнике
        {
            ViewBag.Message = null;
            await using var departmentsRepository = service.dataManager.DepartmentRepository;
            await using var employeeRepository = service.dataManager.EmployeeRepository;

            List<Guid> departmnetsIDs = (from x in await departmentsRepository.GetAsync() select x.ID).ToList();

            EmployeeEditModel employeeEditModel = new (id, departmentID, surname, firstname, patronymic, dateOfBirth, docSeries,
                                                       docNumber, position, departmnetsIDs);

            if (isFromDepartment) { return View(employeeEditModel); } //если перешли со страницы, то просто отображается форма

            var employee = await employeeRepository.GetFirstOrDefault(id.ToString());
            var newEmployee = (Employee?)CreateNewEntity(employee, employeeEditModel);

            if (newEmployee is null) { return View(employeeEditModel); } // Валидация не пройдена

            var isEqual = newEmployee.ID > 0; // если ID == 0, то это новая запись

            int affectedRows = isEqual ? await employeeRepository.UpdateAsync(newEmployee) :
                                         await employeeRepository.InsertAsync(newEmployee);

            employeeEditModel.ID = newEmployee.ID; //обновление ID как в БД

            GenerateMessage(affectedRows, isEqual);
            return View(employeeEditModel);
        }

        public Entity? CreateNewEntity(Entity? entity, IEditModel editModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Validation error";
                return null; // возвращается null при проваленной валидации
            }

            if (entity is not null) 
            {
                editModel.CopyPropertiesTo(entity); // обновляем свойтва в соотвествии с формой
                return entity;
            }
            else
            {
                return editModel.ToEntity();
            }
        }

        public void GenerateMessage(int affectedRows, bool isEqual)
        {
            if (ViewBag.Message is not null) { return; }
            ViewBag.Message = affectedRows > 0 ? isEqual ? "Successfully updated" : "Successfully inserted"
                                               : "Something went wrong. Try once again";
        }
    }
}
