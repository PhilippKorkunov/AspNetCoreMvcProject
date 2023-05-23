using DepartmentsWebApp.Models.DepartmentModel;
using DepartmentsWebApp.Models.EmployeeModel;
using TestDBLib.Entities;

namespace DepartmentsWebApp.Services
{
    public static class StaticService
    {
        public static DepartmentViewModel ToViewModel(this Department department, List<DepartmentViewModel>? childrenDepartments, List<EmployeeViewModel>? employees)
                                                            => new(department, childrenDepartments, employees);

        public static DepartmentEditModel ToEditModel(this Department department) => new(department);

        public static EmployeeViewModel ToViewModel(this Employee employee) => new(employee);

        public static EmployeeEditModel ToEditModel(this Employee employee, List<Guid>? departmentsIDs = null) => new(employee, departmentsIDs);

    }
}
