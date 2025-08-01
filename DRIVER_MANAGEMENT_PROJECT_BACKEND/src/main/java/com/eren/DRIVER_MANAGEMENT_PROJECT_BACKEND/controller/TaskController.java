package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.TaskDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Task;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Tasks;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.TaskService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.List;

@RestController
@RequestMapping(value = "/task")
@Tag(name = "Task API", description = "Endpoints are managing tasks related operations.")
public class TaskController {

    @Autowired
    public TaskService taskService;

    /**
     * Lists all tasks.
     *
     * @return a list of all TaskDTOs
     */
    @Operation(
            summary = "List all tasks",
            description = "Retrieves a list of all tasks in the system."
    )
    @PostMapping(value = "/list")
    public List<TaskDTO> listAllTasks() {
        return taskService.listAllTasks();
    }

    /**
     * Finds a task by registration number.
     *
     * @param registrationNumber the registration number of the task
     * @return the TaskDTO associated with the given registration number
     */
    @Operation(
            summary = "Find task by registration number",
            description = "Retrieves a task by its registration number."
    )
    @PostMapping(value = "/find/registrationNumber")
    public TaskDTO findTaskByRegistrationNumber(
            @Parameter(description = "Registration number of driver")
            @RequestParam("registrationNumber") String registrationNumber) {
        return taskService.findTaskByRegistrationNumber(registrationNumber);
    }

    /**
     * Finds a task by registration number (wrapped in ResponseEntity).
     *
     * @param registrationNumber the registration number of the task
     * @return ResponseEntity with TaskDTO
     */
    @Operation(
            summary = "Find task by registration number (ResponseEntity)",
            description = "Retrieves a task by its registration number, returning a ResponseEntity."
    )
    @PostMapping(value = "/list/driver")
    public ResponseEntity<TaskDTO> findListByRegistrationNumber(
            @Parameter(description = "Registration number of task")
            @RequestParam("registrationNumber") String registrationNumber) {
        return new ResponseEntity<>(taskService.findTaskByRegistrationNumber(registrationNumber), HttpStatus.OK);
    }

