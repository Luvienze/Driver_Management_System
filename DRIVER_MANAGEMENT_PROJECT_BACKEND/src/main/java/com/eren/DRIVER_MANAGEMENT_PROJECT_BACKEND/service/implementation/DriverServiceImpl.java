package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.ChiefRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.DriverRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.GarageRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.PersonRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.DriverDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chief;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Driver;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.CadreTypes;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Days;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Genders;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.DriverService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.DayOfWeek;
import java.time.LocalDate;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class DriverServiceImpl implements DriverService {

    @Autowired
    DriverRepository driverRepository;
    @Autowired
    PersonRepository personRepository;
    @Autowired
    GarageRepository garageRepository;
    @Autowired
    ChiefRepository chiefRepository;

    /**
     * Finds a driver by their ID.
     *
     * @param id Identifier of the driver.
     * @return a {@link DriverDTO} representing the driver, or null if not found.
     */
    @Override
    public DriverDTO findDriverById(Integer id) {
        Driver driver = driverRepository.findDriverById(id);
        return convertToDto(driver);
    }

    /**
     * Finds a driver by their registration number.
     *
     * @param registrationNumber the registration number of the driver.
     * @return a {@link DriverDTO} representing the driver, or null if not found.
     */
    @Override
    public DriverDTO findDriverByRegistrationNumber(String registrationNumber) {
       Driver driver = driverRepository.findDriverByRegistrationNumber(registrationNumber);
       return convertToDto(driver);
    }

    /**
     * Retrieves all drivers as DTOs.
     *
     * @return a list of {@link DriverDTO} representing all drivers.
     */
    @Override
    public List<DriverDTO> getAllDriversDTO() {
        return driverRepository.getAllDrivers().
                stream()
                .map(this::convertToDto)
                .collect(Collectors.toList());
    }

    /**
     * Finds active or inactive drivers under a specific chief.
     *
     * @param chiefId the ID of the chief.
     * @param isActive whether to find active (true) or inactive (false) drivers.
     * @return a list of {@link DriverDTO} filtered by chief and active status.
     */
    @Override
    public List<DriverDTO> findActiveDriversByChief(int chiefId, Boolean isActive) {
        Chief chief = chiefRepository.findChiefById(chiefId);
        return driverRepository.findActiveDriversByChief(chief,isActive)
                .stream()
                .map(this::convertToDto)
                .collect(Collectors.toList());
    }

    /**
     * Saves a new driver or updates an existing one.
     *
     * @param dto the driver data transfer object containing driver information.
     * @return the saved or updated {@link DriverDTO}.
     * @throws RuntimeException if related person or chief cannot be found.
     */
    @Override
    public DriverDTO saveOrUpdateDriver(DriverDTO dto) {
        Driver driver = new Driver();
        Person person = personRepository.findPersonByRegistrationNumber(dto.getRegistrationNumber());
        if (person == null) {
            throw new RuntimeException("Person not found with registration number: " + dto.getRegistrationNumber());
        }

        Chief chief = chiefRepository.findChiefById(dto.getChiefId());
        if (chief == null) {
            throw new RuntimeException("Chief not found with id: " + dto.getChiefId());
        }

        Integer id = driver.getId();
        if(id != null && id > 0){
            driver = driverRepository.findDriverById(id);

            if (driver == null){
                throw new RuntimeException("Driver with id " + id + " not found.");
            }
            driver.setPersonId(person);
            driver.setGarageId(chief.getGarageId());
            driver.setChiefId(chief);
            driver.setCadre(CadreTypes.values()[dto.getCadre()]);
            driver.setDayOff(Days.values()[dto.getDayOff()]);
            driver.setIsActive(dto.getIsActive());
        } else {
            driver = new Driver();
            driver.setPersonId(person);
            driver.setGarageId(chief.getGarageId());
            driver.setChiefId(chief);
            driver.setCadre(CadreTypes.values()[dto.getCadre()]);
            driver.setDayOff(Days.values()[dto.getDayOff()]);
            driver.setIsActive(dto.getIsActive());

        }
        Driver savedDriver = driverRepository.save(driver);
        return convertToDto(savedDriver);
    }

    /**
     * Updates the 'isActive' status of a driver by their ID.
     *
     * @param driverId the ID of the driver.
     * @param isActive the new active status.
     * @return null (can be modified to return updated driver DTO if desired).
     */
    @Override
    public DriverDTO updateIsActive(Integer driverId, boolean isActive) {
        if (driverId == null ) {
            System.out.println("Driver Not Found");
        }
        driverRepository.findById(driverId).ifPresent(driver -> {
            driverRepository.updateIsActive(isActive, driverId);
        });
        return null;
    }

    /**
     * Updates a driver identified by registration number with the information in the DTO.
     *
     * @param dto the driver data transfer object containing updated information.
     * @return updated {@link DriverDTO}, or null if driver or related entities are not found.
     */
    @Transactional
    @Override
    public DriverDTO updateDriverByRegistrationNumber(DriverDTO dto) {
        if(dto != null) {
            String registrationNumber = dto.getRegistrationNumber();
            if(registrationNumber == null) {
                return null;
            }
            Driver driver = driverRepository.findDriverByRegistrationNumber(registrationNumber);
            System.out.println(driver);
            if(driver == null) {
                return null;
            }
            else{

                String chiefFullName = dto.getChiefFirstName() + " " + dto.getChiefLastName();
                Chief chiefId = chiefRepository.findChiefByName(chiefFullName);
                if(chiefId == null) {
                    return null;
                }

                Garage garageId = garageRepository.findGarageByGarageName(dto.getGarage());
                if(garageId == null) {
                    return null;
                }

                CadreTypes cadreType = CadreTypes.values()[dto.getCadre()];
                if(cadreType == null) {
                    return null;
                }
                Days dayOff = Days.values()[dto.getDayOff()];
                if(dayOff == null) {
                    return null;
                }
                Boolean isActive = dto.getIsActive();
                if (isActive == null) isActive = false;

                driverRepository.updateDriverByRegistrationNumber(registrationNumber,
                        garageId,
                        chiefId,
                        cadreType,
                        dayOff,
                        isActive);
                driver = driverRepository.findDriverByRegistrationNumber(registrationNumber);
                return convertToDto(driver);
            }
        }
        return null;
    }

    /**
     * Returns the count of drivers who have a day off on the specified date.
     *
     * @param day the date to check.
     * @return the number of drivers with a day off on the given day.
     */
    @Override
    public int getDriverOnDayOff(LocalDate day) {

        DayOfWeek dayOfWeek = day.getDayOfWeek();
        String formattedDay = dayOfWeek.name().charAt(0) + dayOfWeek.name().substring(1).toLowerCase();

        Days dayEnum = Days.valueOf(formattedDay.toUpperCase());

        return driverRepository.getDriverOnDayOff(dayEnum);
    }

    /**
     * Returns the count of drivers filtered by gender.
     *
     * @param gender the integer index of the gender enum.
     * @return the count of drivers of the specified gender.
     */
    @Override
    public int getDriverGenderCount(int gender) {
        Genders genders = Genders.values()[gender];
        return driverRepository.getDriverGenderCount(genders);
    }

    /**
     * Converts a {@link Driver} entity to a {@link DriverDTO}.
     *
     * @param d the driver entity.
     * @return the corresponding driver DTO.
     */
    private DriverDTO convertToDto(Driver d) {
        Person p = d.getPersonId();
        Chief cd = d.getChiefId();
        Garage g = d.getGarageId();
        Person cp = cd.getPersonId();

        DriverDTO dto = new DriverDTO();
        dto.setId(d.getId());
        dto.setPersonFirstName(p.getFirstName());
        dto.setPersonLastName(p.getLastName());
        dto.setPhoneNumber(p.getPhone());
        dto.setRegistrationNumber(p.getRegistrationNumber());
        dto.setGarage(g.getGarageName());
        dto.setChiefId(cd.getId());
        dto.setChiefFirstName(cp.getFirstName());
        dto.setChiefLastName(cp.getLastName());
        dto.setCadre(d.getCadre().ordinal());
        dto.setDayOff(d.getDayOff().ordinal());
        dto.setIsActive(d.getIsActive());
        return dto;
    }

}