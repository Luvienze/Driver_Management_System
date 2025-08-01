package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.wrapper;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.DriverDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.PersonDTO;
import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for wrapper of person dto and driver dto.
 * </p>
 * Contains data transfer objects of person and driver.
 */
@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class PersonDriverRequestDto {

    /**
     * Data transfer object of person.
     */
    @Schema(description = "Data transfer object of person.")
    private PersonDTO personDto;

    /**
     * Data transfer object of driver.
     */
    @Schema(description = "Data transfer object of driver.")
    private DriverDTO driverDto;
}
