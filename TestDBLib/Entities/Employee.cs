using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBLib.Entities
{
    [Table("Empoyee")]
    public class Employee : Entity
    {
        public decimal ID { get; set; }
        public Department Department { get; set; }
        public Guid DepartmentID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set;}
        public string? Patronymic { get; set;}
        public DateTime DateOfBirth { get; set; }
        public string? DocSeries { get; set; }
        public string? DocNumber{ get; set; }
        public string Position { get; set;}
    }
}
