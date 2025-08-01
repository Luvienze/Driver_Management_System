package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.BloodGroups;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Genders;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.*;
import java.time.LocalDate;

/**
 * Represents person in the system.
 * </p>
 * Person entity stores detailed information belongs to chief and driver.
 */
@Schema(description = "Person entity stores detailed information belongs to chief and driver.")
@Entity
@Table(name = "person")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Person {

    /**
     * Unique identifier for the person (e.g. 1).
     */
    @Schema(description = "Unique identifier for the person.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    /**
     * Firstname of the person (e.g. John).
     */
    @Schema(description = "Firstname of the person.", example = "John")
    @Column(name = "first_name", nullable = false)
    private String firstName;

    /**
     * Lastname of the person (e.g. Doe).
     */
    @Schema(description = "Lastname of the person.", example = "Doe")
    @Column(name = "last_name", nullable = false)
    private String lastName;

    /**
     * Gender of the person (e.g. MALE, FEMALE, OTHER).
     */
    @Schema(description = "Gender of the person.", example = "MALE")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "gender", nullable = false)
    private Genders gender;

    /**
     * Blood group of the person (e.g. A_POSITIVE as A+).
     */
    @Schema(description = "Blood group of the person.", example = "A_POSITIVE (A+)")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "blood_group", nullable = false)
    private BloodGroups bloodGroup;

    /**
     * Birthdate of the person (e.g. 29.07.2002).
     */
    @Schema(description = "Birthdate of the person.")
    @Temporal(TemporalType.DATE)
    @Column(name = "date_of_birth", nullable = false)
    private LocalDate dateOfBirth;

    /**
     * Phone number of the person (e.g 051234567890).
     */
    @Schema(description = "Phone number of the person.", example = "051234567890")
    @Column(name = "phone", nullable = false, length = 10)
    private String phone;

    /**
     * Address where the person is physically located (e.g. İstanbul).
     */
    @Schema(description = "Address where the person is physically located.", example = "İstanbul")
    @Column(name = "address", nullable = false, length = 100)
    private String address;

    /**
     * File path of the person picture.
     */
    @Schema(description = " File path of the person picture.")
    @Lob
    @Column(name = "image_url")
    private String imageUrl;

    /**
     * Unique value that represents whether the person is real (e.g. REG005).
     */
    @Schema(description = " Unique value that represents whether the person is real.", example = "REG005")
    @Column(name = "registration_number", unique = true, nullable = false, length = 6)
    private String registrationNumber;

    /**
     * Start date when person is being registered to system (e.g. 16.06.2025).
     */
    @Schema(description = "Start date when person is being registered to system.", example = "16.06.2025")
    @Temporal(TemporalType.DATE)
    @Column(name = "date_of_start")
    private LocalDate dateOfStart;

    /**
     * Indicates whether the person is deleted (e.g. true).
     */
    @Schema(description = "Indicates whether the person is deleted.", example = "true")
    @Column(name = "is_deleted",nullable = false)
    private Boolean isDeleted;

}
