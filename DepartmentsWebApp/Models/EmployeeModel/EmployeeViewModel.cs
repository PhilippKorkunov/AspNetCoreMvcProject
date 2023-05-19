using TestDBLib.Entities;

namespace DepartmentsWebApp.Models.EmployeeModel
{
    public class EmployeeViewModel : IModel
    {
        public decimal ID { get; set; }
        public Department Department { get; set; }
        public Guid DepartmentID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string? Patronymic { get; set; }
        public string DateOfBirth { get; set; }
        public int FullAge
        {
            get
            {
                DateTime today = DateTime.Today;
                DateTime dateOfBirth = DateTime.Parse(DateOfBirth);

                int age = today.Year - dateOfBirth.Year;
                if (dateOfBirth.AddYears(age) > today)
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
            DateOfBirth = employee.DateOfBirth.ToString("dd.MM.yyyy");
            DocSeries = employee.DocSeries;
            DocNumber = employee.DocNumber;
            Position = employee.Position;
        }

        public string Name { get => $"{SurName} {FirstName} {Patronymic}"; }
    }
}
