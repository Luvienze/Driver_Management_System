package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChieftaincyDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.ChieftiencyRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chieftiency;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.GarageRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.ChieftiencyService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

/**
 * Service interface for managing Chieftaincy entities.
 */
@Service
public class ChieftiencyServiceImpl implements ChieftiencyService {

    @Autowired
    private ChieftiencyRepository chieftiencyRepository;
    @Autowired
    private GarageRepository garageRepository;

    /**
     * Retrieves all chieftaincies from the repository and converts them to DTOs.
     *
     * @return a list of {@link ChieftaincyDTO} representing all chieftaincies.
     */
    @Override
    public List<ChieftaincyDTO> findAllChieftiencies() {
        return chieftiencyRepository.findAll()
                .stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    /**
     * Converts a {@link Chieftiency} entity to a {@link ChieftaincyDTO}.
     *
     * @param chieftiency the entity to convert.
     * @return the converted DTO with garage details included.
     */
    private ChieftaincyDTO convertToDTO(Chieftiency chieftiency) {

        Garage garage = garageRepository.getGarageById(chieftiency.getGarageId().getId());
        ChieftaincyDTO dto = new ChieftaincyDTO();
        dto.setId(chieftiency.getId());
        dto.setChieftiencyName(chieftiency.getChieftiencyName());
        dto.setGarageId(garage.getId());
        dto.setGarageName(garage.getGarageName());
        return dto;
    }

}
