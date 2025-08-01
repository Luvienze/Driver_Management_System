package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chieftiency;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

/**
 * Repository interface for Chieftaincy entity.
 * </p>
 * Provides methods for performing database operations related to chieftaincies.
 */
@Repository
public interface ChieftiencyRepository extends JpaRepository<Chieftiency, Integer> {
}
