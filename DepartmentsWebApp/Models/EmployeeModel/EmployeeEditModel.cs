using System.ComponentModel.DataAnnotations;
using TestDBLib.Entities;

namespace DepartmentsWebApp.Models.EmployeeModel
{
    public class EmployeeEditModel 
    {
        public decimal ID { get; set; }

        [Required]
        public Guid DepartmentID { get; set; }

        [Required]
        public string SurName { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string? Patronymic { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? DocSeries { get; set; }
        public string? DocNumber { get; set; }

        [Required]
        public string Position { get; set; }


        public EmployeeEditModel(Employee employee) 
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
        }
    }
}
