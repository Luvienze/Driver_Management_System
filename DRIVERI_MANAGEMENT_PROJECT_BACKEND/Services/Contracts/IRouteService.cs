using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRouteService
    {
        IEnumerable<RouteDto> GetAllRoutes(bool trackChanges);
        RouteDto GetRouteByRouteName(string routeName, bool trackChanges);
        void SaveOrUpdateRoute(RouteDto routeDto);
        void DeleteOneRoute(int id, bool trackChanges);
    }
}
