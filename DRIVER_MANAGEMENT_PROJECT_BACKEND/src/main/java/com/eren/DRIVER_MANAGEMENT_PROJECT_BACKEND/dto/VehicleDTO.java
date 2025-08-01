package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for Vehicle entity.
 * <p>
 * Contains information about vehicle.
 */
@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class VehicleDTO {

    /**
     * Unique identifier for the vehicle (e.g. 1).
     */
    @Schema(description = "Unique identifier for the vehicle.", example = "1")
    private Integer id;

    /**
     * Door number for the vehicle (e.g. B1234).
     */
    @Schema(description = "Door number for the vehicle.", example = "B1234")
    private String doorNo;

    /**
     * Capacity of the vehicle (e.g. 35).
     */
    @Schema(description = "Capacity of the vehicle.", example = "35")
    private Integer capacity;

    /**
     * Fuel tank of the vehicle (e.g. 115).
     */
    @Schema(description = "Fuel tank of the vehicle.", example = "115")
    private Integer fuelTank;

    /**
     * Plate of the vehicle (e.g. 34 KL 5432).
     */
    @Schema(description = "Plate of the vehicle.", example = "34 KL 5432")
    private String plate;

    /**
     * Number of standing passengers allowed (e.g. 22).
     */
    @Schema(description = "Number of standing passengers allowed.", example = "22")
    private Integer personOnFoot;

    /**
     * Number of sitting passengers allowed (e.g. 25).
     */
    @Schema(description = "Number of sitting passengers allowed.", example = "25")
    private Integer personOnSit;

    /**
     * Indicates if the vehicle is accessible for disabled passengers (e.g. true).
     */
    @Schema(description = "Indicates if the vehicle is accessible for disabled passengers.", example = "true")
    private Boolean suitableForDisabled;

    /**
     * Model year of the vehicle (e.g. 2019).
     */
    @Schema(description = "Model year of the vehicle.", example = "2019")
    private Integer modelYear;

    /**
     * Identifier for the garage (e.g. 1).
     */
    @Schema(description = "Identifier for the garage.", example = "1")
    private Integer garageId;

    /**
     * Garage name to which the vehicle belongs (e.g. Merkez Garajı).
     */
    @Schema(description = "Garage nem to which the vehicle belongs.", example = "Merkez Garajı")
    private String garageName;

    /**
     * Status for the vehicle (e.g. DAMAGED, MALFUNCTION, READY_FOR_SERVICE).
     */
    @Schema(description = "Status for the vehicle.", example = "MALFUNCTION")
    private int status;
}
