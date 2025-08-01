package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums;

/**
 * Represents the status of a vehicle in the system.
 *
 * <p>This enumeration is used to track the operational condition of vehicles,
 * such as whether they are available for use or undergoing maintenance.</p>
 */
public enum VehicleStatuses {

    /** Vehicle is operational and ready to be assigned to a task. */
    READY_FOR_SERVICE,

    /** Vehicle has a malfunction and is not suitable for service. */
    MALFUNCTION,

    /** Vehicle is damaged and requires repair. */
    DAMAGED,

    /** Vehicle is out of service and not currently in use. */
    OUT_OF_SERVICE,

    /** Vehicle is undergoing maintenance or repair. */
    UNDER_MAINTENANCE
}
