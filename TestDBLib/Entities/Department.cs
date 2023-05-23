using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDBLib.Entities
{
    public class Department : Entity
    {
        public Guid ID { get; set; }
        public Guid? ParentDepartmentID { get; set; }

        [MaxLength(10)]
        public string? Code { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [NotMapped]
        public override string VirtualId { get => ID.ToString(); }


        public override bool Equals(object? obj)
        {

            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Department Department = (Department)obj;

            return ParentDepartmentID == Department.ParentDepartmentID &&
                   Code == Department.Code && Name == Department.Name; // сравнивание всех данных, кроме ID, поскольку Id всегда уникален
        }
    }
}
