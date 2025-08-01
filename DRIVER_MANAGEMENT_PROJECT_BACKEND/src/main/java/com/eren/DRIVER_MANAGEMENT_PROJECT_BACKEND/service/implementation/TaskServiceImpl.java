package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.*;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.TaskDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.*;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Directions;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Tasks;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.TaskService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Service class for managing Task entities.
 */
@Service
public class TaskServiceImpl implements TaskService {

    @Autowired
    private TaskRepository taskRepository;
    @Autowired
    private DriverRepository driverRepository;
    @Autowired
    private PersonRepository personRepository;
    @Autowired
    private VehicleRepository vehicleDAO;
    @Autowired
    private RouteRepository routeRepository;
    @Autowired
    private LineRepository lineRepository;

    final LocalDate today = LocalDate.now();
    final LocalDateTime startOfDay = today.atStartOfDay();
    final LocalDateTime endOfDay = today.atTime(LocalTime.MAX);

    /**
     * Retrieves all tasks as a list of TaskDTOs.
     *
     * @return List<TaskDTO> list of all tasks.
     */
    @Override
    public List<TaskDTO> listAllTasks() {
        return taskRepository.listTaskList()
                .stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    /**
     * Finds a task by its unique ID.
     *
     * @param id The unique identifier of the task.
     * @return TaskDTO representing the found task, or null if not found.
     */
    @Override
    public TaskDTO findTaskById(int id) {
        Task task = taskRepository.findTaskById(id);
        if(task == null) return null;

        return convertToDTO(task);
    }

    /**
     * Finds a task associated with a driver's registration number for today.
     *
     * @param registrationNumber The driver's registration number.
     * @return TaskDTO for the task found or null if no task or null registrationNumber.
     */
    @Override
    public TaskDTO findTaskByRegistrationNumber(String registrationNumber) {

        if(registrationNumber == null){
            return null;
        }
        Task task = taskRepository.findTaskByRegistrationNumber(registrationNumber,startOfDay,endOfDay);

        if(task == null){
            return null;
        }
        return convertToDTO(task);
    }

    /**
     * Retrieves a list of tasks for a given driver's registration number for today.
     *
     * @param registrationNumber The driver's registration number.
     * @return List<TaskDTO> of tasks or null if no tasks found or invalid registrationNumber.
     */
    @Override
    public List<TaskDTO> findTaskListByRegistrationNumber(String registrationNumber) {

        if(registrationNumber == null){
            return null;
        }

        if(personRepository.findPersonByRegistrationNumber(registrationNumber) == null){
            return null;
        }
        List<Task> taskList = taskRepository.findTaskListByRegistrationNumber(registrationNumber,startOfDay,endOfDay);

        if(taskList.isEmpty()){
            return null;
        }

        return converToDTOList(taskList);
    }

    /**
     * Saves a new Task or updates an existing Task based on the provided TaskDTO.
     *
     * @param taskDTO TaskDTO containing task details.
     * @return TaskDTO of the saved or updated task.
     * @throws RuntimeException if trying to update a task that does not exist.
     */
    @Override
    public TaskDTO saveOrUpdateTask(TaskDTO taskDTO) {

        Task task;
        Integer id = taskDTO.getId();
        Driver driver = driverRepository.findDriverByRegistrationNumber(taskDTO.getRegistrationNumber());
        Vehicle vehicle = vehicleDAO.findVehicleByDoorNo(taskDTO.getDoorNo());
        Route route = routeRepository.findRouteByRouteName(taskDTO.getRouteName());
        Line line = lineRepository.findLineByLineCode(taskDTO.getLineCode());
        if (id != null && id > 0) {
            task = taskRepository.findById(id).orElseThrow(() -> new RuntimeException("Task with id " + id + " not found"));

            task.setDriverId(driver);
            task.setVehicleId(vehicle);
            task.setRouteId(route);
            task.setLineCode(line);
            task.setDirection(Directions.values()[taskDTO.getDirection()]);
            task.setDateOfStart(taskDTO.getDateOfStart());
            task.setDateOfEnd(taskDTO.getDateOfEnd());
            task.setPassengerCount(taskDTO.getPassengerCount());
            task.setOrerStart(taskDTO.getOrerStart());
            task.setOrerEnd(taskDTO.getOrerEnd());
            task.setChiefStart(taskDTO.getChiefStart());
            task.setChiefEnd(taskDTO.getChiefEnd());
            task.setStatus(Tasks.values()[taskDTO.getStatus()]);

        } else {
            task = new Task();

            task.setDriverId(driver);
            task.setVehicleId(vehicle);
            task.setRouteId(route);
            task.setLineCode(line);
            task.setDirection(Directions.values()[taskDTO.getDirection()]);
            task.setDateOfStart(taskDTO.getDateOfStart());
            task.setDateOfEnd(taskDTO.getDateOfEnd());
            task.setPassengerCount(taskDTO.getPassengerCount());
            task.setOrerStart(taskDTO.getOrerStart());
            task.setOrerEnd(taskDTO.getOrerEnd());
            task.setChiefStart(taskDTO.getChiefStart());
            task.setChiefEnd(taskDTO.getChiefEnd());
            task.setStatus(Tasks.values()[taskDTO.getStatus()]);
        }

        Task savedTask = taskRepository.save(task);
        return convertToDTO(savedTask);
    }

    /**
     * Retrieves the list of tasks scheduled between startOfDay and endOfDay.
     *
     * @param startOfDay Start datetime boundary (inclusive).
     * @param endOfDay End datetime boundary (inclusive).
     * @return List of TaskDTO within the specified date-time range.
     */
    @Override
    public List<TaskDTO> getDailyTaskList(LocalDateTime startOfDay, LocalDateTime endOfDay) {
        return taskRepository.getDailyTaskList(startOfDay,endOfDay)
                .stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    /**
     * Retrieves archived tasks filtered by a specific status.
     *
     * @param status Integer representing the ordinal of Tasks enum for filtering.
     * @return List of archived TaskDTOs with the specified status.
     */
    @Override
    public List<TaskDTO> getArchivedTaskList(int status) {
        Tasks task = Tasks.values()[status];
        return taskRepository.getArchivedTaskList(task)
                .stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    /**
     * Counts the number of tasks scheduled between startOfDay and endOfDay.
     *
     * @param startOfDay Start datetime boundary.
     * @param endOfDay End datetime boundary.
     * @return Number of tasks in the specified date range.
     */
    @Override
    public int getDailyTaskCount(LocalDateTime startOfDay, LocalDateTime endOfDay) {
        return taskRepository.getDailyTaskCount(startOfDay, endOfDay);
    }

    /**
     * Counts the number of distinct drivers assigned tasks between startOfDay and endOfDay.
     *
     * @param startOfDay Start datetime boundary.
     * @param endOfDay End datetime boundary.
     * @return Number of distinct drivers in the specified date range.
     */
    @Override
    public int getDailyDriverCount(LocalDateTime startOfDay, LocalDateTime endOfDay) {
        return taskRepository.getDailyDriverCount(startOfDay, endOfDay);
    }

    /**
     * Counts the number of planned tasks within the specified date range and status.
     *
     * @param startOfDay Start datetime boundary.
     * @param endOfDay End datetime boundary.
     * @param status Task status enum to filter by.
     * @return Number of planned tasks matching criteria, or 0 if none found.
     */
    @Override
    public int getPlannedTaskCount(LocalDateTime startOfDay, LocalDateTime endOfDay, Tasks status) {
        Integer result = taskRepository.getPlannedTaskCount(startOfDay, endOfDay, status);
        return result != null ? result : 0;
    }

    /**
     * Converts a Task entity to a TaskDTO.
     *
     * @param task The Task entity.
     * @return TaskDTO representation of the task.
     */
    private TaskDTO convertToDTO(Task task) {

        Driver d = task.getDriverId();
        Person p = d.getPersonId();
        Vehicle v = task.getVehicleId();
        Line l = task.getLineCode();
        Route r = task.getRouteId();
        TaskDTO dto = new TaskDTO();

        dto.setId(task.getId());
        dto.setRegistrationNumber(p.getRegistrationNumber());
        dto.setDoorNo(v.getDoorNo());
        dto.setRouteName(r.getRouteName());
        dto.setRouteId(task.getRouteId().getId());
        dto.setLineCode(l.getLineCode());
        dto.setDirection(task.getDirection().ordinal());
        dto.setDateOfStart(task.getDateOfStart());
        dto.setDateOfEnd(task.getDateOfEnd());
        dto.setPassengerCount(task.getPassengerCount());
        dto.setOrerStart(task.getOrerStart());
        dto.setOrerEnd(task.getOrerEnd());
        dto.setChiefStart(task.getChiefStart());
        dto.setChiefEnd(task.getChiefEnd());
        dto.setStatus(task.getStatus().ordinal());
        return dto;
    }

    private List<TaskDTO> converToDTOList(List<Task> taskList) {
        return taskList.stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

}
