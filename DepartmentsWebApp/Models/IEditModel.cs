using TestDBLib.Entities;

namespace DepartmentsWebApp.Models
{
    public interface IEditModel //Edit-модель
    {
        public Entity ToEntity();

        public static IEditModel FromEntity(Entity entity) { throw new NotImplementedException(); }

        public void CopyPropertiesTo(Entity entity);
    }
}
