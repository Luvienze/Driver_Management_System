package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.VehicleRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.VehicleDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.*;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.VehicleStatuses;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.VehicleService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class VehicleServiceImpl implements VehicleService {

    @Autowired
    private VehicleRepository vehicleDAO;

    /**
     * Retrieves a vehicle by its door number.
     *
     * @param doorNo the door number of the vehicle.
     * @return a {@link VehicleDTO} representing the vehicle, or {@code null} if doorNo is null,
     * empty, or no vehicle is found.
     */
    @Override
    public VehicleDTO getVehicleByDoorNo(String doorNo) {
        if(doorNo == null || doorNo.isEmpty()){
            return null;
        }
        Vehicle vehicle = vehicleDAO.findVehicleByDoorNo(doorNo);
        return convertToDTO(vehicle);
    }

    /**
     * Retrieves all vehicles.
     *
     * @return a list of {@link VehicleDTO} representing all vehicles.
     */
    @Override
    public List<VehicleDTO> getAllVehicles() {
        return vehicleDAO.findAll().stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    /**
     * Finds vehicles by garage ID and status.
     *
     * @param garageId the ID of the garage.
     * @param status the integer index corresponding to the vehicle status.
     * @return a list of {@link VehicleDTO} of vehicles filtered by garage ID and status.
     */
    @Override
    public List<VehicleDTO> findByGarageId(int garageId, int status) {
        VehicleStatuses statuses = VehicleStatuses.values()[status];
        return vehicleDAO.findByGarageId(garageId, statuses)
                .stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    /**
     * Counts the number of planned tasks within the specified date range and status.
     * @param status Status of the vehicle.
     * @return Number of planned vehicle matching criteria, or 0 if none found.
     */
    @Override
    public int getVehiclesCountByStatus(int status) {
        VehicleStatuses statuses = VehicleStatuses.values()[status];
        return vehicleDAO.getVehiclesCountByStatus(statuses);
    }

    /**
     * Converts a {@link Vehicle} entity to a {@link VehicleDTO}.
     *
     * @param vehicle the {@link Vehicle} entity to convert.
     * @return the corresponding {@link VehicleDTO}.
     */
    private VehicleDTO convertToDTO(Vehicle vehicle) {

        Garage garage = vehicle.getGarage();
        VehicleDTO dto = new VehicleDTO();

        dto.setId(vehicle.getId());
        dto.setDoorNo(vehicle.getDoorNo());
        dto.setCapacity(vehicle.getCapacity());
        dto.setFuelTank(vehicle.getFuelTank());
        dto.setPlate(vehicle.getPlate());
        dto.setPersonOnFoot(vehicle.getPersonOnFoot());
        dto.setPersonOnSit(vehicle.getPersonOnSit());
        dto.setSuitableForDisabled(vehicle.getSuitableForDisabled());
        dto.setModelYear(vehicle.getModelYear());
        dto.setGarageId(garage.getId());
        dto.setGarageName(garage.getGarageName());
        dto.setStatus(vehicle.getStatus().ordinal());
        return dto;
    }
}
