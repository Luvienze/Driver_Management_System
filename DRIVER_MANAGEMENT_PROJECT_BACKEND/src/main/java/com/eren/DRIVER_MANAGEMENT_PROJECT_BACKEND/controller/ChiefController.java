package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChiefDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chief;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation.ChiefServiceImpl;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import java.util.List;

@RestController
@RequestMapping(value = "/chief")
@Tag(name = "Chief API", description = "Endpoints are managing chief related operations.")
public class ChiefController {

    @Autowired
    private ChiefServiceImpl chiefService;

    /**
     * Finds a Chief by their ID.
     *
     * @param id the ID of the Chief to find
     * @return ResponseEntity containing the ChiefDTO if found, or BAD_REQUEST if the ID is invalid
     */
    @Operation(
            summary = "Find Chief by ID",
            description = "Returns the Chief details based on the given ID"
    )
    @PostMapping(value = "/find")
    public ResponseEntity<ChiefDTO> findChiefById(
            @Parameter(description = "ID of the Chief", required = true)
            @RequestParam Integer id){
        if (id == null || id <= 0){
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
        return new ResponseEntity<>(chiefService.findChiefById(id), HttpStatus.OK);
    }

    /**
     * Retrieves a Chief by their registration number.
     *
     * @param registrationNumber the registration number of the Chief
     * @return ResponseEntity containing the ChiefDTO if found, or NOT_FOUND if no Chief with that registration number exists
     */
    @Operation(
            summary = "Get Chief by registration number",
            description = "Fetches Chief information using registration number"
    )
    @PostMapping(value = "/find/registrationNumber")
    public ResponseEntity<ChiefDTO> getChiefByRegistrationNumber(
            @Parameter(description = "Registration number of the Chief", required = true)
            @RequestParam String registrationNumber) {
        if (chiefService.findChiefByRegistrationNumber(registrationNumber) == null) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(chiefService.findChiefByRegistrationNumber(registrationNumber), HttpStatus.OK);
    }

    /**
     * Retrieves the Chief assigned to a driver by the driver's registration number.
     *
     * @param registrationNumber the registration number of the driver
     * @return ResponseEntity containing the ChiefDTO if found, or NOT_FOUND if no Chief is assigned to the driver
     */
    @Operation(
            summary = "Get Chief of a driver by registration number",
            description = "Returns the Chief assigned to the driver with the given registration number"
    )
    @PostMapping(value = "/find/driver")
    public ResponseEntity<ChiefDTO> getDriverChiefByRegistrationNumber(
            @Parameter(description = "Registration number of the driver", required = true)
            @RequestParam String registrationNumber) {
        if (chiefService.findPersonChiefByRegistrationNumber(registrationNumber) == null) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(chiefService.findPersonChiefByRegistrationNumber(registrationNumber), HttpStatus.OK);
    }

    /**
     * Retrieves a list of active Chiefs as DTOs.
     *
     * @return List of ChiefDTOs representing active Chiefs
     */
    @Operation(
            summary = "Get all active Chiefs",
            description = "Returns a list of all active Chief records"
    )
    @PostMapping(value = "/list/name")
    public List<ChiefDTO> getAllChiefList() {
        return chiefService.findActiveChiefs();
    }
}
