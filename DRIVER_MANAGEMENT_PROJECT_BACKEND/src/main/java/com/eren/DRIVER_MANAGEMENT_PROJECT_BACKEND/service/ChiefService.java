package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChiefDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chief;
import org.springframework.data.repository.query.Param;

import java.util.List;

/**
 * Service interface for managing Chief entities.
 */
public interface ChiefService {

    /**
     * Finds a chief by their registration number.
     *
     * @param registrationNumber Registration number of the chief.
     * @return ChiefDTO representing the chief with the specified registration number, or null if not found.
     */
    ChiefDTO findChiefByRegistrationNumber(String registrationNumber);

    /**
     * Finds the chief responsible for a person by the person's registration number.
     *
     * @param registrationNumber Registration number of the person.
     * @return ChiefDTO representing the chief of the specified person, or null if not found.
     */
    ChiefDTO findPersonChiefByRegistrationNumber(String registrationNumber);

    /**
     * Finds a chief by their unique ID.
     *
     * @param id The unique identifier of the chief.
     * @return ChiefDTO representing the chief with the specified ID, or null if not found.
     */
    ChiefDTO findChiefById(int id);


    /**
     * Retrieves a list of active chiefs.
     *
     * @return List of ChiefDTOs representing active chiefs.
     */
    List<ChiefDTO> findActiveChiefs();
}
