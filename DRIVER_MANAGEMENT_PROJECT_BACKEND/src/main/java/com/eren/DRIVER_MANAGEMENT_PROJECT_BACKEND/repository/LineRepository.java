package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Line;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

/**
 * Repository interface for Line entity.
 * </p>
 * Provides methods for performing database operations related to lines.
 */
@Repository
public interface LineRepository extends JpaRepository<Line, Integer> {

    /**
     * Finds line by their line code.
     *
     * @param lineCode Line code of line.
     * @return The matching line entity, or null if not found
     */
    @Query("""
        SELECT l FROM Line l
        WHERE l.lineCode = :lineCode
    """)
    Line findLineByLineCode(String lineCode);
}
