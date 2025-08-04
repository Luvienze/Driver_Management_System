using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        public RouteRepository(RepositoryContext context) : base(context)
        {
        }

        public void SaveOrUpdateRoute(Route route) => Create(route);

        public void DeleteRoute(Route route) => Delete(route);

        public IQueryable<Route> GetAllRoutes(bool trackChanges) =>
            FindAll(trackChanges);
        public Route GetRouteById(int routeId, bool trackChanges) =>
            FindByCondition(r => r.Id.Equals(routeId), trackChanges)
            .SingleOrDefault();

        public Route GetRouteByRouteName(string routeName, bool trackChanges) =>
     FindByCondition(r => r.RouteName.Trim().ToLower() == routeName.Trim().ToLower(), trackChanges)
     .SingleOrDefault();

    }
}
