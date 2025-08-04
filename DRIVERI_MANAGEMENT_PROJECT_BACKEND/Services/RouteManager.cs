using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class RouteManager : IRouteService
    {

        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public RouteManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }
        public void SaveOrUpdateRoute(RouteDto routeDto)
        {
            int id = routeDto.Id;
            if(routeDto is null) throw new RouteNotFoundException(id);

            if(id <= 0)
            {
                var mappedRoute = _mapper.Map<Route>(routeDto);
                _manager.Route.SaveOrUpdateRoute(mappedRoute);
                _manager.Save();
            }
            else if (id > 0)
            {
                var existingRoute = _manager.Route.GetRouteById(id, false);
                if (existingRoute is null) throw new RouteNotFoundException(id);
                existingRoute = _mapper.Map<Route>(routeDto);
                _manager.Save();
            }
           
        }

        public void DeleteOneRoute(int id, bool trackChanges)
        {
            if(id <= 0) throw new RouteNotFoundException(id);
            var entity = _manager.Route.GetRouteById(id, trackChanges);
            if (entity is null)
            {
                string message = $"Route with id {id} not found.";
                _logger.LogInfo(message);
                throw new RouteNotFoundException(id);
            }
            _manager.Route.DeleteRoute(entity);
            _manager.Save();
        }

        public IEnumerable<RouteDto> GetAllRoutes(bool trackChanges)
        {
            var routes = _manager.Route.GetAllRoutes(trackChanges);
            if(routes is null || !routes.Any())
            {
                _logger.LogInfo("No routes found.");
                return Enumerable.Empty<RouteDto>();
            }
            return routes.Select(route => _mapper.Map<RouteDto>(route)).ToList();

        }

        public RouteDto GetRouteByRouteName(string routeName, bool trackChanges)
        {
            if(string.IsNullOrWhiteSpace(routeName)) throw new ArgumentException("Route name cannot be null or empty.", nameof(routeName));
            
            var route = _manager.Route.GetRouteByRouteName(routeName, trackChanges);
            if (route is null)
            {
                string message = $"Route with name {routeName} not found.";
                _logger.LogInfo(message);
                throw new ArgumentException(message);
            }
            return _mapper.Map<RouteDto>(route);
        }

    }
}
