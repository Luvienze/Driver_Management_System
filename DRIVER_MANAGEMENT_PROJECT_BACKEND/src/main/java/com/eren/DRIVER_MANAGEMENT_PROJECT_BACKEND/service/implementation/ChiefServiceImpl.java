package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.ChiefRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.GarageRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.PersonRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChiefDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chief;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.ChiefService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.ArrayList;
import java.util.List;

/**
 * Service implementation for managing Chief entities and related operations.
 */
@Service
public class ChiefServiceImpl implements ChiefService {

    @Autowired
    private ChiefRepository chiefRepository;
    @Autowired
    private PersonRepository personRepository;
    @Autowired
    private GarageRepository garageRepository;

    /**
     * Finds a chief by their registration number.
     *
     * @param registrationNumber Registration number of the chief.
     * @return {@link Chief} if found, or null if the person does not exist.
     */
    @Override
    public ChiefDTO findChiefByRegistrationNumber(String registrationNumber) {
        if(personRepository.findPersonByRegistrationNumber(registrationNumber) == null) {
            return null;
        }

        Chief chief = chiefRepository.findChiefByRegistrationNumber(registrationNumber);
        return convertToChiefDTO(chief);
    }

    /**
     * Finds a driver's chief by driver's registration number.
     *
     * @param registrationNumber Registration number of the person.
     * @return {@link Chief} if found, or null if the person does not exist.
     */
    @Override
    public ChiefDTO findPersonChiefByRegistrationNumber(String registrationNumber) {
        if(personRepository.findPersonByRegistrationNumber(registrationNumber) == null) {
            return null;
        }

        Chief chief = chiefRepository.findPersonChiefByRegistrationNumber(registrationNumber);
        return convertToChiefDTO(chief);
    }

    /**
     * Finds a chief by their unique ID.
     *
     * @param id the chief's ID.
     * @return corresponding {@link ChiefDTO}, or null if not found.
     */
    @Override
    public ChiefDTO findChiefById(int id) {
        Chief chief = chiefRepository.findChiefById(id);
        if(chief == null) {
            return null;
        }
        return convertToChiefDTO(chief);
    }

    /**
     * Retrieves all active chiefs.
     *
     * @return list of {@link ChiefDTO} for active chiefs.
     */
    @Override
    public List<ChiefDTO> findActiveChiefs() {
        return chiefRepository.findActiveChiefs();
    }

    /**
     * Converts a {@link Chief} entity to a {@link ChiefDTO}.
     *
     * @param chief the chief entity.
     * @return the corresponding DTO with person and garage info included.
     */
    private ChiefDTO convertToChiefDTO(Chief chief) {
        Person person = personRepository.findPersonById(chief.getId());
        Garage garage = garageRepository.getGarageById(chief.getId());

        ChiefDTO chiefDTO = new ChiefDTO();
        chiefDTO.setId(chief.getId());
        chiefDTO.setPersonId(person.getId());
        chiefDTO.setChiefFirstName(person.getFirstName());
        chiefDTO.setChiefLastName(person.getLastName());
        chiefDTO.setGarageName(garage.getGarageName());
        chiefDTO.setGarageId(garage.getId());
        chiefDTO.setGarageName(garage.getGarageName());
        chiefDTO.setActive(chief.getIsActive());

        return chiefDTO;
    }

}
