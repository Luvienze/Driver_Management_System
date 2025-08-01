package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

/**
 * Repository interface for Person entity.
 * </p>
 * Provides methods for performing database operations related to persons.
 */
@Repository
public interface PersonRepository extends JpaRepository<Person, Integer> {

    /**
     * Finds person by associated driver's person id.
     *
     * @param id ID of the driver.
     * @return The matching Person entity, or null if not found.
     */
    @Query("""
        SELECT p FROM Person p
        LEFT JOIN Driver d ON p.id = d.personId.id
        WHERE d.id = :id
    """)
    Person findPersonByDriverId(@Param("id") int id);

    /**
     * Finds person by their id.
     *
     * @param id ID of the person.
     * @return The matching Person entity, or null if not found.
     */
    @Query("""
        SELECT p FROM Person p
        WHERE p.id = :id
    """)
    Person findPersonById(@Param("id") int id);

    /**
     * Finds person by driver of chief's registration number.
     *
     * @param registrationNumber Registration number of driver or chief.
     * @return The matching Person entity, or null if not found.
     */
    @Query("""
        SELECT p FROM Person p
        WHERE p.registrationNumber = :registrationNumber
    """
    )
    Person findPersonByRegistrationNumber(@Param("registrationNumber") String registrationNumber);

    /**
     * Set person's isDeleted field to true by chief or driver's registration number.
     *
     * @param registrationNumber Registration number of driver or chief.
     */
    @Transactional
    @Modifying
    @Query("""
        UPDATE Person p
        SET p.isDeleted = true
        WHERE p.registrationNumber = :registrationNumber
    """)
    void deletePersonByRegistrationNumber(@Param("registrationNumber") String registrationNumber);
}
