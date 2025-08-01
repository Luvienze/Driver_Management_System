package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums;

/**
 * Represents the status of a task within the system.
 *
 * <p>This enum defines various stages that a task can be in during its lifecycle,
 * such as planned, in progress, completed, or cancelled.</p>
 */
public enum Tasks {

    /** The task has been completed successfully. */
    COMPLETED,

    /** The task is currently in progress. */
    IN_PROGRESS,

    /** The task has been cancelled and will not be completed. */
    CANCELLED,

    /** The task is scheduled but has not started yet. */
    PLANNED
}

