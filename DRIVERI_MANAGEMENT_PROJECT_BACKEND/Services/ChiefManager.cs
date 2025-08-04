using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ChiefManager : IChiefService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public ChiefManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }
        public void DeleteOneChief(int id, bool trackChanges)
        {
            if(id < 0) throw new ArgumentException("Chief ID must be greater than or equal to zero.", nameof(id));
            var chief = _manager.Chief.GetChiefById(id);
            if (chief == null)
            {
                _logger.LogInfo($"Chief with id {id} not found.");
                throw new ChiefNotFoundException(id);
            }
            _manager.Chief.Delete(chief);
            _manager.Save();
        }

        public IEnumerable<ChiefDto> GetActiveChiefs()
        {
            var chiefs = _manager.Chief.GetActiveChiefs();
            if (!chiefs.Any()) 
            { 
                _logger.LogInfo("No active chiefs found.");
                return Enumerable.Empty<ChiefDto>();
            }
            return chiefs.Select(chief => _mapper.Map<ChiefDto>(chief)).ToList();
        }

        public IEnumerable<ChiefDto> GetAllChiefs(bool trackChanges)
        {
            var chiefs = _manager.Chief.GetAllChief(trackChanges);
            if(!chiefs.Any())
            {
                _logger.LogInfo("No chiefs found.");
                return Enumerable.Empty<ChiefDto>();
            }
            return chiefs.Select(chief => _mapper.Map<ChiefDto>(chief)).ToList();
        }

        public ChiefDto GetChiefById(int chiefId)
        {
            if(chiefId < 0) throw new ArgumentException("Chief ID must be greater than or equal to zero.", nameof(chiefId));
            var chief = _manager.Chief.GetChiefById(chiefId);
            if(chief == null)
            {
                _logger.LogInfo($"Chief with id {chiefId} not found.");
                throw new ChiefNotFoundException(chiefId);
            }
            return _mapper.Map<ChiefDto>(chief);
        }

        public ChiefDto GetChiefByName(string fullname)
        {
            if(fullname == null) throw new ArgumentNullException(nameof(fullname), "Full name cannot be null.");
            var chief = _manager.Chief.GetChiefByName(fullname);
            if(chief == null)
            {
                _logger.LogInfo($"Chief with name {fullname} not found.");
                throw new ChiefNotFoundException(chief.Id);
            }
            return _mapper.Map<ChiefDto>(chief);
        }

        public ChiefDto GetChiefByRegistrationNumber(string registrationNumber)
        {
           if(registrationNumber == null) throw new ArgumentNullException(nameof(registrationNumber), "Registration number cannot be null.");
            var chief = _manager.Chief.GetChiefByRegistrationNumber(registrationNumber);

            if(chief == null)
            {
                _logger.LogInfo($"Chief with registration number {registrationNumber} not found.");
                throw new ChiefNotFoundException(chief.Id);
            }
            return _mapper.Map<ChiefDto>(chief);
        }

        public ChiefDto GetPersonChiefByRegistrationNumber(string registrationNumber)
        {
            if (registrationNumber == null) throw new ArgumentNullException(nameof(registrationNumber), "Registration number cannot be null.");
            var chief = _manager.Chief.GetPersonChiefByRegistrationNumber(registrationNumber);
            if (chief == null)
            {
                _logger.LogInfo($"Chief with registration number {registrationNumber} not found.");
                throw new ChiefNotFoundException(chief.Id);
            }
            return _mapper.Map<ChiefDto>(chief);
        }

        public void UpdateOneChief(int id, ChiefDto chiefDto, bool trackChanges)
        {
            if(id < 0) throw new ArgumentException("Chief ID must be greater than or equal to zero.", nameof(id));
            if(chiefDto == null) throw new ArgumentNullException(nameof(chiefDto), "Chief DTO cannot be null.");
            var chief = _manager.Chief.GetChiefById(id);
            if (chief == null)
            {
                _logger.LogInfo($"Chief with id {id} not found.");
                throw new ChiefNotFoundException(id);
            }
            _mapper.Map(chiefDto, chief);
            _manager.Chief.Update(chief);
            _manager.Save();
        }
    }
}
