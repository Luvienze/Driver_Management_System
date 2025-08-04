using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IPersonRepository Person { get; set; }
        IRoleRepository Role { get; set; }
        ILineRepository Line { get; set; }
        IRouteRepository Route { get; set; }
        IGarageRepository Garage { get; set; }
        IVehicleRepository Vehicle { get; set; }
        IChiefRepository Chief { get; set; }
        ITaskRepository Task { get; set; }
        IDriverRepository Driver { get; set; }
        void Save();
    }
}
