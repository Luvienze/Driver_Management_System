package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Route;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

/**
 * Repository interface for Route entity.
 * </p>
 * Provides methods for performing database operations related to routes.
 */
@Repository
public interface RouteRepository extends JpaRepository<Route, Integer> {

    /**
     * Finds route by their route name.
     * @param routeName RouteName of route.
     * @return The matching route entity, or null if not found.
     */
    @Query("""
        SELECT r FROM Route r
        WHERE r.routeName = :routeName
    """)
    Route findRouteByRouteName(@Param("routeName") String routeName);
}
