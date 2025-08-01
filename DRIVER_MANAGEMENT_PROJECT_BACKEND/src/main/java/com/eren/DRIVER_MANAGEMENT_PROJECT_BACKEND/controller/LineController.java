package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.LineDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Line;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.LineService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping(value = "/line")
@Tag(name = "Line API", description = "Endpoints are managing line related operations.")
public class LineController {

    @Autowired
    private LineService lineService;

    /**
     * Retrieve list of all lines.
     *
     * @return list of {@link LineDTO}
     */
    @Operation(
            summary = "Get all lines",
            description = "Returns a list of all line records as DTO's"
    )
    @PostMapping(value = "/list")
    public ResponseEntity<List<LineDTO>> list() {
        return new ResponseEntity<>(lineService.getAllLines(), HttpStatus.OK);
    }

    /**
     * Finds line by their line code.
     *
     * @param lineCode Line code of line.
     * @return finds {@link LineDTO} by line code
     */
    @Operation(
            summary = "Find line by line code",
            description = "Finds line records by line code as DTO's"
    )
    @PostMapping(value = "/find/code")
    public ResponseEntity<LineDTO> getLineByLineCode(
            @Parameter(description = "Line code of line", required = true)
            @RequestParam String lineCode) {
        LineDTO lineDTO = lineService.getLineByLineCode(lineCode);
        return new ResponseEntity<>(lineDTO, HttpStatus.OK);
    }
}
