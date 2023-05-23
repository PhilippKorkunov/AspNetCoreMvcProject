using DepartmentsWebApp.Models.EmployeeModel;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using TestDBLib.Entities;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DepartmentsWebApp.Models.DepartmentModel
{
    public class DepartmentEditModel : IEditModel
    {
        [Required]
        public Guid ID { get; set; }

        public Guid? ParentDepartmentID { get; set; }

        [MaxLength(10)]
        public string? Code { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public List<Guid>? DepartmentsIDs { get; set; }

        public DepartmentEditModel(Department department, List<Guid>? departmentsIds = null) 
        { 
            ID = department.ID;
            ParentDepartmentID = department.ParentDepartmentID;
            Code = department.Code;
            Name = department.Name;
            DepartmentsIDs = departmentsIds;
        }

        public DepartmentEditModel(Guid id, Guid? parentDepartmentId, string? code, string name,  List<Guid>? departmentsIds = null) 
        { 
            ID = id;
            ParentDepartmentID = parentDepartmentId;
            Code = code;
            Name = name;
            DepartmentsIDs = departmentsIds;
        }

        public override bool Equals(object? obj)
        {

            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            DepartmentEditModel departmentEditModel = (DepartmentEditModel)obj;

            return ID == departmentEditModel.ID && ParentDepartmentID == departmentEditModel.ParentDepartmentID &&
                   Code == departmentEditModel.Code && Name == departmentEditModel.Name;
        }

        public static bool operator ==(DepartmentEditModel left, DepartmentEditModel rigth) => left.ID == rigth.ID;
        public static bool operator !=(DepartmentEditModel left, DepartmentEditModel rigth) => left.ID != rigth.ID;

        public Entity ToEntity()
        {
            Department department = new Department()
            {
                ParentDepartmentID = this.ParentDepartmentID,
                Code = this.Code,
                Name = this.Name
            };
            return department;
        }

        public void CopyPropertiesTo(Entity entity)
        {
            Department department = (Department)entity;
            department.ParentDepartmentID = ParentDepartmentID;
            department.Name = Name;
            department.Code = Code;
        }

        public static IEditModel FromEntity(Entity entity)
        {
            Department department = (Department)entity;
            return new DepartmentEditModel(department);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
