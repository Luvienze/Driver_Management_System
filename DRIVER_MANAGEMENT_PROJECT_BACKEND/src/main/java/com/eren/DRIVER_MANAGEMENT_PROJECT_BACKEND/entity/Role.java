package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Roles;
import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.persistence.*;
import lombok.*;

/**
 * Represents role in the system.
 * </p>
 * Role storing information such as role name and is associated with a person.
 */
@Schema(description = "Role storing information such as role name and is associated with a person.")
@Entity
@Table(name = "role")
@Data
@AllArgsConstructor
@NoArgsConstructor
@ToString
public class Role {

    /**
     * Unique identifier for the role (e.g. 1).
     */
    @Schema(description = "Unique identifier for the role.", example = "1")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    /**
     * Title or the name of the role (e.g. ADMIN, CHIEF, DRIVER).
     */
    @Schema(description = "Title or the name of the role.", example = "CHIEF")
    @Enumerated(EnumType.ORDINAL)
    @Column(name = "role_name", nullable = false)
    private Roles roleName;

    /**
     * Person who is assigned to a role.
     */
    @Schema(description = " Person who is assigned to a role.")
    @ManyToOne
    @JoinColumn(name = "person_id", referencedColumnName = "id")
    private Person person;
}
