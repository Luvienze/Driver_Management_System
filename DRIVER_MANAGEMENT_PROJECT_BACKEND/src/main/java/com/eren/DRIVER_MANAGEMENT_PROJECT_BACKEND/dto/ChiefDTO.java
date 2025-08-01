package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.ToString;

/**
 * Data Transfer Object for Chief entity.
 * </p>
 * Contains detailed information about a chief, including name, related garage, chieftaincy and current status.
 */
@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class ChiefDTO {

    /**
     * Unique identifier of the chief (e.g. 1).
     */
    @Schema(description = "Unique identifier of the chief.", example = "1")
    private Integer id;

    /**
     * Identifier of the person assigned as a chief (e.g. 1).
     */
    @Schema(description = "Identifier of the person assigned as a chief.", example = "1")
    private Integer personId;

    /**
     * First name of the chief (e.g. John).
     */
    @Schema(description = "First name of the chief.", example = "John")
    private String chiefFirstName;

    /**
     * Last name of the chief (e.g. Doe).
     */
    @Schema(description = "Last name of the chief.", example = "Doe")
    private String chiefLastName;

    /**
     * Name of the garage the chief is responsible for (e.g. Merkez Garajı).
     */
    @Schema(description = "Name of the garage the chief is responsible for.", example = "Merkez Garajı")
    private String garageName;

    /**
     * Identifier of the garage (e.g. 1).
     */
    @Schema(description = "Identifier of the garage.", example = "1")
    private Integer garageId;

    /**
     * Title or position name of the chief (e.g., "Ulaşım Şefliği").
     */
    @Schema(description = "Title or position name of the chief.", example = "Ulaşım Şefliği")
    private String chieftiencyName;

    /**
     * Indicates whether the chief is currently active (e.g. true).
     */
    @Schema(description = "Indicates whether the chief is currently active.", example = "true")
    private boolean isActive;

}
