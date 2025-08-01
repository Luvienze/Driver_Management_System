package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.GarageDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import java.util.List;

/**
 * Service interface for managing Garage entities.
 */
public interface GarageService {

    /**
     * Retrieves a list of all garages.
     *
     * @return List of all Garage entities.
     */
    List<Garage> getAllGarages();

    /**
     * Retrieves a list of all garages with only basic name/id information.
     * @return List of GarageDTOs containing name and/or minimal info.
     */
    List<GarageDTO> getGarageNameList();

    /**
     * Retrieves the garage associated with a given driver or chief's registration number.
     *
     * @param registrationNumber The registration number of the driver or chief.
     * @return GarageDTO representing the associated garage, or null if not found.
     */
    GarageDTO getGarageByRegistrationNumber(String registrationNumber);
}
