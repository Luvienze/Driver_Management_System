using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPersonService> _personService;
        private readonly Lazy<IRoleService> _roleService;
        private readonly Lazy<ILineService> _lineService;
        private readonly Lazy<IRouteService> _routeService;
        private readonly Lazy<IGarageService> _garageService;
        private readonly Lazy<IChiefService> _chiefService;
        private readonly Lazy<IVehicleService> _vehicleService;
        private readonly Lazy<ITaskService> _taskService;
        private readonly Lazy<IDriverService> _driverService;
        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerService logger,
            IMapper mapper)
        {
            _personService = new Lazy<IPersonService>(() => new PersonManager(repositoryManager, logger, mapper));
            _roleService = new Lazy<IRoleService>(() => new RoleManager(repositoryManager, logger, mapper));
            _lineService = new Lazy<ILineService>(() => new LineManager(repositoryManager, logger, mapper));
            _routeService = new Lazy<IRouteService>(() => new RouteManager(repositoryManager, logger, mapper));
            _garageService = new Lazy<IGarageService>(() => new GarageManager(repositoryManager, logger, mapper));
            _chiefService = new Lazy<IChiefService>(() => new ChiefManager(repositoryManager, logger, mapper));
            _vehicleService = new Lazy<IVehicleService>(() => new VehicleManager(repositoryManager, logger, mapper));
            _taskService = new Lazy<ITaskService>(() => new TaskManager(repositoryManager, logger, mapper));
            _driverService = new Lazy<IDriverService>(() => new DriverManager(repositoryManager, logger, mapper));
        }
        public IPersonService PersonService => _personService.Value;
        public IRoleService RoleService => _roleService.Value;
        public ILineService LineService => _lineService.Value;
        public IRouteService RouteService => _routeService.Value;
        public IGarageService GarageService => _garageService.Value;
        public IChiefService ChiefService => _chiefService.Value;
        public IVehicleService VehicleService => _vehicleService.Value;
        public ITaskService TaskService => _taskService.Value;
        public IDriverService DriverService => _driverService.Value;

    }
}
