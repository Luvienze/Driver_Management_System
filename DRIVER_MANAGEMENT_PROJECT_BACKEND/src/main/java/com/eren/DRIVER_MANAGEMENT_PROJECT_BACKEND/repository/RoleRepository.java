package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Role;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

/**
 * Repository interface for Role entity.
 * </p>
 * Provides methods for performing database operations related to role.
 */
@Repository
public interface RoleRepository extends JpaRepository<Role, Integer> {

    /**
     * Retrieves a list of roles by person.
     *
     * @param person Person entity for the person column in role table.
     * @return Retrieves a list of roles, or null if not found
     */
    @Query("""
        SELECT r FROM Role r
        WHERE r.person = :person
    """)
    List<Role> findRolesByPersonId(@Param("person") Person person);

    /**
     * Retrieves a person's list of roles by person registration and phone number.
     *
     * @param regNo Registration number of driver or chief.
     * @param phone Phone number of person.
     * @return Retrieves a person's list of roles by phone and registration number, or null if not found.
     */
    @Query("""
        SELECT r FROM Role r
        LEFT JOIN FETCH r.person p
        WHERE p.registrationNumber = :regNo AND p.phone = :phone
    """)
    List<Role> findRolesByRegNoAndPhone(@Param("regNo") String regNo, @Param("phone") String phone);

}
