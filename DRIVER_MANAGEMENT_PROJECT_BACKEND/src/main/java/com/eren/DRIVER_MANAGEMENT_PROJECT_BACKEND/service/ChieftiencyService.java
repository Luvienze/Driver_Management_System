package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChieftaincyDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chieftiency;
import java.util.List;

/**
 * Service interface for managing Chieftaincy entities.
 */
public interface ChieftiencyService {

    /**
     * Retrieves a list of chieftaincies.
     *
     * @return List of chieftaincies.
     */
    List<ChieftaincyDTO> findAllChieftiencies();
}
