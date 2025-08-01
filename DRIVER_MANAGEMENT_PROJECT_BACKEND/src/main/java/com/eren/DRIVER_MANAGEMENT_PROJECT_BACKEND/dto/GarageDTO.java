package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for Garage entity.
 * </p>
 * Contains detailed information about garage.
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class GarageDTO {

    /**
     * Unique identifier for the  garage (e.g. 1).
     */
    @Schema(description = "Unique identifier for the garage.", example = "1")
    private Integer id;

    /**
     * Title or name for the garage (e.g. Merkez  Garajı).
     */
    @Schema(description = "Title or name for the garage.", example = "Merkez Garajı")
    private String garageName;

    /**
     * Address where the garage is physically located. (e.g. İstanbul)
     */
    @Schema(description = "Address where the garage is physically located.", example = "İstanbul")
    private String garageAddress;
}
