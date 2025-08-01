package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.GarageDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Garage;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation.GarageServiceImpl;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.apache.coyote.Response;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import java.util.List;

@RestController
@RequestMapping(value = "/garage")
@Tag(name = "Garage API", description = "Endpoints are managing garage related operations")
public class GarageController {

    @Autowired
    private GarageServiceImpl garageService;

    /**
     * Retrieves a list of all garages.
     *
     * @return a list of Garage entities
     */
    @Operation(summary = "Get all garages", description = "Retrieves a list of all garage entities.")
    @PostMapping(value = "/list")
    public List<Garage> getAllGarage() {
        return garageService.getAllGarages();
    }

    /**
     * Retrieves a garage by its registration number.
     *
     * @param registrationNumber the unique registration number of the garage
     * @return the GarageDTO if found, otherwise returns BAD_REQUEST if input is invalid
     */
    @Operation(summary = "Find garage by registration number",
    description = "Fetches garage information using the registration number.")
    @PostMapping(value = "/find/registrationNumber")
    public ResponseEntity<GarageDTO> getGarageByRegistrationNumber(
            @Parameter(description = "The registration number of the garage", required = true)
            @RequestParam String registrationNumber) {
        if(registrationNumber == null || registrationNumber.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
        return new ResponseEntity<>(garageService.getGarageByRegistrationNumber(registrationNumber), HttpStatus.OK);
    }

    /**
     * Retrieves a list of all garage names.
     *
     * @return a list of GarageDTOs containing only garage name data
     */
    @Operation(summary = "Get all garage names",
            description = "Returns a list containing only the names of all garages.")
    @PostMapping(value = "/list/name")
    public ResponseEntity<List<GarageDTO>> getAllGarageNameList() {
        return new ResponseEntity<>(garageService.getGarageNameList(), HttpStatus.OK);
    }
}
