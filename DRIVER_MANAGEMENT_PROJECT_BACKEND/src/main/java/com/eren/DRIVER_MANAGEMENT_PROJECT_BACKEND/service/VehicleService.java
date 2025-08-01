package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.VehicleDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Task;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Vehicle;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.VehicleStatuses;
import org.springframework.data.repository.query.Param;

import java.util.List;

/**
 * Service interface for managing vehicle entities.
 */
public interface VehicleService {

    /**
     * Retrieves a vehicle by its door number.
     *
     * @param doorNo the unique door number of the vehicle.
     * @return the matching {@link VehicleDTO}, or null if not found.
     */
    VehicleDTO getVehicleByDoorNo(String doorNo);

    /**
     * Retrieves all vehicles in the system.
     *
     * @return a list of all {@link VehicleDTO} objects.
     */
    List<VehicleDTO> getAllVehicles();


    /**
     * Retrieves vehicles that belong to a specific garage and have a specific status.
     *
     * @param garageId the ID of the garage.
     * @param status the status of the vehicle (e.g., ACTIVE, INACTIVE).
     * @return a list of {@link VehicleDTO} objects matching the garage and status.
     */
    List<VehicleDTO> findByGarageId(int garageId, int status);

    /**
     * Counts the number of vehicle with a specific status.
     * @param status Status of the task.
     * @return the count of planned or filtered vehicles.
     */
    int getVehiclesCountByStatus(int status);
}
