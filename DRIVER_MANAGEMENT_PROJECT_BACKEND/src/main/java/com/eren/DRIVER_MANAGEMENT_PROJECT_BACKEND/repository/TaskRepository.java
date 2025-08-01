package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Task;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Tasks;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import java.time.LocalDateTime;
import java.util.List;

/**
 * Repository interface for Task entity.
 * </p>
 * Provides methods for performing database operations related to tasks.
 */
@Repository
public interface TaskRepository extends JpaRepository<Task, Integer> {

    /**
     * Retrieves a list of tasks with associated entities.
     *
     * @return Retrieves a list of tasks with associated entities, or null if not found.
     */
    @Query("""
        SELECT t FROM Task t
        LEFT JOIN FETCH t.driverId d
        LEFT JOIN FETCH t.vehicleId v
        LEFT JOIN FETCH t.routeId r
        LEFT JOIN FETCH t.lineCode l
    """)
    List<Task> listTaskList();

    /**
     * Finds task with associated entities by task's id.
     *
     * @param id ID of the task.
     * @return Matching task entity, or null if not found.
     */
    @Query("""
        SELECT t FROM Task t
        LEFT JOIN FETCH t.driverId d
        LEFT JOIN FETCH t.vehicleId v
        LEFT JOIN FETCH t.routeId r
        LEFT JOIN FETCH t.lineCode l
        LEFT JOIN FETCH d.personId p
        WHERE t.id = :id
    """)
    Task findTaskById(@Param("id") int id);

    /**
     * Find task with associated entities by registration number, start and end day.
     *
     * @param registrationNumber Registration number of person.
     * @param startOfDay DateOfStart of task.
     * @param endOfDay DateOfEnd of task.
     * @return An assigned task to driver by driver's registration number and restricted dates.
     */
    @Query("""
        SELECT t FROM Task t
        LEFT JOIN FETCH t.driverId d
        LEFT JOIN FETCH t.vehicleId v
        LEFT JOIN FETCH t.routeId r
        LEFT JOIN FETCH t.lineCode l
        LEFT JOIN FETCH d.personId p
        WHERE p.registrationNumber = :registrationNumber
        AND t.dateOfStart BETWEEN :startOfDay AND :endOfDay
    """)
    Task findTaskByRegistrationNumber(String registrationNumber,
                                      @Param("startOfDay") LocalDateTime startOfDay,
                                      @Param("endOfDay") LocalDateTime endOfDay);

    /**
     * Retrieves the list of tasks assigned to a driver identified by the given registration number,
     * filtered by the tasks' start date falling between the specified start and end date/time.
     *
     * <p>
     * This query fetches associated entities to avoid lazy loading issues:
     * - Driver entity
     * - Vehicle entity
     * - Route entity
     * - LineCode entity
     * - Person entity related to the driver
     * </p>
     *
     * @param registrationNumber The registration number of the driver whose tasks are being queried.
     * @param startOfDay The start of the date/time range for filtering tasks (inclusive).
     * @param endOfDay The end of the date/time range for filtering tasks (inclusive).
     * @return A list of {@link Task} entities that match the given driver's registration number
     *         and whose start date is within the specified range.
     */
    @Query("""
        SELECT t FROM Task t
        LEFT JOIN FETCH t.driverId d
        LEFT JOIN FETCH t.vehicleId v
        LEFT JOIN FETCH t.routeId r
        LEFT JOIN FETCH t.lineCode l
        LEFT JOIN FETCH d.personId p
        WHERE p.registrationNumber = :registrationNumber
        AND t.dateOfStart BETWEEN :startOfDay AND :endOfDay
    """)
    List<Task> findTaskListByRegistrationNumber(String registrationNumber,
                                      @Param("startOfDay") LocalDateTime startOfDay,
                                      @Param("endOfDay") LocalDateTime endOfDay);

