package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

import java.time.LocalDate;

/**
 * Data Transfer Object for Driver entity.
 * <p>
 * Contains detailed information about personal and chief information.
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class DriverDTO {

    /**
     * Unique identifier for the driver (e.g. 1).
     */
    @Schema(description = "Unique identifier for the driver.", example = "1")
    private int id;

    /**
     * First name of the driver (e.g. John).
     */
    @Schema(description = "First name of the driver.", example = "John")
    private String personFirstName;

    /**
     * Last name of the driver (e.g. Doe).
     */
    @Schema(description = "Last name of the driver.", example = "Doe")
    private String personLastName;

    /**
     * Phone number of the driver (e.g. 05123456789).
     */
    @Schema(description = "Phone number of the driver.", example = "05123456789")
    private String phoneNumber;

    /**
     * Registration number assigned to the driver (e.g. REG005).
     */
    @Schema(description = "Registration number assigned to the driver.", example = "REG005")
    private String registrationNumber;

    /**
     * Name of the garage where the driver is assigned (e.g. Merkez Garajı).
     */
    @Schema(description = "Name of the garage.", example = "Merkez Garajı")
    private String garage;

    /**
     * ID of the chief responsible for the driver (e.g. 1).
     */
    @Schema(description = "ID of the chief.", example = "1")
    private Integer chiefId;

    /**
     * First name of the chief (e.g. Jane).
     */
    @Schema(description = "First name of the chief.", example = "Jane")
    private String chiefFirstName;

    /**
     * Last name of the chief (e.g. Doe).
     */
    @Schema(description = "Last name of the chief.", example = "Doe")
    private String chiefLastName;

    /**
     * Cadre type represented by an integer (e.g. 2).
     */
    @Schema(description = "Cadre type represented by an integer.", example = "2")
    private int cadre;

    /**
     * Number of day-offs assigned to the driver (e.g. 4).
     */
    @Schema(description = "Number of day-offs assigned to the driver.", example = "4")
    private int dayOff;

    /**
     * Whether the driver is active or not (e.g. true).
     */
    @Schema(description = "Driver active status", example = "true")
    private Boolean isActive;
}
