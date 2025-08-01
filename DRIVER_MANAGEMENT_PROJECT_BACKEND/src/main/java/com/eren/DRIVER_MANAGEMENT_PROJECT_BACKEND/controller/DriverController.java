package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.*;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Driver;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Genders;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.ChiefService;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.DriverService;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.PersonService;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.RoleService;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.wrapper.PersonDriverRequestDto;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import javax.print.attribute.standard.Media;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.time.LocalDate;
import java.util.List;
import java.util.UUID;

@Slf4j
@RestController
@RequestMapping(value = "/driver")
@Tag(name = "Driver API", description = "Endpoints are managing driver related operations.")
public class DriverController {

    @Autowired
    private DriverService driverService;
    @Autowired
    private PersonService personService;
    @Autowired
    private ChiefService chiefService;
    @Autowired
    private RoleService roleService;
    @Value("${external.images.path}")
    private String imageUploadPath;

    /**
     * Retrieves the list of all drivers.
     *
     * @return a list of DriverDTO wrapped in a ResponseEntity
     */
    @Operation(
            summary = "Get all drivers",
            description = "Get all drivers as DTO's"
    )
    @PostMapping(value = "/list")
    public ResponseEntity<List<DriverDTO>> getAllDrivers() {
        return ResponseEntity.ok(driverService.getAllDriversDTO());
    }

    /**
     * Finds a driver by their registration number.
     *
     * @param registrationNumber the registration number of the driver
     * @return the DriverDTO wrapped in a ResponseEntity
     */
    @Operation(
            summary = "Finds driver by their registration number",
            description = "Finds driver by their registration number as DTO's"
    )
    @PostMapping(value = "/find/registrationNumber")
    public ResponseEntity<DriverDTO> getDriverByRegistrationNumber(
            @Parameter(description = "Registration number of driver", required = true)
            @RequestParam String registrationNumber) {
            return ResponseEntity.ok(driverService.findDriverByRegistrationNumber(registrationNumber));
    }

    /**
     * Sets the active status of a driver.
     *
     * @param driverDTO the driver data containing the ID and active status
     * @return the updated DriverDTO wrapped in a ResponseEntity
     */
    @Operation(
            summary = "Sets the active status of a driver",
            description = "Sets driver's active status by driver dto and returns as DTO's"
    )
    @PostMapping(value = "/set/active")
    public ResponseEntity<DriverDTO> setActiveDriver(
            @Parameter(description = "Data transfer object of driver", required = true)
            @RequestBody DriverDTO driverDTO) {
        Integer id = driverDTO.getId();
        if(id == null){
            System.out.println("id is null");
        }
        driverService.updateIsActive(id, driverDTO.getIsActive());
        DriverDTO dto = driverService.findDriverById(id);
        return ResponseEntity.ok(dto);
    }

    /**
     * Updates a driver based on their registration number.
     *
     * @param driverDTO the driver data to be updated
     * @return a message indicating the result of the update
     */
    @Operation(
            summary = "Updates driver by registration number",
            description = "Updated driver by driver dto"
    )
    @CrossOrigin(origins = "*")
    @PostMapping(value = "/update")
    public ResponseEntity<String> updateDriverByRegistrationNumber(
            @Parameter(description = "Data transfer object of driver", required = true)
            @RequestBody DriverDTO driverDTO) {
        DriverDTO updatedDriver = driverService.updateDriverByRegistrationNumber(driverDTO);
        if (updatedDriver == null) {
            return ResponseEntity.badRequest().body("Failed to update driver. Check if registrationNumber, garage, or chief exists.");
        }
        return ResponseEntity.ok("Driver updated successfully");
    }

