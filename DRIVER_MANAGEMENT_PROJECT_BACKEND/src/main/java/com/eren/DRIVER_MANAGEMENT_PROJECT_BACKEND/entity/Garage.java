package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.*;

import java.util.List;
/**
 * Represents a garage in the system.
 * </p>
 * This entity stores information such as the garage name and address.
 */
@Schema(description = "Garage entity storing name and address information.")
@Entity
@Table(name = "garage")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Garage {

    /**
     * Unique identifier for the garage (e.g. 1 ).
     */
    @Schema(description = "Unique identifier for the garage.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    /**
     * Name or title of the garage (e.g. Merkez Garajı).
     */
    @Schema(description = "Name or title of the garage.", example = "Merkez Garajı")
    @Column(name = "garage_name", nullable = false)
    private String garageName;

    /**
     * Physical address where the garage is located (e.g. İstanbul).
     */
    @Schema(description = "Physical address where the garage is located.", example = "İstanbul")
    @Column(name = "garage_address")
    private String garageAddress;

}