    /**
     * Retrieves a list of tasks filtered by their status.
     *
     * <p>
     * The query fetches related entities eagerly to avoid lazy loading issues:
     * - Driver entity
     * - Vehicle entity
     * - Route entity
     * - LineCode entity
     * - Person entity related to the driver
     * </p>
     *
     * @param status The status of the tasks to retrieve (e.g., COMPLETED, CANCELLED).
     * @return A list of {@link Task} entities matching the specified status.
     */
    @Query("""
        SELECT t FROM Task t
        LEFT JOIN FETCH t.driverId d
        LEFT JOIN FETCH t.vehicleId v
        LEFT JOIN FETCH t.routeId r
        LEFT JOIN FETCH t.lineCode l
        LEFT JOIN FETCH d.personId p
        WHERE t.status = :status
    """)
    List<Task> getArchivedTaskList(@Param("status") Tasks status);

    /**
     * Retrieves a list of tasks that start within the specified date and time range.
     *
     * <p>
     * The query eagerly fetches associated entities to prevent lazy loading issues:
     * - Driver entity
     * - Vehicle entity
     * - Route entity
     * - LineCode entity
     * - Person entity related to the driver
     * </p>
     *
     * @param startOfDay The start of the date/time range (inclusive).
     * @param endOfDay The end of the date/time range (inclusive).
     * @return A list of {@link Task} entities whose start date falls within the specified range.
     */
    @Query("""
        SELECT t FROM Task t
        LEFT JOIN FETCH t.driverId d
        LEFT JOIN FETCH t.vehicleId v
        LEFT JOIN FETCH t.routeId r
        LEFT JOIN FETCH t.lineCode l
        LEFT JOIN FETCH d.personId p
        WHERE t.dateOfStart >= :startOfDay AND t.dateOfStart <= :endOfDay
    """)
    List<Task> getDailyTaskList(@Param("startOfDay") LocalDateTime startOfDay,
                                @Param("endOfDay") LocalDateTime endOfDay);

    /**
     * Retrieves the count of distinct tasks that start within the specified date and time range.
     *
     * @param startOfDay The start of the date/time range (inclusive).
     * @param endOfDay The end of the date/time range (exclusive).
     * @return The number of distinct {@link Task} entities whose start date falls within the specified range.
     */
    @Query("""
        SELECT COUNT(distinct t) FROM Task t
        WHERE t.dateOfStart >= :startOfDay AND t.dateOfStart < :endOfDay
    """)
    int getDailyTaskCount(@Param("startOfDay") LocalDateTime startOfDay,
                          @Param("endOfDay") LocalDateTime endOfDay);

    /**
     * Retrieves the count of distinct drivers assigned to tasks that start within the specified date and time range.
     *
     * @param startOfDay The start of the date/time range (inclusive).
     * @param endOfDay The end of the date/time range (exclusive).
     * @return The number of distinct drivers who have tasks starting within the specified range.
     */
    @Query("""
        SELECT COUNT (distinct t.driverId) FROM Task t
        WHERE t.dateOfStart >= :startOfDay AND t.dateOfStart < :endOfDay
    """)
    int getDailyDriverCount(@Param("startOfDay") LocalDateTime startOfDay,
                            @Param("endOfDay") LocalDateTime endOfDay);

    /**
     * Retrieves the count of distinct tasks with a specified status that start within the given date and time range.
     *
     * @param startOfDay The start of the date/time range (inclusive).
     * @param endOfDay The end of the date/time range (inclusive).
     * @param status The status of the tasks to filter by.
     * @return The number of distinct tasks matching the criteria.
     */
    @Query("""
        SELECT COUNT(distinct t) FROM Task t
        WHERE t.dateOfStart >= :startOfDay AND t.dateOfStart <= :endOfDay AND t.status = :status
    """)
    int getPlannedTaskCount(@Param("startOfDay") LocalDateTime startOfDay,
                            @Param("endOfDay") LocalDateTime endOfDay,
                            @Param("status") Tasks status);

}
