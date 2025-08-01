package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for Line entity.
 * </p>
 * Contains detailed information about line.
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class LineDTO {

    /**
     * Unique identifier for the line (e.g. 1).
     */
    @Schema(description = "Unique identifier for the line.", example = "1")
    private Integer id;

    /**
     * Code number for the line (e.g. 34BZ).
     */
    @Schema(description = "Code number for the line.", example = "34BZ")
    private String lineCode;

    /**
     * Title or name for the line (e.g. Fatih-Taksim).
     */
    @Schema(description = "Title or name for the line.", example = "Fatih-Taksim")
    private String lineName;

    /**
     * Indicates whether the line is currently active (e.g. true).
     */
    @Schema(description = "Indicates whether the line is currently active.", example = "true")
    private Boolean isActive;
}
