package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;
import java.time.LocalDate;
import java.time.LocalDateTime;

/**
 * Data Transfer Object for Task entity.
 * <p>
 * Contains detailed information about task, represent relations such as driver, vehicle, route and line as integer type.
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class TaskDTO {

    /**
     * Unique identifier for the task (e.g. 1).
     */
    @Schema(description = "Unique identifier for the task.", example = "1")
    private Integer id;

    /**
     * The driver who is assigned to work in task (e.g. REG005).
     */
    @Schema(description = "The driver who is assigned to work in task.", example = "REG005")
    private String registrationNumber;

    /**
     * The vehicle that is driven by driver for the task (e.g. BZ123).
     */
    @Schema(description = "The vehicle that is driven by driver for the task.", example = "BZ123")
    private String doorNo;

    /**
     * Identifier for the route (e.g. 1).
     */
    @Schema(description = "Identifier for the route.", example = "1")
    private Integer routeId;

    /**
     * The route assigned for the task (e.g. Fatih-Taksim).
     */
    @Schema(description = "The route assigned for the task.", example = "Fatih-Taksim")
    private String routeName;

    /**
     * The line code for the task (e.g. 34BZ).
     */
    @Schema(description = "The line code for the task.", example = "34BZ")
    private String lineCode;

    /**
     * The direction for the task, represented by an integer (e.g. DEPARTURE, ARRIVAL).
     */
    @Schema(description = "The direction for the task.", example = "DEPARTURE")
    private int direction;

    /**
     * The start time for the task (e.g. 2025-07-12T08:00:00).
     */
    @Schema(description = "Start time for the task.", example = "2025-07-12T08:00:00")
    private LocalDateTime dateOfStart;

    /**
     * The end time for the task (e.g. 2025-07-12T10:00:00).
     */
    @Schema(description = "End time for the task.", example = "2025-07-12T10:00:00")
    private LocalDateTime dateOfEnd;

    /**
     * Passenger count expected or recorded for the task (e.g. 100).
     */
    @Schema(description = "TPassenger count expected or recorded for the task.", example = "100")
    private Integer passengerCount;

    /**
     * Scheduled (orer) start time (e.g. 2025-07-12T07:50:00).
     */
    @Schema(description = "Scheduled (orer) start time.", example = "2025-07-12T07:50:00")
    private LocalDateTime orerStart;

    /**
     * Scheduled (orer) end time (e.g. 2025-07-12T10:10:00).
     */
    @Schema(description = "Scheduled (orer) end time.", example = "2025-07-12T10:10:00")
    private LocalDateTime orerEnd;

    /**
     * Chief-modified start time, if applicable (e.g. 2025-07-12T08:05:00).
     */
    @Schema(description = "Chief-modified start time, if applicable.", example = "2025-07-12T08:05:00")
    private LocalDateTime chiefStart;

    /**
     * Chief-modified end time, if applicable (e.g. 2025-07-12T08:15:00).
     */
    @Schema(description = "Chief-modified end time, if applicable.", example = "2025-07-12T10:15:00")
    private LocalDateTime chiefEnd;

    /**
     * The status for the task, represented by an integer (e.g. PLANNED, COMPLETED).
     */
    @Schema(description = "The status for the task.", example = "PLANNED")
    private int status;
}
