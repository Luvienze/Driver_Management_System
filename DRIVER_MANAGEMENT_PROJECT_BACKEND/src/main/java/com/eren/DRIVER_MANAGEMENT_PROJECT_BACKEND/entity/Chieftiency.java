package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;
import io.swagger.v3.oas.annotations.media.Schema;

/**
 * Represents chieftaincy in the system.
 * <p>
 * Each chieftaincy is associated with a garage.
 */
@Schema(description = "Represents chieftaincy that is associated with a garage.")
@Entity
@Table(name = "chieftiency")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Chieftiency {

    /**
     * Unique identifier for the chieftaincy (e.g. 1).
     */
    @Schema(description = "Unique identifier for the chieftaincy.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    /**
     * Name or title of the chieftaincy (e.g. Ulaşım Şefliği).
     */
    @Schema(description = "Name or title of the chieftaincy.", example = "Ulaşım Şefliği")
    @Column(name = "chieftiency_name", nullable = false)
    private String chieftiencyName;

    /**
     * Garage where the chieftaincy is currently assigned (e.g .1).
     */
    @Schema(description = "Garage where the chieftaincy is currently assigned.", example = "1")
    @OneToOne(cascade = {CascadeType.DETACH,
            CascadeType.MERGE,
            CascadeType.REFRESH,
            CascadeType.PERSIST})
    @JoinColumn(name = "garage_id", referencedColumnName = "id")
    private Garage garageId;
}
