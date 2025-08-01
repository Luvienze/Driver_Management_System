package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Represents route in the system.
 * </p>
 * Route storing information such as route name, distance and duration.
 */
@Schema(description = "Route storing information such as route name, distance and duration.")
@Entity
@Table(name = "[route]")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Route {

    /**
     * Unique identifier for the route (e.g. 1).
     */
    @Schema(description = "Unique identifier for the route.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    /**
     * Title or name of the route (e.g. Fatih-Taksim Hattı).
     */
    @Schema(description = "Title or name of the route.", example = "Fatih-Taksim Hattı")
    @Column(name = "route_name", nullable = false, length = 50)
    private String routeName;

    /**
     * Distance of the route (e.g. 8).
     */
    @Schema(description = "Distance of the route.", example = "8")
    @Column(name = "distance", nullable = false)
    private Integer distance;

    /**
     * Duration of the route (e.g. 20).
     */
    @Schema(description = "Duration of the route.", example = "20")
    @Column(name = "duration", nullable = false)
    private Integer duration;
}
