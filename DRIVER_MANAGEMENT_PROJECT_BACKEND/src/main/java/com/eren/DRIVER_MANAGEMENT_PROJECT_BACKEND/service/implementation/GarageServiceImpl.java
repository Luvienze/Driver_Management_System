package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.GarageRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.PersonRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.GarageDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.GarageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class GarageServiceImpl implements GarageService {

    @Autowired
    private GarageRepository garageRepository;
    @Autowired
    private PersonRepository personRepository;

    /**
     * Retrieves all garages from the database.
     *
     * @return List of all {@link Garage} entities.
     */
    @Override
    public List<Garage> getAllGarages() {
        return garageRepository.findAll();
    }

    /**
     * Retrieves a list of garages with their names mapped as {@link GarageDTO}.
     *
     * @return List of {@link GarageDTO} containing garage names and basic info.
     */
    @Override
    public List<GarageDTO> getGarageNameList() {
        return convertToGarageNameDTO(garageRepository.getGarageNameList());
    }

    /**
     * Finds a garage by the registration number of the associated person.
     *
     * @param registrationNumber The registration number of the person.
     * @return {@link GarageDTO} corresponding to the garage if found; {@code null} otherwise.
     */
    @Override
    public GarageDTO getGarageByRegistrationNumber(String registrationNumber) {
        if(personRepository.findPersonByRegistrationNumber(registrationNumber) == null) {
            System.out.println("Person no found ");
            return null;
        }
        Garage garage = garageRepository.getGarageByRegistrationNumber(registrationNumber);
        return convertToDto(garage);
    }

    /**
     * Converts a list of {@link Garage} entities to a list of {@link GarageDTO}.
     *
     * @param garages List of garages to convert.
     * @return List of converted {@link GarageDTO}.
     */
    private List<GarageDTO> convertToGarageNameDTO(List<Garage> garages) {
        List<GarageDTO> garageDTOList = new ArrayList<>();
        for (Garage garage : garages) {
            garageDTOList.add(convertToDto(garage));
        }
        return garageDTOList;
    }

    /**
     * Converts a {@link Garage} entity to a {@link GarageDTO}.
     *
     * @param garage The garage entity to convert.
     * @return The corresponding {@link GarageDTO}.
     */
    private GarageDTO convertToDto(Garage garage) {
        GarageDTO garageDTO = new GarageDTO();
        garageDTO.setId(garage.getId());
        garageDTO.setGarageName(garage.getGarageName());
        garageDTO.setGarageAddress(garage.getGarageAddress());
        return garageDTO;
    }
}
