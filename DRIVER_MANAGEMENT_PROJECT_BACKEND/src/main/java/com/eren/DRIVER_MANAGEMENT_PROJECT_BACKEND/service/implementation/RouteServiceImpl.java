package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.RouteRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.RouteDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Route;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.RouteService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class RouteServiceImpl implements RouteService {

    @Autowired
    private RouteRepository routeRepository;

    /**
     * Finds a route by its route name.
     *
     * @param routeName the name of the route to find.
     * @return a {@link RouteDTO} representing the route, or {@code null} if the routeName is null,
     * empty, or no matching route is found.
     */
    @Override
    public RouteDTO findRouteByRouteName(String routeName) {
        if(routeName == null || routeName.isEmpty()) return null;

        Route route = routeRepository.findRouteByRouteName(routeName);
        if(route == null) return null;

        return convertToDto(route);
    }

    /**
     * Retrieves all routes.
     *
     * @return a list of {@link RouteDTO} representing all routes.
     */
    @Override
    public List<RouteDTO> getAllRoutes() {
        return routeRepository.findAll().stream()
                .map(this::convertToDto)
                .collect(Collectors.toList());
    }

    /**
     * Converts a {@link Route} entity to a {@link RouteDTO}.
     *
     * @param route the {@link Route} entity to convert.
     * @return the converted {@link RouteDTO}.
     */
    private RouteDTO convertToDto(Route route) {
        RouteDTO routeDTO = new RouteDTO();
        routeDTO.setId(route.getId());
        routeDTO.setRouteName(route.getRouteName());
        routeDTO.setDistance(route.getDistance());
        routeDTO.setDuration(route.getDuration());
        return routeDTO;
    }
}
