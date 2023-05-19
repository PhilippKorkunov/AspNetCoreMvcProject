using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using TestDBLib.Entities;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DepartmentsWebApp.Models.DepartmentModel
{
    public class DepartmentEditModel
    {
        [Required]
        public Guid ID { get; set; }

        public Guid? ParentDepartmentID { get; set; }
        public string? Code { get; set; }

        [Required]
        public string Name { get; set; }

        public DepartmentEditModel(Department department) 
        { 
            ID = department.ID;
            ParentDepartmentID = department.ParentDepartmentID;
            Code = department.Code;
            Name = department.Name;
        }

    }
}
