package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Represents line in the system.
 * </p>
 * This entity stores information such as line code and line name.
 */
@Schema(description = "Line entity storing line code and line name information.")
@Entity
@Table(name = "line")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Line {

    /**
     * Unique identifier for the line (e.g. 1).
     */
    @Schema(description = "Unique identifier for the line.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    /**
     * Code number for the line (e.g. 34BZ).
     */
    @Schema(description = "Code number for the line.", example = "34BZ")
    @Column(name = "line_code", length = 6, nullable = false)
    private String lineCode;

    /**
     * Title or name for the line (e.g. Fatih-Taksim).
     */
    @Schema(description = "Title or name for the line.", example = "Fatih-Taksim")
    @Column(name = "line_name", length = 50, nullable = false)
    private String  lineName;

    /**
     * Indicates whether the line is currently active (e.g. true).
     */
    @Schema(description = "Indicates whether the line is currently active.", example = "true")
    @Column(name = "is_active", nullable = false)
    private Boolean isActive;
}
