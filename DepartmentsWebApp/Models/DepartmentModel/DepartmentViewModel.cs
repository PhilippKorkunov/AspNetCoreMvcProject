using DepartmentsWebApp.Models.EmployeeModel;
using TestDBLib.Entities;

namespace DepartmentsWebApp.Models.DepartmentModel
{
    public class DepartmentViewModel : IViewModel
    {
        public Department CurrentDepartment { get; set; }
        public string Name { get => CurrentDepartment.Name; }

        public List<DepartmentViewModel>? ChildrenDepartments { get; set; }
        public List<EmployeeViewModel>? Employees { get; set; }

        public DepartmentViewModel(Department department, List<DepartmentViewModel>? childrenDepartments, List<EmployeeViewModel>? employees)
        {
            CurrentDepartment = department;
            ChildrenDepartments = childrenDepartments;
            Employees = employees;
        }
    }
}
