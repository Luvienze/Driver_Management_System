package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import jakarta.persistence.*;
import lombok.*;
import io.swagger.v3.oas.annotations.media.Schema;

/**
 * Represents a chief in the system.
 * </p>
 * Each chief is associated with a person and a garage.
 */
@Schema(description = "Represents a chief who is associated with a person and a garage.")
@Entity
@Table(name = "chief")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Chief{

    /**
     * Unique identifier for the chief (e.g. 1).
     */
    @Schema(description = "Unique identifier for the chief.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    /**
     * Personal information of the chief, linked to the Person (e.g. 1).
     */
    @Schema(description = "Personal information of the chief, linked to the Person.", example = "1")
    @ManyToOne
    @JoinColumn(name = "personId", referencedColumnName = "id")
    private Person personId;

    /**
     * Garage where the chief is currently assigned (e.g. 1).
     */
    @Schema(description = "Garage where the chief is currently assigned.", example = "1")
    @ManyToOne(optional = false)
    @JoinColumn(name = "garage_id", referencedColumnName = "id", nullable = false)
    private Garage garageId;

    /**
     * Indicates whether the driver is currently active (e.g. true).
     */
    @Schema(description = "Indicates whether the driver is currently active.", example = "true")
    @Column(name = "is_active", nullable = false)
    private Boolean isActive;

}
