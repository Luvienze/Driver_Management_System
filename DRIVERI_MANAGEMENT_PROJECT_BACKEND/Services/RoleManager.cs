using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class RoleManager : IRoleService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public RoleManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public void SaveOrUpdateRole(RoleDto roleDto)
        {
            int id = roleDto.Id;
            if(roleDto is null) throw new RoleNotFoundException(id);
            if (id <= 0)
            {
                var mappedRole = _mapper.Map<Role>(roleDto);
                _manager.Role.SaveOrUpdateRole(mappedRole);
                _manager.Save();
            }
            else if (id > 0)
            {
                var existingRole = _manager.Role.GetRoleById(id, false);
                if(existingRole is null) throw new RoleNotFoundException(id);
                existingRole = _mapper.Map<Role>(roleDto);
                _manager.Role.SaveOrUpdateRole(existingRole);
                _manager.Save();
            }
        }

        public void DeleteOneRole(int id, bool trackChanges)
        {
            var entity = _manager.Role.GetRoleById(id, trackChanges);
            if (entity is null){
                string message = $"Role with id {id} not found.";
                _logger.LogInfo(message);
                throw new RoleNotFoundException(id);
            }

            _manager.Role.DeleteRole(entity);
            _manager.Save();
        }

        public IEnumerable<RoleDto> GetAllRoles(bool trackChanges)
        {
            return _manager.Role.GetAllRoles(trackChanges)
                .Select(role => _mapper.Map<RoleDto>(role))
                .ToList();
        }

        public IEnumerable<RoleDto> GetRoleByPersonId(int personId, bool trackChanges)
        {
            if(personId <= 0)
            {
                throw new ArgumentException("Person ID must be greater than zero.", nameof(personId));
            }
            var roles = _manager.Role.FindRoleByPersonId(personId, trackChanges);
            if (roles is null || !roles.Any())
            {
                string message = $"No roles found for person with ID {personId}.";
                _logger.LogInfo(message);
                throw new RoleNotFoundException(personId);
            }
            return roles
                .Select(role => _mapper.Map<RoleDto>(role))
                .ToList();
        }

        public IEnumerable<RoleDto> GetRolesByRegistrationNumberAndPhone(string registrationNumber, string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(registrationNumber) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Registration number and phone number cannot be null or empty.");
            }
            var roles = _manager.Role.FindRolesByRegistrationAndPhone(registrationNumber, phoneNumber);
            return roles
                .Select(role => _mapper.Map<RoleDto>(role))
                .ToList();
        }

    }
}
