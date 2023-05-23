using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBLib.Entities
{
    [Table("Empoyee")]
    public class Employee : Entity
    {
        [Key]
        [Column(TypeName = "numeric(5, 0)")]
        public int ID { get; set; }
        public Department Department { get; set; }
        public Guid DepartmentID { get; set; }

        [MaxLength(50)]
        public string SurName { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set;}

        [MaxLength(50)]
        public string? Patronymic { get; set;}

        [Column(TypeName = "datetime")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(4)]
        public string? DocSeries { get; set; }

        [MaxLength(6)]
        public string? DocNumber{ get; set; }

        [MaxLength(50)]
        public string Position { get; set;}


        [NotMapped]
        public override string VirtualId { get => ID.ToString(); }


        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Employee employee = (Employee)obj;

            return DepartmentID == employee.DepartmentID && SurName == employee.SurName && FirstName == employee.FirstName && 
                   Patronymic == employee.Patronymic && DocSeries == employee.DocSeries && DocNumber == employee.DocNumber &&
                   DateOfBirth == employee.DateOfBirth && Position == employee.Position; // сравнивание всех данных, кроме ID, поскольку Id всегда уникален
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
