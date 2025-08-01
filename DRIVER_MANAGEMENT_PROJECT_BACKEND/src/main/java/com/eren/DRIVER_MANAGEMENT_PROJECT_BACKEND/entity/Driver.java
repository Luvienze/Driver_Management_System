package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.CadreTypes;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Days;
import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonIdentityInfo;
import com.fasterxml.jackson.annotation.ObjectIdGenerators;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.*;
import org.springframework.context.annotation.Lazy;

import java.util.ArrayList;
import java.util.List;

/**
 * Represents a driver in the system.
 * <p>
 * Each driver is associated with a person, a garage and a chief.
 */
@Schema(description = "Represents a driver who is associated with a person, a garage and a chief.")
@Entity
@Table(name = "driver")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Driver  {

    /**
     * Unique identifier for the driver (e.g. 1).
     */
    @Schema(description = "Unique identifier for the driver", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    /**
     * Personal information of the driver, linked to the person entity (e.g. 1).
     */
    @Schema(description = "Personal information of the driver, linked to the person entity.", example = "1")
    @ManyToOne
    @JoinColumn(name = "person_id", referencedColumnName = "id", nullable = false)
    private Person personId;

    /**
     * Garage where the driver is currently assigned (e.g. 1).
     */
    @Schema(description = "Garage where the driver is currently assigned.", example = "1")
    @ManyToOne(optional = false)
    @JoinColumn(name = "garage_id", referencedColumnName = "id", nullable = false)
    private Garage garageId;

    /**
     * Chief who responsible for assigning tasks to the driver (e.g. 1).
     */
    @Schema(description = "Chief who responsible for assigning tasks to the driver.", example = "1")
    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "chief_id", referencedColumnName = "id", nullable = false)
    @JsonBackReference
    private Chief chiefId;

    /**
     * Employment status of the driver within the organization (e.g. 1).
     */
    @Schema(description = "Employment status of the driver within the organization.", example = "1")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "cadre", nullable = false)
    private CadreTypes cadre;

    /**
     * Weekly day off assigned to the driver (e.g. 2, 3).
     */
    @Schema(description = "Weekly day off assigned to the driver.", example = "2")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "day_off", nullable = false)
    private Days dayOff;

    /**
     * Indicates whether the driver is currently active (e.g. true).
     */
    @Schema(description = "Indicates whether the driver is currently active.", example = "true")
    @Column(name = "is_active", nullable = false)
    private Boolean isActive;

}
