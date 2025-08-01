package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.TaskDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Task;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Tasks;
import org.springframework.data.repository.query.Param;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;
/**
 * Service interface for managing tasks entities.
 */
public interface TaskService {

    /**
     * Retrieves all tasks in the system.
     *
     * @return a list of all {@link TaskDTO} objects.
     */
    List<TaskDTO> listAllTasks();

    /**
     * Finds a specific task by its ID.
     *
     * @param id the ID of the task.
     * @return the {@link TaskDTO} if found, otherwise null.
     */
    TaskDTO findTaskById(int id);

    /**
     * Finds the most recent or current task assigned to a driver by their registration number.
     *
     * @param registrationNumber the driver's registration number.
     * @return the matching {@link TaskDTO}, or null if not found.
     */
    TaskDTO findTaskByRegistrationNumber(String registrationNumber);

    /**
     * Retrieves all tasks assigned to a driver based on their registration number.
     *
     * @param registrationNumber the driver's registration number.
     * @return a list of {@link TaskDTO} objects.
     */
    List<TaskDTO> findTaskListByRegistrationNumber(String registrationNumber);

    /**
     * Saves a new task or updates an existing one.
     *
     * @param taskDTO the task data to be saved or updated.
     * @return the saved or updated {@link TaskDTO}.
     */
    TaskDTO saveOrUpdateTask(TaskDTO taskDTO);

    /**
     * Retrieves all tasks assigned within a specific time range (usually a day).
     *
     * @param startOfDay start of the time interval.
     * @param endOfDay end of the time interval.
     * @return a list of {@link TaskDTO} objects scheduled in that interval.
     */
    List<TaskDTO> getDailyTaskList(LocalDateTime startOfDay, LocalDateTime endOfDay);

    /**
     * Retrieves tasks with a specific status, typically representing archived or completed tasks.
     *
     * @param status the numeric representation of the task status (e.g., archived).
     * @return a list of {@link TaskDTO} objects.
     */
    List<TaskDTO> getArchivedTaskList(int status);

    /**
     * Counts the number of tasks scheduled within a time range.
     *
     * @param startOfDay start of the time interval.
     * @param endOfDay end of the time interval.
     * @return the count of tasks.
     */
    int getDailyTaskCount(LocalDateTime startOfDay, LocalDateTime endOfDay);

    /**
     * Counts the number of distinct drivers who have tasks assigned within a time range.
     *
     * @param startOfDay start of the time interval.
     * @param endOfDay end of the time interval.
     * @return the number of unique drivers working on that day.
     */
    int getDailyDriverCount(LocalDateTime startOfDay, LocalDateTime endOfDay);

    /**
     * Counts the number of tasks with a specific status within a given time range.
     *
     * @param startOfDay start of the time interval.
     * @param endOfDay end of the time interval.
     * @param status the status of tasks to filter (e.g., PLANNED, COMPLETED).
     * @return the count of planned or filtered tasks.
     */
    int getPlannedTaskCount(LocalDateTime startOfDay, LocalDateTime endOfDay, Tasks status);
}
