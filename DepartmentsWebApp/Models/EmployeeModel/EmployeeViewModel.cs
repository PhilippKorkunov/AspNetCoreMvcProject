using TestDBLib.Entities;

namespace DepartmentsWebApp.Models.EmployeeModel
{
    public class EmployeeViewModel : IViewModel
    {
        public decimal ID { get; set; }
        public Department Department { get; set; }
        public Guid DepartmentID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int FullAge
        {
            get
            {
                DateTime today = DateTime.Today;

                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.AddYears(age) > today)
                {
                    age--;
                }
                return age;
            }
            private set { }
        }
        public string? DocSeries { get; set; }
        public string? DocNumber { get; set; }
        public string Position { get; set; }


        public EmployeeViewModel(Employee employee) : base()
        {
            ID = employee.ID;
            Department = employee.Department;
            DepartmentID = employee.DepartmentID;
            SurName = employee.SurName;
            FirstName = employee.FirstName;
            Patronymic = employee.Patronymic;
            DateOfBirth = employee.DateOfBirth;
            DocSeries = employee.DocSeries;
            DocNumber = employee.DocNumber;
            Position = employee.Position;
        }

        public string Name { get => $"{SurName} {FirstName} {Patronymic}"; }
    }
}
