using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRouteRepository : IRepositoryBase<Route>
    {
        IQueryable<Route> GetAllRoutes(bool trackChanges);
        Route GetRouteById(int routeId, bool trackChanges);
        Route GetRouteByRouteName(string routeName, bool trackChanges);
        void SaveOrUpdateRoute(Route route);
        void DeleteRoute(Route route);
    }
}
