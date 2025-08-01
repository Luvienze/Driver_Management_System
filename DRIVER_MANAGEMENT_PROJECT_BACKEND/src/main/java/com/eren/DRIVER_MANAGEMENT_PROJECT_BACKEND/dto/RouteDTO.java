package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for Route entity.
 * <p>
 * Contains information about route such as name, distance and duration.
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class RouteDTO {

    /**
     * Unique identifier for the route (e.g. 1).
     */
    @Schema(description = "Unique identifier for the route.", example = "1")
    private Integer id;

    /**
     * Name of the route (e.g. Fatih-Taksim).
     */
    @Schema(description = "Name of the route.", example = "Fatih-Taksim")
    private String routeName;

    /**
     * Distance of the route (e.g. 8).
     */
    @Schema(description = "Distance of the route.", example = "8")
    private Integer distance;

    /**
     * Duration of the route (e.g. 20).
     */
    @Schema(description = "Duration of the route.", example = "20")
    private Integer duration;
}
