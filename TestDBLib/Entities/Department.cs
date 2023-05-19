namespace TestDBLib.Entities
{
    public class Department : Entity
    {
        public Guid ID { get; set; }
        public Guid? ParentDepartmentID { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
    }
}
