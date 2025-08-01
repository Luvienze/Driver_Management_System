package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.RouteDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Route;
import java.util.List;

/**
 * Service interface for managing route-related operations.
 */
public interface RouteService {

    /**
     * Finds a route by its name.
     *
     * @param routeName the name of the route to search for.
     * @return the {@link RouteDTO} corresponding to the given name, or null if not found.
     */
    RouteDTO findRouteByRouteName(String routeName);

    /**
     * Retrieves all routes in the system.
     *
     * @return a list of all {@link RouteDTO} objects.
     */
    List<RouteDTO> getAllRoutes();
}
