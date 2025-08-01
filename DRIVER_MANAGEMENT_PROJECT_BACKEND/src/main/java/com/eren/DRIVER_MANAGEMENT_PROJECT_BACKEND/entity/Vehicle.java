package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.VehicleStatuses;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Represents vehicle in the system.
 * <p>
 * Each vehicle is associated with a garage.
 * </p>
 * Storing information such as door number, capacity, fuel tank.
 */
@Schema(description = "Represents a task assigned to a driver, vehicle, route, and line. Contains scheduling and status information.")
@Entity
@Table(name = "vehicle")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Vehicle {

    /**
     * Unique identifier for the vehicle (e.g. 1).
     */
    @Schema(description = "Unique identifier for the vehicle.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    /**
     * Door number for the vehicle (e.g. B1234).
     */
    @Schema(description = "Door number for the vehicle.", example = "B1234")
    @Column(name = "door_no", nullable = false)
    private String doorNo;

    /**
     * Capacity of the vehicle (e.g. 35).
     */
    @Schema(description = "Capacity of the vehicle.", example = "35")
    @Column(name = "capacity", nullable = false)
    private Integer capacity;

    /**
     * Fuel tank of the vehicle (e.g. 115).
     */
    @Schema(description = "Fuel tank of the vehicle.", example = "115")
    @Column(name = "fuel_tank", nullable = false)
    private Integer fuelTank;

    /**
     * Plate of the vehicle (e.g. 34 KL 5432).
     */
    @Schema(description = "Plate of the vehicle.", example = "34 KL 5432")
    @Column(name = "plate", nullable = false)
    private String plate;

    /**
     * Number of standing passengers allowed (e.g. 22).
     */
    @Schema(description = "Number of standing passengers allowed.", example = "22")
    @Column(name = "person_on_foot")
    private Integer personOnFoot;

    /**
     * Number of sitting passengers allowed (e.g. 25).
     */
    @Schema(description = "Number of sitting passengers allowed", example = "25")
    @Column(name = "person_on_sit")
    private Integer personOnSit;

    /**
     * Indicates if the vehicle is accessible for disabled passengers (e.g. true).
     */
    @Schema(description = "Indicates if the vehicle is accessible for disabled passengers", example = "true")
    @Column(name = "suitable_for_disabled", nullable = false)
    private Boolean suitableForDisabled;

    /**
     * Model year of the vehicle (e.g. 2019).
     */
    @Schema(description = "Model year of the vehicle.", example = "2019")
    @Column(name = "model_year")
    private Integer modelYear;

    /**
     * Status for the vehicle (e.g. DAMAGED, MALFUNCTION, READY_FOR_SERVICE).
     */
    @Schema(description = "Status for the vehicle.", example = "MALFUNCTION")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "status")
    private VehicleStatuses status;

    /**
     * Garage to which the vehicle belongs (e.g. Merkez Garajı).
     */
    @Schema(description = "Garage to which the vehicle belongs.", example = "Merkez Garajı")
    @OneToOne
    @JoinColumn(name = "garage_id")
    private Garage garage;
}
