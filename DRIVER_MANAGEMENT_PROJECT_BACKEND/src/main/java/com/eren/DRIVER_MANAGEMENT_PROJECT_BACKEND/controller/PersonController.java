package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.DriverDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.PersonDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Driver;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.DriverService;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.wrapper.PersonDriverRequestDto;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.PersonService;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/person")
@Tag(name = "Person API", description = "Endpoints are managing person related operations.")
public class PersonController {

    @Autowired
    PersonService personService;
    @Autowired
    DriverService  driverService;

    /**
     * Finds a Person by their Driver ID.
     *
     * @param id the driver ID
     * @return the PersonDTO associated with the given driver ID
     */
    @Operation(
            summary = "Find person by driver ID",
            description = "Retrieves the person details associated with the given driver ID."
    )
    @PostMapping(value = "/find")
    public PersonDTO findPersonByDriverId(
            @Parameter(description = "Identifier of driver", required = true)
            @RequestParam("id") int id){
        return personService.findPersonByDriverId(id);
    }

    /**
     * Finds a Person by their registration number.
     *
     * @param registrationNumber the registration number of the person
     * @return the PersonDTO associated with the given registration number
     */
    @Operation(
            summary = "Find person by registration number",
            description = "Retrieves the person details using their registration number."
    )
    @PostMapping(value = "/find/registrationNumber")
    public PersonDTO findPersonByRegistrationNo(
            @Parameter(description = "Registration number of person", required = true)
            @RequestParam("registrationNumber") String registrationNumber){
        return personService.findPersonByRegistrationNumber(registrationNumber);
    }

    /**
     * Finds both Person and Driver information by registration number.
     *
     * @param registrationNumber the registration number of the person/driver
     * @return ResponseEntity containing PersonDriverRequestDto if found, or BAD_REQUEST/NOT_FOUND if invalid or missing
     */
    @Operation(
            summary = "Find combined person and driver info by registration number",
            description = "Retrieves combined person and driver details based on registration number."
    )
    @PostMapping(value = "/find/driver")
    public ResponseEntity<PersonDriverRequestDto> findDriverPersonInfoByRegistrationNo(
            @Parameter(description = "Registration number of person", required = true)
            @RequestParam("registrationNumber") String registrationNumber){
        if (registrationNumber == null || registrationNumber.isEmpty()){
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        PersonDTO personDTO = personService.findPersonByRegistrationNumber(registrationNumber);
        if (personDTO == null){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        DriverDTO driverDTO = driverService.findDriverByRegistrationNumber(registrationNumber);
        if (driverDTO == null){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(new PersonDriverRequestDto(personDTO, driverDTO), HttpStatus.OK);
    }

    /**
     * Retrieves a list of all persons.
     *
     * @return ResponseEntity containing a list of PersonDTOs
     */
    @Operation(
            summary = "Get all persons",
            description = "Retrieves a list of all persons in the system."
    )
    @PostMapping(value = "/list")
    public ResponseEntity<List<PersonDTO>> getAllPersons(){
        return ResponseEntity.ok(personService.getAllPersonsDTO());
    }

    /**
     * Saves or updates a Person record.
     *
     * @param personDTO the person data to save or update
     * @return ResponseEntity containing the saved or updated PersonDTO
     */
    @Operation(
            summary = "Save or update person",
            description = "Creates a new person or updates an existing one."
    )
    @PostMapping(value = "/saveOrUpdate")
    public ResponseEntity<PersonDTO> saveOrUpdatePerson(
            @Parameter(description = "Data transfer object of person", required = true)
            @RequestBody PersonDTO personDTO){
        PersonDTO savedPerson = personService.saveOrUpdatePerson(personDTO);
        return ResponseEntity.ok(savedPerson);
    }

    /**
     * Deletes a person by registration number by marking them inactive and deleted.
     *
     * @param registrationNumber the registration number of the person to delete
     */
    @Operation(
            summary = "Delete person by registration number",
            description = "Marks the person and their associated driver as inactive/deleted by registration number."
    )
    @PostMapping(value = "/delete")
    public void deletePersonByRegistrationNumber(
            @Parameter(description = "Registration number of person", required = true)
            @RequestParam("registrationNumber") String registrationNumber){
        // Find driver by registration number
        DriverDTO driver = driverService.findDriverByRegistrationNumber(registrationNumber);

        // Set active to false
        driverService.updateIsActive(driver.getId(), false);

        // Set person is deleted to true
        personService.deletePersonByRegistrationNumber(registrationNumber);
    }

}