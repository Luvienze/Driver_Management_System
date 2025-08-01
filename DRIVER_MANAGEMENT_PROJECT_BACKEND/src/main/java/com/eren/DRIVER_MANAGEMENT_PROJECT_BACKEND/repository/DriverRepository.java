package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chief;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Driver;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.CadreTypes;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Days;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Genders;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;
import java.util.List;

/**
 * Repository interface for Driver entity.
 * </p>
 * Provides methods for performing database operations related to drivers.
 */
@Repository
public interface DriverRepository extends JpaRepository<Driver, Integer> {

    /**
     * Finds driver by their id.
     *
     * @param id ID of the driver.
     * @return The matching Driver entity, or null if not found.
     */
    @Query("""
        SELECT d FROM Driver d
        WHERE d.id = :id
    """
    )
    Driver findDriverById(Integer id);

    /**
     * Finds driver by their registration number.
     *
     * @param registrationNumber Registration number of the driver's associated person.
     * @return The matching Driver entity, or null if not found.
     */
    @Query("""
        SELECT d FROM Driver d
        LEFT JOIN FETCH d.personId p
        LEFT JOIN FETCH d.chiefId c
        WHERE p.registrationNumber = :registrationNumber
    """)
    Driver findDriverByRegistrationNumber(String registrationNumber);

    /**
     * Retrieves a list of not deleted drivers.
     *
     * @return List of not deleted drivers, or null if not found.
     */
    @Query("""
        SELECT d FROM Driver d
        JOIN FETCH d.personId p
        JOIN FETCH d.chiefId c
        JOIN FETCH d.garageId g
        WHERE p.isDeleted is false
    """)
    List<Driver> getAllDrivers();

    /**
     * Retrieves a list of active or inactive drivers assigned to a specific chief.
     *
     * @param chief The Chief entity to which the drivers are assigned.
     * @param isActive The status indicating whether to fetch active (true) or inactive (false) drivers.
     * @return A list of drivers filtered by chief and activity status.
     */
    @Query("""
        SELECT d FROM Driver d
        WHERE d.chiefId = :chief AND d.isActive = :isActive
    """)
    List<Driver> findActiveDriversByChief(@Param("chief") Chief chief,
                                          @Param("isActive") Boolean isActive);

    /**
     * Updates the active status of a driver based on their unique identifier.
     *
     * @param isActive New active status to set (true for active, false for inactive).
     * @param id Unique identifier of the driver whose status will be updated.
     */
    @Modifying
    @Transactional
    @Query("""
        UPDATE Driver d
        SET d.isActive = :isActive
        WHERE d.id = :id
""")
    void updateIsActive(@Param("isActive") Boolean isActive, @Param("id") Integer id);

    /**
     * Updates the specified fields of a driver using their registration number as the identifier.
     *
     * <p>This method updates the garage, chief, cadre type, day off, and active status
     * of the driver associated with the given registration number.
     *
     * @param registrationNumber Unique registration number of the driver.
     * @param garageId The new garage to assign to the driver.
     * @param chiefId The new chief to assign to the driver.
     * @param cadre The updated cadre type for the driver.
     * @param dayOff The updated day off value for the driver.
     * @param isActive The new active status of the driver.
     */
    @Modifying
    @Transactional
    @Query("""
        UPDATE Driver d
        SET d.garageId = :garageId,
        d.chiefId = :chiefId,
        d.cadre = :cadre,
        d.dayOff = :dayOff,
        d.isActive = :isActive
        WHERE d.personId.registrationNumber = :registrationNumber
    """)
    void updateDriverByRegistrationNumber(@Param("registrationNumber") String registrationNumber,
                                            @Param("garageId") Garage garageId,
                                            @Param("chiefId") Chief chiefId,
                                            @Param("cadre") CadreTypes cadre,
                                            @Param("dayOff") Days dayOff,
                                            @Param("isActive") Boolean isActive);

    /**
     * Retrieves the number of distinct drivers who have a day off on the specified day
     * and are not marked as deleted.
     *
     * <p>
     * The method joins the {@code Driver} entity with its related {@code Person}
     * and filters out any person marked as deleted.
     * </p>
     *
     * @param day The day to filter drivers by their day off.
     * @return The number of distinct active (non-deleted) drivers on that day off.
     */
    @Query("""
        SELECT count(distinct d) FROM Driver d
        JOIN d.personId p
        WHERE d.dayOff = :day AND p.isDeleted = false
    """)
    int getDriverOnDayOff (@Param("day") Days day);

    /**
     * Retrieves the number of distinct drivers based on gender who are not marked as deleted.
     *
     * <p>
     * This method joins the {@code Driver} entity with its associated {@code Person} entity,
     * filters by the given gender, and excludes any person marked as deleted.
     * </p>
     *
     * @param gender The gender to filter drivers by (e.g., {@code MALE}, {@code FEMALE}).
     * @return The number of distinct non-deleted drivers with the specified gender.
     */
    @Query("""
        SELECT count (distinct d) FROM Driver d
        JOIN d.personId p
        WHERE p.gender = :gender AND p.isDeleted = false
    """)
    int getDriverGenderCount(@Param("gender")Genders gender);
}