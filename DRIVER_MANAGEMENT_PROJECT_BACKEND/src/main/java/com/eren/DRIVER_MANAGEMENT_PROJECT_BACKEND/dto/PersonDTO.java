package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;
import java.time.LocalDate;

/**
 * Data Transfer Object for Person entity.
 * </p>
 * Contains detailed information about person.
 */
@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class PersonDTO {

    /**
     * Unique identifier for the person (e.g. 1).
     */
    @Schema(description = "Unique identifier for the person.", example = "1")
    private Integer id;

    /**
     * First name of the person (e.g. John).
     */
    @Schema(description = "First name of the person.", example = "John")
    private String firstName;

    /**
     * Last name of the person (e.g. Doe).
     */
    @Schema(description = "Last name of the person.", example = "Doe")
    private String lastName;

    /**
     * Gender represented by an integer (e.g. 2).
     */
    @Schema(description = "Gender represented by an integer.", example = "2")
    private int gender;

    /**
     * Blood group represented by an integer (e.g. 2).
     */
    @Schema(description = "Blood group represented by an integer.", example = "2")
    private int bloodGroup;

    /**
     * Birthdate of person (e.g. 29-07-2002).
     */
    @Schema(description = "Birthdate of person.", example = "29-07-2002")
    private LocalDate dateOfBirth;

    /**
     * Phone number for the person (e.g. 05123456789)
     */
    @Schema(description = "Phone number for the person.", example = "05123456789")
    private String phone;

    /**
     * Address where the person is physically located (e.g. İstanbul).
     */
    private String address;
    @Schema(description = "Address where the person is physically located.", example = "İstanbul")
    private String imageUrl;

    /**
     * File path of person picture.
     */
    @Schema(description = "File path of person picture.")
    private String registrationNumber;

    /**
     * Start date when person is being registered to system (e.g. 16.06.2025)
     */
    @Schema(description = "Start date when person is being registered to system.", example = "16.06.2025")
    private LocalDate dateOfStart;

    /**
     * Indicates whether the person is deleted (e.g. true).
     */
    @Schema(description = "Indicated whether the person is deleted.", example = "true")
    private Boolean isDeleted;
}
