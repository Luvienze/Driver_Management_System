package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChiefDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.VehicleDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.ChiefService;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.VehicleService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(value = "/vehicle")
@Tag(name = "Vehicle API", description = "Endpoints are managing vehicle related operations.")
public class VehicleController {

    @Autowired
    private VehicleService vehicleService;
    @Autowired
    private ChiefService chiefService;

    /**
     * Retrieves a list of all vehicles.
     *
     * @return ResponseEntity containing the list of all VehicleDTO objects with HTTP status 200 (OK)
     */
    @Operation(summary = "Retrieve all vehicles",
            description = "Returns a list of all vehicles in the system.")
    @PostMapping("/list")
    public ResponseEntity<List<VehicleDTO>> findAll() {
        return new ResponseEntity<>(vehicleService.getAllVehicles(), HttpStatus.OK);
    }

    /**
     * Retrieves a vehicle by its door number.
     *
     * @param doorNo the door number of the vehicle to find
     * @return ResponseEntity containing the VehicleDTO if found, or HTTP status 400 (BAD_REQUEST) if the door number is null or empty
     */
    @Operation(summary = "Retrieve vehicle by door number",
            description = "Returns vehicle details for the given door number. " +
                    "Returns 400 if door number is null or empty.")
    @PostMapping(value = "/find")
    public ResponseEntity<VehicleDTO> getVehicleByDoorNo (
            @Parameter(description = "Door number of vehicle", required = true)
            @RequestParam String doorNo) {
        if(doorNo == null || doorNo.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
        return new ResponseEntity<>(vehicleService.getVehicleByDoorNo(doorNo), HttpStatus.OK);
    }

    /**
     * Retrieves a list of vehicles by the garage ID associated with the given Chief ID.
     *
     * @param id the ID of the Chief whose garage vehicles to retrieve
     * @return ResponseEntity containing the list of VehicleDTO objects if found,
     *         or HTTP status 400 (BAD_REQUEST) if the ID is invalid, Chief not found, or no vehicles found
     */
    @Operation(summary = "Retrieve vehicles by garage ID from Chief ID",
            description = "Finds vehicles in the garage associated with the chief ID. " +
                    "Returns 400 if ID is invalid, chief not found, or no vehicles found.")
    @PostMapping(value = "/list/garages/id")
    public ResponseEntity<List<VehicleDTO>> findByGarageId(
            @Parameter(description = "Identifier of garage", required = true)
            @RequestParam int id) {
        if(id <= 0) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        ChiefDTO chief = chiefService.findChiefById(id);
        if(chief == null) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
        int garageId = chief.getGarageId();
        List<VehicleDTO> vehicleDTOList = vehicleService.findByGarageId(garageId, 1);

        if(vehicleDTOList.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
        return new ResponseEntity<>(vehicleDTOList, HttpStatus.OK);
    }

    /**
     * Gets count of vehicles by given status.
     * @param status Status of the vehicles.
     * @return counts of vehicles by given status.
     */
    @Operation(summary = "Counts vehicles by given status.",
            description = "Counts vehicles by given status.")
    @CrossOrigin(origins = "*")
    @PostMapping(value = "/find/status")
    public ResponseEntity<Integer> getVehicleCountsByStatus(@RequestBody int status) {
        return new ResponseEntity<>(vehicleService.getVehiclesCountByStatus(status), HttpStatus.OK);

    }

}
