package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.PersonDTO;
import java.util.List;

/**
 * Service interface for managing Person-related operations.
 */
public interface PersonService {

    /**
     * Retrieves a person by their ID.
     *
     * @param id the ID of the person to retrieve.
     * @return the corresponding {@link PersonDTO}, or null if not found.
     */
    PersonDTO findPersonById(int id);

    /**
     * Retrieves a person by the ID of the associated driver.
     *
     * @param id the driver ID.
     * @return the corresponding {@link PersonDTO}, or null if not found.
     */
    PersonDTO findPersonByDriverId(int id);

    /**
     * Retrieves a person by their registration number.
     *
     * @param registrationNumber the unique registration number of the person.
     * @return the corresponding {@link PersonDTO}, or null if not found.
     */
    PersonDTO findPersonByRegistrationNumber(String registrationNumber);

    /**
     * Retrieves all persons in the system as DTOs.
     *
     * @return a list of {@link PersonDTO} representing all persons,
     *         or an empty list if none exist.
     */
    List<PersonDTO> getAllPersonsDTO();

    /**
     * Saves a new person or updates an existing one based on the provided DTO.
     *
     * @param personDTO the person data to save or update.
     * @return the saved or updated {@link PersonDTO}.
     */
    PersonDTO saveOrUpdatePerson(PersonDTO personDTO);

    /**
     * Deletes a person using their registration number.
     *
     * @param registrationNumber the registration number of the person to delete.
     */
    void deletePersonByRegistrationNumber(String registrationNumber);
}
