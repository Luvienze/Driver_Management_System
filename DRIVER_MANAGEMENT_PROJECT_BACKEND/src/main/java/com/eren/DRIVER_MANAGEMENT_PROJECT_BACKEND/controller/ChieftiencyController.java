package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.ChieftaincyDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Chieftiency;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation.ChieftiencyServiceImpl;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import java.util.List;

@RestController
@RequestMapping(value = "/chieftiency")
@Tag(name = "Chieftaincy API", description = "Endpoints are managing chieftaincy related operations.")
public class ChieftiencyController {

    @Autowired
    private ChieftiencyServiceImpl chieftiencyService;

    /**
     * Retrieves a list of Chieftaincies as DTO's.
     * @return List of {@link ChieftaincyDTO}
     */
    @Operation(
            summary = "Get all chieftaincies",
            description = "Returns a list of all chieftaincy records as DTOs"
    )
    @PostMapping(value = "/list")
    public List<ChieftaincyDTO> listAllChiftiencies() {
        return chieftiencyService.findAllChieftiencies();
    }
}
