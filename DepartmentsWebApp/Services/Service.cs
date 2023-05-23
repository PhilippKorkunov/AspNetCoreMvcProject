using BusinessLayerLib;
using EFCore.BulkExtensions;
using TestDBLib.Entities;

namespace DepartmentsWebApp.Services
{
    public class Service
    {
        public readonly DataManager dataManager;

        public Service(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
    }
}
