using NetTopologySuite.Mathematics;
using System.ComponentModel.DataAnnotations;
using TestDBLib.Entities;

namespace DepartmentsWebApp.Models.EmployeeModel
{
    public class EmployeeEditModel : IEditModel
    {
        public int ID { get; set; }

        [Required, RegularExpression(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$", 
            ErrorMessage = "Expected GUID")]
        public Guid DepartmentID { get; set; }

        [Required, RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?)", ErrorMessage = "Expected Russian word starting with upper letter")]
        [MaxLength(50)]
        public string SurName { get; set; }

        [Required, RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?)", ErrorMessage = "Expected Russian word starting with upper letter")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [RegularExpression(@"([А-ЯЁ][а-яё]+[\-\s]?)", ErrorMessage = "Expected Russian word starting with upper letter")]
        [MaxLength(50)]
        public string? Patronymic { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [MinLength(4), MaxLength(4)]
        public string? DocSeries { get; set; }

        [MinLength(6), MaxLength(6)]
        public string? DocNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

        public List<Guid>? DepartmentIDs { get; set; }

        public EmployeeEditModel(Employee employee, List<Guid>? departmntsIDs = null)
        {
            ID = employee.ID;
            DepartmentID = employee.DepartmentID;
            SurName = employee.SurName;
            FirstName = employee.FirstName;
            Patronymic = employee.Patronymic;
            DateOfBirth = employee.DateOfBirth;
            DocSeries = employee.DocSeries;
            DocNumber = employee.DocNumber;
            Position = employee.Position;
            DepartmentIDs = departmntsIDs;
        }
        public EmployeeEditModel(int id, Guid departmentID, string surname, string firstname, string? patronymic,
            DateTime dateOfBirth, string? docSeries, string? docNumber, string position, List<Guid>? departmntsIDs = null)
        {
            ID = id;
            DepartmentID = departmentID;
            SurName = surname;
            FirstName = firstname;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            DocSeries = docSeries;
            DocNumber = docNumber;
            Position = position;
            DepartmentIDs = departmntsIDs;
        }

        public static bool operator == (EmployeeEditModel left, EmployeeEditModel right) => left.ID == right.ID;
        public static bool operator !=(EmployeeEditModel left, EmployeeEditModel right) => left.ID != right.ID;

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            EmployeeEditModel employeeEditModel = (EmployeeEditModel)obj;

            return ID == employeeEditModel.ID && DepartmentID == employeeEditModel.DepartmentID && SurName == employeeEditModel.SurName &&
                   FirstName == employeeEditModel.FirstName && Patronymic == employeeEditModel.Patronymic &&
                   DocSeries == employeeEditModel.DocSeries && DocNumber == employeeEditModel.DocNumber &&
                   DateOfBirth == employeeEditModel.DateOfBirth && Position == employeeEditModel.Position;
        }

        public override int GetHashCode() => base.GetHashCode();

        public Entity ToEntity()
        {
            Employee employee = new Employee()
            {
                DepartmentID = this.DepartmentID,
                Position = this.Position,
                SurName = this.SurName,
                FirstName = this.FirstName,
                Patronymic = this.Patronymic,
                DateOfBirth = this.DateOfBirth,
                DocSeries = this.DocSeries,
                DocNumber = this.DocNumber
            };
            return employee;
        }

        public void CopyPropertiesTo(Entity entity)
        {
            Employee employee = (Employee)entity;
            employee.DepartmentID = DepartmentID;
            employee.Position = Position;
            employee.SurName = SurName;
            employee.FirstName = FirstName;
            employee.Patronymic = Patronymic;
            employee.DocSeries = DocSeries;
            employee.DocNumber = DocNumber;
            employee.DateOfBirth = DateOfBirth;
        }

        public static IEditModel FromEntity(Entity entity)
        {
            Employee employee = (Employee)entity;
            return new EmployeeEditModel(employee);
        }
    }
}
