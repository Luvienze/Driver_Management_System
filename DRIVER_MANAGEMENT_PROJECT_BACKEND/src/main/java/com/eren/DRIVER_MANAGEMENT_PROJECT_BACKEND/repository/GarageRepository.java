package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import java.util.List;

/**
 * Repository interface for Garage entity.
 * </p>
 * Provides methods for performing database operations related to garages.
 */
@Repository
public interface GarageRepository extends JpaRepository<Garage, Integer> {

    /**
     * Finds garage by their id.
     *
     * @param id ID of the garage.
     * @return The matching Garage entity, or null if not found.
     */
    @Query("""
        SELECT g FROM Garage g
        WHERE g.id = :id
    """)
    Garage getGarageById(@Param("id") Integer id);

    /**
     * Finds garage by their name.
     *
     * @param garageName GarageName of the garage.
     * @return The matching Garage entity, or null if not found.
     */
    @Query("""
        SELECT g FROM Garage g
        WHERE g.garageName = :garageName
    """)
    Garage findGarageByGarageName(@Param("garageName") String garageName);

    /**
     * Retrieves a list of garages.
     * @return Retrieves a list of garages, or null if not found.
     */
    @Query("""
        SELECT g FROM Garage g
    """)
    List<Garage> getGarageNameList();

    /**
     * Finds garage by chief or driver registration number.
     * @param registrationNumber Registration number of chief or driver.
     * @return The matching garage entity, or null if not found.
     */
    @Query("""
        SELECT c.garageId FROM Driver d
        JOIN d.personId p
        JOIN d.chiefId c
        WHERE p.registrationNumber = :registrationNumber
    """)
    Garage getGarageByRegistrationNumber(@Param("registrationNumber") String registrationNumber);

}
