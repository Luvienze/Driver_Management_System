using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IPersonService PersonService { get; }
        IRoleService RoleService { get; }
        ILineService LineService { get; }
        IRouteService RouteService { get; }
        IGarageService GarageService { get; }
        IChiefService ChiefService { get; }
        ITaskService TaskService { get; }
        IDriverService DriverService { get; }
        IVehicleService VehicleService { get; }
    }
}