    /**
     * Finds a task by its ID.
     *
     * @param taskId the ID of the task
     * @return ResponseEntity with TaskDTO or BAD_REQUEST if invalid ID
     */
    @Operation(
            summary = "Find task by ID",
            description = "Retrieves a task by its task ID."
    )
    @PostMapping(value = "/find/id")
    public ResponseEntity<TaskDTO> findTaskById(
            @Parameter(description = "Identifier of task", required = true)
            @RequestParam("taskId") int taskId) {

        if(taskId <= 0){
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        TaskDTO sendTask = taskService.findTaskById(taskId);
        return new ResponseEntity<>(sendTask, HttpStatus.OK);
    }

    /**
     * Finds list of tasks assigned to driver by registration number.
     *
     * @param registrationNumber the registration number of the driver
     * @return ResponseEntity with list of TaskDTOs
     */
    @Operation(
            summary = "Find tasks assigned to driver",
            description = "Retrieves a list of tasks assigned to a driver by registration number."
    )
    @PostMapping(value = "/list/driver/assigned")
    public ResponseEntity<List<TaskDTO>> findTaskListByRegistrationNumber(
            @Parameter(description = "Registration number of driver")
            @RequestParam("registrationNumber") String registrationNumber) {
        return new ResponseEntity<>(taskService.findTaskListByRegistrationNumber(registrationNumber), HttpStatus.OK);
    }

    /**
     * Saves or updates a task.
     *
     * @param taskDTO the task data to save or update
     * @return ResponseEntity with saved or updated TaskDTO
     */
    @Operation(
            summary = "Save or update task",
            description = "Creates a new task or updates an existing one."
    )
    @PostMapping(value = "/saveOrUpdate")
    public ResponseEntity<TaskDTO> saveOrUpdateTask(
            @Parameter(description = "Data transfer object of task", required = true)
            @RequestBody TaskDTO taskDTO) {
        TaskDTO task = taskService.saveOrUpdateTask(taskDTO);
        return ResponseEntity.ok(task);
    }

    /**
     * Retrieves list of archived tasks.
     *
     * @return ResponseEntity with list of archived TaskDTOs
     */
    @Operation(
            summary = "Get archived tasks",
            description = "Retrieves a list of archived tasks."
    )
    @PostMapping(value = "/list/archived")
    public ResponseEntity<List<TaskDTO>> findArchivedTasks() {
        List<TaskDTO> tasks = taskService.getArchivedTaskList(0);
        return ResponseEntity.ok(tasks);
    }

    /**
     * Retrieves the list of tasks for today.
     *
     * @return ResponseEntity with list of today's TaskDTOs
     */
    @Operation(
            summary = "Get daily task list",
            description = "Retrieves tasks assigned for the current day."
    )
    @PostMapping(value = "/daily/tasks")
    ResponseEntity<List<TaskDTO>> getDailyTaskList(){
        LocalDate today = LocalDate.now();
        LocalDateTime start = today.atStartOfDay();
        LocalDateTime end = today.atTime(LocalTime.MAX);
        List<TaskDTO> tasks = taskService.getDailyTaskList(start, end);
        System.out.println(tasks);
        return ResponseEntity.ok(tasks);
    }

    /**
     * Gets count of tasks within a date-time range.
     *
     * @param startOfDay optional start date-time, defaults to start of today
     * @param endOfDay optional end date-time, defaults to end of today
     * @return ResponseEntity with count of tasks
     */
    @Operation(
            summary = "Get daily tasks count",
            description = "Returns the number of tasks within a given date-time range (defaults to today)."
    )
    @PostMapping(value = "/daily/count")
    public ResponseEntity<Integer> getDailyTasksCount(
            @Parameter(description = "Start date of task", required = true)
            @RequestParam(required = false) LocalDateTime startOfDay,
            @Parameter(description = "End date of task", required = true)
            @RequestParam(required = false) LocalDateTime endOfDay) {
        if (startOfDay == null) {
            startOfDay = LocalDate.now().atStartOfDay();
        }
        if (endOfDay == null) {
            endOfDay = LocalDate.now().atTime(LocalTime.from(LocalDateTime.MAX));
        }

        return new ResponseEntity<>(taskService.getDailyTaskCount(startOfDay, endOfDay), HttpStatus.OK);
    }

    /**
     * Gets count of drivers with tasks within a date-time range.
     *
     * @param startOfDay optional start date-time, defaults to now
     * @param endOfDay optional end date-time, defaults to now
     * @return ResponseEntity with count of drivers
     */
    @Operation(
            summary = "Get daily driver count",
            description = "Returns the number of drivers with tasks in a given date-time range."
    )
    @PostMapping(value = "/daily/driver")
    public ResponseEntity<Integer> getDailyDriverCount(
            @Parameter(description = "Start date of task", required = true)
            @RequestParam(required = false) LocalDateTime startOfDay,
            @Parameter(description = "End date of task", required = true)
            @RequestParam(required = false) LocalDateTime endOfDay) {
        if (startOfDay == null) {
            startOfDay = LocalDateTime.now();
        }
        if (endOfDay == null) {
            endOfDay = LocalDateTime.now();
        }
        return new ResponseEntity<>(taskService.getDailyDriverCount(startOfDay, endOfDay), HttpStatus.OK);
    }

    /**
     * Gets count of planned tasks within a date-time range and status.
     *
     * @param startOfDay optional start date-time, defaults to now
     * @param endOfDay optional end date-time, defaults to now
     * @param status optional task status, defaults to Tasks.PLANNED
     * @return ResponseEntity with count of planned tasks
     */
    @Operation(
            summary = "Get daily planned task count",
            description = "Returns the count of planned tasks for a given date-time range and status."
    )
    @PostMapping(value = "/daily/plannedTask")
    public ResponseEntity<Integer> getDailyPlannedTask(
            @Parameter(description = "Start date of task", required = true)
            @RequestParam(required = false) LocalDateTime startOfDay,
            @Parameter(description = "End date of task", required = true)
            @RequestParam(required = false) LocalDateTime endOfDay,
            @Parameter(description = "Status of task", required = true)
            @RequestParam(required = false) Tasks status) {

        if (startOfDay == null) startOfDay = LocalDateTime.now();
        if (endOfDay == null) endOfDay = LocalDateTime.now();
        if (status == null) status = Tasks.PLANNED;

        return new ResponseEntity<>(taskService.getPlannedTaskCount(startOfDay, endOfDay, status), HttpStatus.OK);
    }

}
