package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Directions;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Tasks;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.*;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.List;

/**
 * Represents task in the system.
 * <p>
 * Each task is associated with a driver, vehicle, route and line.
 * <p>
 * Storing information such as start and end date of task, status.
 */
@Schema(description = "Each task is associated with a driver, vehicle, route and line.")
@Entity
@Table(name = "task")
@Data
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class Task {

    /**
     * Unique identifier for the task (e.g. 1).
     */
    @Schema(description = "Unique identifier for the task.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    /**
     * The driver who is assigned to work in task (e.g. 1).
     */
    @Schema(description = "The driver who is assigned to work in task.", example = "1")
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    @JoinColumn(name = "driver_id", referencedColumnName = "id")
    private Driver driverId;

    /**
     * The vehicle that is driven by driver for the task (e.g. 1).
     */
    @Schema(description = "The vehicle that is driven by driver for the task.", example = "1")
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    @JoinColumn(name = "vehicle_id", referencedColumnName = "id")
    private Vehicle vehicleId;

    /**
     * The route assigned for the task (e.g. 1).
     */
    @Schema(description = "The route assigned for the task.", example = "1")
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    @JoinColumn(name = "route_id")
    private Route routeId;

    /**
     * The line code for the task (e.g. 34BZ).
     */
    @Schema(description = "The line code for the task.", example = "34BZ")
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    @JoinColumn(name = "line_code")
    private Line lineCode;

    /**
     * The direction for the task (e.g. DEPARTURE, ARRIVAL).
     */
    @Schema(description = "The direction for the task.", example = "DEPARTURE")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "direction", nullable = false)
    private Directions direction;

    /**
     * The start time for the task (e.g. 2025-07-12T08:00:00).
     */
    @Schema(description = "Start time for the task.", example = "2025-07-12T08:00:00")
    @Column(name = "date_of_start", nullable = false)
    private LocalDateTime dateOfStart;

    /**
     * The end time for the task (e.g. 2025-07-12T10:00:00).
     */
    @Schema(description = "End time for the task.", example = "2025-07-12T10:00:00")
    @Column(name = "date_of_end", nullable = false)
    private LocalDateTime dateOfEnd;

    /**
     * Passenger count expected or recorded for the task (e.g. 100).
     */
    @Schema(description = "TPassenger count expected or recorded for the task.", example = "100")
    @Column(name = "passenger_count", nullable = false)
    private Integer passengerCount;

    /**
     * Scheduled (orer) start time (e.g. 2025-07-12T07:50:00).
     */
    @Schema(description = "Scheduled (orer) start time.", example = "2025-07-12T07:50:00")
    @Column(name = "orer_start")
    private LocalDateTime orerStart;

    /**
     * Scheduled (orer) end time (e.g. 2025-07-12T10:10:00).
     */
    @Schema(description = "Scheduled (orer) end time.", example = "2025-07-12T10:10:00")
    @Column(name = "orer_end")
    private LocalDateTime orerEnd;

    /**
     * Chief-modified start time, if applicable (e.g. 2025-07-12T08:05:00).
     */
    @Schema(description = "Chief-modified start time, if applicable.", example = "2025-07-12T08:05:00")
    @Column(name = "chief_start")
    private LocalDateTime chiefStart;

    /**
     * Chief-modified end time, if applicable (e.g. 2025-07-12T08:15:00).
     */
    @Schema(description = "Chief-modified end time, if applicable.", example = "2025-07-12T10:15:00")
    @Column(name = "chief_end")
    private LocalDateTime chiefEnd;

    /**
     * The status for the task (e.g. PLANNED, COMPLETED).
     */
    @Schema(description = "The status for the task.", example = "PLANNED")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "status", nullable = false)
    private Tasks status;

}
