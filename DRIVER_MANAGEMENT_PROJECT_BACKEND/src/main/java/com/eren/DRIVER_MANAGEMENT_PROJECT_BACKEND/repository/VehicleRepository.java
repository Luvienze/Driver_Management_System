package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Vehicle;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Tasks;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.VehicleStatuses;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.time.LocalDateTime;
import java.util.List;

/**
 * Repository interface for Vehicle entity.
 * </p>
 * Provides methods for performing database operations related to vehicles.
 */
@Repository
public interface VehicleRepository extends JpaRepository<Vehicle, Integer> {

    /**
     * Finds a Vehicle entity by its door number.
     *
     * @param doorNo The door number of the vehicle.
     * @return The Vehicle entity matching the specified door number, or null if none found.
     */
    @Query("""
        SELECT v FROM Vehicle v
        LEFT JOIN FETCH v.garage g
        WHERE v.doorNo = :doorNo
        
    """)
    Vehicle findVehicleByDoorNo (@Param("doorNo") String doorNo);

    /**
     * Retrieves a list of Vehicle entities that belong to a garage with the given name.
     *
     * @param garageName The name of the garage.
     * @return List of vehicles associated with the specified garage name.
     */
    @Query("""
        SELECT v FROM Vehicle v
        WHERE v.garage = :garage
    """)
    List<Vehicle> findByGarageName(@Param("garage") String garageName);

    /**
     * Retrieves a list of vehicles filtered by garage ID and vehicle status.
     *
     * @param garageId The ID of the garage.
     * @param status The status of the vehicle (e.g., READY_FOR_SERVICE, UNDER_MAINTENANCE).
     * @return List of vehicles in the specified garage with the given status.
     */
    @Query("""
        SELECT v FROM Vehicle v
        WHERE v.garage.id = :garageId and v.status = :status
    """)
    List<Vehicle> findByGarageId(@Param("garageId") int garageId,
                                 @Param("status") VehicleStatuses status);


    /**
     * Retrieves the count of distinct vehicle with a specified status.
     * @param status Status of the vehicle.
     * @return The number of distinct vehicles matching the criteria.
     */
    @Query("""
        SELECT COUNT(distinct v) FROM Vehicle v
        WHERE v.status = :status
    """)
    int getVehiclesCountByStatus(@Param("status") VehicleStatuses status);
}
