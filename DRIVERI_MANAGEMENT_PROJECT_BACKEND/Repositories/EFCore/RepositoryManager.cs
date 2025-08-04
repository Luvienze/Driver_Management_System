using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IPersonRepository> _personRepository;
        private readonly Lazy<IRoleRepository> _roleRepository;
        private readonly Lazy<IGarageRepository> _garageRepository;
        private readonly Lazy<ILineRepository> _lineRepository;
        private readonly Lazy<IRouteRepository> _routeRepository;
        private readonly Lazy<IVehicleRepository> _vehicleRepository;
        private readonly Lazy<IChiefRepository> _chiefRepository;
        private readonly Lazy<ITaskRepository> _taskRepository;
        private readonly Lazy<IDriverRepository> _driverRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _personRepository = new Lazy<IPersonRepository>(() => new PersonRepository(_context));
            _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(_context));
            _garageRepository = new Lazy<IGarageRepository>(() => new GarageRepository(_context));
            _lineRepository = new Lazy<ILineRepository>(() => new LineRepository(_context));
            _routeRepository = new Lazy<IRouteRepository>(() => new RouteRepository(_context));
            _vehicleRepository = new Lazy<IVehicleRepository>(() => new VehicleRepository(_context));
            _chiefRepository = new Lazy<IChiefRepository>(() => new ChiefRepository(_context));
            _taskRepository = new Lazy<ITaskRepository>(() => new TaskRepository(_context));
            _driverRepository = new Lazy<IDriverRepository>(() => new DriverRepository(_context));
        }
        public IPersonRepository Person => _personRepository.Value;
        public IRoleRepository Role => _roleRepository.Value;
        public IGarageRepository Garage => _garageRepository.Value;
        public ILineRepository Line => _lineRepository.Value;
        public IRouteRepository Route => _routeRepository.Value;
        public IVehicleRepository Vehicle => _vehicleRepository.Value;
        public IChiefRepository Chief => _chiefRepository.Value;
        public ITaskRepository Task => _taskRepository.Value;
        public IDriverRepository Driver => _driverRepository.Value;
        IPersonRepository IRepositoryManager.Person { get => Person; set => throw new NotImplementedException(); }
        IRoleRepository IRepositoryManager.Role { get => Role; set => throw new NotImplementedException(); }
        IGarageRepository IRepositoryManager.Garage { get => Garage; set => throw new NotImplementedException(); }
        ILineRepository IRepositoryManager.Line { get => Line; set => throw new NotImplementedException(); }
        IRouteRepository IRepositoryManager.Route { get => Route; set => throw new NotImplementedException(); }
        IVehicleRepository IRepositoryManager.Vehicle { get => Vehicle; set => throw new NotImplementedException(); }
        IChiefRepository IRepositoryManager.Chief { get => Chief; set => throw new NotImplementedException(); }
        ITaskRepository IRepositoryManager.Task { get => Task; set => throw new NotImplementedException(); }
        IDriverRepository IRepositoryManager.Driver { get => Driver; set => throw new NotImplementedException(); }
        void IRepositoryManager.Save()
        {
            _context.SaveChanges();
        }
    }
}
