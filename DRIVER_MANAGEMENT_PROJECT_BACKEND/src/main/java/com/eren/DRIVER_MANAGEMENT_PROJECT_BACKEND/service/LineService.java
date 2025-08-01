package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.LineDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Line;
import java.util.List;

/**
 * Service interface for managing Line entities.
 */
public interface LineService {

    /**
     * Retrieves a line by its unique line code.
     *
     * @param lineCode The unique code of the line.
     * @return The corresponding LineDTO if found, or null otherwise.
     */
    LineDTO getLineByLineCode(String lineCode);

    /**
     * Retrieves all lines in the system.
     *
     * @return List of LineDTOs representing all lines.
     */
    List<LineDTO> getAllLines();
}
