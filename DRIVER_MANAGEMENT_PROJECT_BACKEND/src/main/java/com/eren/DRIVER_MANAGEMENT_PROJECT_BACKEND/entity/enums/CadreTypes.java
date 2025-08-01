package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums;

/**
 * Represents various cadre types for transport-related tasks.
 *
 * <p>This enumeration defines the categories of transportation services,
 * such as public transport, intercity transport, corporate shuttle services,
 * and other specialized operations.</p>
 */
public enum CadreTypes {

    /** Urban public transportation services (e.g. city buses). */
    URBAN_PUBLIC_TRANSPORT,
    /** Intercity transportation services (e.g. between cities). */
    INTERCITY_TRANSPORT,
    /** Shuttle services for corporate or institutional personnel. */
    CORPORATE_SHUTTLE_SERVICE,
    /** Tourism and charter-based transportation services. */
    TOURISM_AND_CHARTER_SERVICE,
    /** Transportation services during night shifts or overnight operations. */
    NIGHT_SHIFT_OPERATION,
    /** Staff assigned as backup or to fill in for absent personnel. */
    BACKUP_AND_RELIEF_STAFF,
    /** Staff used for training, testing, or performance evaluation. */
    TRAINING_AND_EVALUATION,
    /** VIP or private transport assignments, typically non-public use. */
    VIP_AND_PRIVATE_TRANSPORT,
    /** Transportation service specifically for municipal employees. */
    MUNICIPAL_STAFF_TRANSPORT,
    /** Staff working on airport transfer and logistics services. */
    AIRPORT_LOGISTIC_TRANSFER
}
