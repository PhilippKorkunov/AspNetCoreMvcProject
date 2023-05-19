using BusinessLayerLib.Implementations;
using TestDBLib.Entities;

namespace BusinessLayerLib
{
    public class DataManager
    {
        public EFRepository<Department> DepartmentRepository { get; private set; }
        public EFRepository<Employee> EmployeeRepository { get; private set; }
        public DataManager(EFRepository<Department> departmentRepository, EFRepository<Employee> employeeRepository)
        {
            EmployeeRepository = employeeRepository;
            DepartmentRepository = departmentRepository;
        }
    }
}
