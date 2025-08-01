package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.PersonDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.RoleDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Role;
import java.util.List;

/**
 * Service interface for managing Role-related operations.
 */
public interface RoleService {

    /**
     * Retrieves all roles in the system.
     *
     * @return a list of all {@link Role} entities.
     */
    List<Role> getAllRoles();

    /**
     * Retrieves a list of roles associated with a specific person.
     *
     * @param personDTO the person whose roles are to be retrieved.
     * @return a list of {@link RoleDTO} assigned to the given person.
     */
    List<RoleDTO> getRoleListByPersonId(PersonDTO personDTO);

    /**
     * Finds roles by a person's registration number and phone number.
     *
     * @param regNo the registration number of the person.
     * @param phone the phone number of the person.
     * @return a list of matching {@link RoleDTO}s, or an empty list if none match.
     */
    List<RoleDTO> findRolesByRegNoAndPhone(String regNo, String phone);

    /**
     * Adds a new person to the system with associated role(s).
     *
     * @param personDTO the {@link PersonDTO} object containing person and role information.
     */
    void addPerson(PersonDTO personDTO);
}
