package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.RouteDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Route;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.RouteService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.apache.coyote.Response;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping(value = "/route")
@Tag(name = "Route API", description = "Endpoints are managing route related operations.")
public class RouteController {

    @Autowired
    private RouteService routeService;

    /**
     * Retrieves a list of all routes.
     *
     * @return ResponseEntity containing a list of RouteDTO objects with HTTP status 200 (OK)
     */
    @Operation(
            summary = "Get all routes",
            description = "Retrieves all available routes"
    )
    @PostMapping(value = "/list")
    public ResponseEntity<List<RouteDTO>> getRouteList() {
        return new ResponseEntity<>(routeService.getAllRoutes(), HttpStatus.OK);
    }

    /**
     * Retrieves a route by its route name.
     *
     * @param routeName the name of the route to find
     * @return ResponseEntity containing the RouteDTO if found,
     *         or HTTP status 400 (BAD_REQUEST) if the route name is empty or no route found with the given name
     */
    @Operation(
            summary = "Find route by route name",
            description = "Finds and returns the route matching the given route name"
    )
    @PostMapping(value = "/find/name")
    public ResponseEntity<RouteDTO> getRouteByRouteName(
            @Parameter(description = "Route name of route", required = true)
            @RequestParam String routeName) {
        if(routeName.isEmpty()){
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        if (routeService.findRouteByRouteName(routeName) == null) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        return new ResponseEntity<>(routeService.findRouteByRouteName(routeName), HttpStatus.OK);
    }
}