    /**
     * Gets the number of drivers who have the day off on the specified date.
     *
     * @param day optional date, defaults to today if not provided
     * @return the number of drivers on day off
     */
    @Operation(
            summary = "Get number of drivers on day off",
            description = "Returns the count of drivers who have the day off on a specified date. If no date is provided, defaults to today."
    )
    @PostMapping("/daily/dayOff")
    public ResponseEntity<Integer> getDriverOnDayOff(
            @Parameter(description = "Local date")
            @RequestParam(required = false)LocalDate day){
        if (day == null) {
            day = LocalDate.now();
        }
        return ResponseEntity.ok(driverService.getDriverOnDayOff(day));
    }

    /**
     * Gets the number of drivers by gender.
     *
     * @param gender the gender value (e.g., 0 = male, 1 = female)
     * @return the number of drivers matching the gender
     */
    @Operation(
            summary = "Get driver count by gender",
            description = "Returns the number of drivers filtered by gender. Gender is represented by integers (e.g., 0 = male, 1 = female)."
    )
    @PostMapping("/daily/gender")
    public ResponseEntity<Integer> getDriverGenderCount(
            @Parameter(description = "Gender as integer")
            @RequestParam(required = false)int gender){
        if (gender < 0) {
            return ResponseEntity.badRequest().body(0);
        }
        return ResponseEntity.ok(driverService.getDriverGenderCount(gender));
    }

    /**
     * Retrieves a list of active drivers under a specific chief.
     *
     * @param chiefId the ID of the chief
     * @return a list of active DriverDTOs associated with the chief
     */
    @Operation(
            summary = "Find active drivers by chief ID",
            description = "Retrieves all active drivers assigned to a chief identified by the given chiefId."
    )
    @PostMapping(value = "/list/active")
    public ResponseEntity<List<DriverDTO>> findActiveDriversByChief(
            @Parameter(description = "Identifier of chief", required = true)
            @RequestParam int chiefId) {
        ChiefDTO chiefDTO = chiefService.findChiefById(chiefId);
        if(chiefDTO == null){
            return ResponseEntity.badRequest().body(null);
        }

        return new ResponseEntity<>(driverService.findActiveDriversByChief(chiefDTO.getId(), true), HttpStatus.OK);
    }


    /**
     * Adds a new driver and associated person.
     *
     * @param personDriverRequestDto the combined person and driver data
     * @return the saved PersonDTO wrapped in a ResponseEntity
     */
    @Operation(
            summary = "Adds a new driver and associated person",
            description = "Adds a new driver and associated person by wrapper dto"
    )
    @PostMapping(value = "/add", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
    public ResponseEntity<PersonDTO> addNewDriver(
            @Parameter(description = "Wrapper class of person and driver", required = true)
            @RequestPart("data") PersonDriverRequestDto personDriverRequestDto,
            @RequestPart(value = "file", required = false) MultipartFile file)
    {
        log.debug("New Driver: " + personDriverRequestDto);
        PersonDTO personDTO = personDriverRequestDto.getPersonDto();
        DriverDTO driverDTO = personDriverRequestDto.getDriverDto();
    
        if (file != null && !file.isEmpty()) {
            try {
                String filename = UUID.randomUUID() + "_" + file.getOriginalFilename();
                Path uploadDir = Paths.get(imageUploadPath);
                if (!Files.exists(uploadDir)) Files.createDirectories(uploadDir);

                Path targetPath = uploadDir.resolve(filename);
                Files.copy(file.getInputStream(), targetPath, StandardCopyOption.REPLACE_EXISTING);

                personDTO.setImageUrl(filename);
                log.info("Saved file to: {}", targetPath.toAbsolutePath());
            } catch (IOException e) {
                log.error("Image saving failed", e);
                return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).build();
            }
        }

        if(personDTO == null){
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }else{
            personService.saveOrUpdatePerson(personDTO);
            PersonDTO person = personService.findPersonByRegistrationNumber(personDTO.getRegistrationNumber());
            if(person == null){
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }
            roleService.addPerson(person);

            driverService.saveOrUpdateDriver(driverDTO);

            DriverDTO driver = driverService.findDriverByRegistrationNumber(personDTO.getRegistrationNumber());
            if(driver == null){
                return  new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }
            return ResponseEntity.ok(person);

        }
    }
}