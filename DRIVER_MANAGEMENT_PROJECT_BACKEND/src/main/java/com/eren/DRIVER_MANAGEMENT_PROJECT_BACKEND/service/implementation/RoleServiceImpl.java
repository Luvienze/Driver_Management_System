package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.PersonRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.RoleRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.PersonDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.RoleDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Role;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Roles;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.RoleService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class RoleServiceImpl implements RoleService {

    @Autowired
    private RoleRepository roleRepository;
    @Autowired
    private PersonRepository personRepository;

    /**
     * Retrieves all roles in the system.
     *
     * @return List of all {@link Role} entities.
     */
    @Override
    public List<Role> getAllRoles() {
        return roleRepository.findAll();
    }

    /**
     * Retrieves the list of roles associated with a given person.
     *
     * @param personDTO The {@link PersonDTO} representing the person.
     * @return List of {@link RoleDTO} for the given person, or {@code null} if the personDTO is {@code null}.
     */
    @Override
    public List<RoleDTO> getRoleListByPersonId(PersonDTO personDTO) {
        if (personDTO == null) {
            return null;
        }

        Person person = personRepository.findPersonById(personDTO.getId());

        return roleRepository.findRolesByPersonId(person)
                .stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    /**
     * Finds roles by registration number and phone number.
     *
     * @param regNo Registration number of the person.
     * @param phone Phone number of the person.
     * @return List of {@link RoleDTO} matching the given registration number and phone,
     *         or {@code null} if either parameter is invalid or no roles found.
     */
    @Override
    public List<RoleDTO> findRolesByRegNoAndPhone(String regNo, String phone) {
        if (regNo == null || regNo.isEmpty()) {
            return null;
        }

        if(phone.isEmpty()){
            return null;
        }

        List<Role> roles = roleRepository.findRolesByRegNoAndPhone(regNo, phone);
        if (roles.isEmpty()) {
            return null;
        }

        return roles.stream().map(this::convertToDTO).collect(Collectors.toList());

    }

    /**
     * Adds a new role for a given person.
     * Sets the role to the third enum value in {@link Roles}.
     *
     * @param personDTO The person for whom the role will be added.
     */
    @Override
    public void addPerson(PersonDTO personDTO) {
        Role role = new Role();
        Person person = personRepository.findPersonById(personDTO.getId());
        role.setRoleName(Roles.values()[2]);
        role.setPerson(person);
        roleRepository.save(role);
    }

    /**
     * Converts a {@link Role} entity to a {@link RoleDTO}.
     *
     * @param role the {@link Role} entity to convert.
     * @return the converted {@link RoleDTO}.
     */
    private RoleDTO convertToDTO(Role role) {
        RoleDTO roleDTO = new RoleDTO();
        roleDTO.setId(role.getId());
        roleDTO.setRoleName(role.getRoleName().ordinal());
        roleDTO.setPersonId(role.getPerson().getId());
        return roleDTO;
    }
}
