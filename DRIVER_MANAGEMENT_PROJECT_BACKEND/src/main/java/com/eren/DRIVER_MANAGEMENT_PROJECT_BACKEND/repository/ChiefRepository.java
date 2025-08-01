package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChiefDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chief;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Repository interface for Chief entity.
 * </p>
 * Provides methods for performing database operations related to chiefs.
 */
@Repository
public interface ChiefRepository extends JpaRepository<Chief, Integer> {

    /**
     * Finds a chief by full name (case-insensitive and trimmed).
     *
     * @param name Full name of the chief (first name + space + last name).
     * @return The matching Chief entity, or null if not found.
     */
    @Query("SELECT c FROM Chief c WHERE LOWER(TRIM(c.personId.firstName || ' ' || c.personId.lastName)) = LOWER(TRIM(:fullName))")
    Chief findChiefByName(@Param("fullName") String name);

    /**
     * Finds a chief by their id.
     *
     * @param id ID of the chief.
     * @return The Chief entity with given ID, or null if not found.
     */
    @Query("""
        SELECT c FROM Chief c
        WHERE c.id = :id
    """)
    Chief findChiefById(@Param("id") int id);

    /**
     * Finds a chief by their registration number.
     *
     * @param registrationNumber Registration number of the chief's associated person.
     * @return The Chief entity linked to the registration number, or null if not found.
     */
    @Query("""
        SELECT c FROM Chief c
        JOIN c.personId p
        WHERE p.registrationNumber = :registrationNumber
    """)
    Chief findChiefByRegistrationNumber(@Param("registrationNumber") String registrationNumber);

    /**
     * Finds driver's chief by the driver's registration number.
     *
     * @param registrationNumber Registration number of the driver.
     * @return The Chief entity associated with the driver, or null if not found.
     */
    @Query("""
        SELECT d.chiefId FROM Driver d
        WHERE d.personId.registrationNumber = :registrationNumber
    """)
    Chief findPersonChiefByRegistrationNumber(@Param("registrationNumber") String registrationNumber);

    /**
     * Retrieves a list of active chiefs as ChiefDTOs (id, person id, first name, last name).
     *
     * @return List of active ChiefDTOs.
     */
    @Query("""
        SELECT
            c.id,
            p.id,
            p.firstName,
            p.lastName
        FROM Chief c
        JOIN c.personId p
        WHERE c.isActive = true
    """)
    List<ChiefDTO> findActiveChiefs();

}
