package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.DriverDTO;
import java.time.LocalDate;
import java.util.List;

/**
 * Service interface for managing Driver entities.
 */
public interface DriverService {

    /**
     * Finds a driver by their ID.
     *
     * @param id Identifier of the driver.
     * @return DriverDTO representing the driver with the specified ID, or null if not found.
     */
    DriverDTO findDriverById(Integer id);

    /**
     * Finds a driver by their registration number.
     *
     * @param registrationNumber Registration number of the driver.
     * @return DriverDTO representing the driver, or null if not found.
     */
    DriverDTO findDriverByRegistrationNumber(String registrationNumber);


    /**
     * Retrieves a list of all drivers as DTOs.
     *
     * May differ from getAllDrivers() if filtering or mapping is applied.
     *
     * @return List of DriverDTOs.
     */
    List<DriverDTO> getAllDriversDTO();

    /**
     * Retrieves a list of a chief's drivers based on activity status.
     *
     * @param chiefId ID of the chief.
     * @param isActive Activity status of the drivers (true for active).
     * @return List of DriverDTOs matching the chief and activity filter.
     */
    List<DriverDTO> findActiveDriversByChief(int chiefId,
                                          Boolean isActive);

    /**
     * Saves a new driver or updates an existing one.
     *
     * @param driverDTO Data Transfer Object representing the driver to be saved or updated.
     * @return The saved or updated DriverDTO.
     */
    DriverDTO saveOrUpdateDriver(DriverDTO driverDTO);

    /**
     * Updates the active status of a driver.
     *
     * @param driverId ID of the driver to update.
     * @param isActive New activity status.
     * @return Updated DriverDTO.
     */
    DriverDTO updateIsActive(Integer driverId, boolean isActive);

    /**
     * Updates a driver's details by their registration number.
     *
     * @param driverDTO DTO containing updated driver information.
     * @return Updated DriverDTO.
     */
    DriverDTO updateDriverByRegistrationNumber(DriverDTO driverDTO);

    /**
     * Returns the number of drivers who are on day off for a specific date.
     *
     * @param day The day to check.
     * @return Count of drivers on leave that day.
     */
    int getDriverOnDayOff (LocalDate day);

    /**
     * Returns the number of drivers by gender.
     *
     * @param gender Integer representing gender (enum recommended).
     * @return Count of drivers with the specified gender.
     */
    int getDriverGenderCount(int gender);

}
