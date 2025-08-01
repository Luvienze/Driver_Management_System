package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for Role entity.
 * <p>
 * Contains information about role and person.
 */
@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class RoleDTO {

    /**
     * Unique identifier for the role (e.g. 1).
     */
    @Schema(description = "Unique identifier for the role.", example = "1")
    private int id;

    /**
     * Name of the role (e.g. ADMIN, CHIEF, DRIVER).
     */
    @Schema(description = "Name of the role.", example = "CHIEF")
    private int roleName;

    /**
     * Identifier for the person (e.g. 1).
     */
    @Schema(description = "Identifier for the person.", example = "1")
    private int personId;
}
