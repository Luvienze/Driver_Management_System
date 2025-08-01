package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for Chieftaincy entity.
 * </p>
 * Contains detailed information about garage.
 */

@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class ChieftaincyDTO {

    /**
     * Unique identifier for the chieftaincy (e.g. 1).
     */
    @Schema(description = "Unique identifier for the chieftaincy.", example = "1")
    public Integer id;

    /**
     * Title or name for the chieftaincy (e.g. Ulaşım Şefliği).
     */
    @Schema(description = "Title or name for the chieftaincy.", example = "Ulaşım Şefliği")
    public String chieftiencyName;

    /**
     * Identifier for the garage (e.g. 1).
     */
    @Schema(description = "Identifier for the garage.", example = "1")
    public Integer garageId;

    /**
     * Title or name for the garage (e.g. Merkez Garajı)
     */
    @Schema(description = "Title or name for the garage", example = "Merkez Garajı")
    public String garageName;
}
