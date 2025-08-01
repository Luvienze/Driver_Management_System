package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.controller;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.PersonDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.RoleDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Role;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation.PersonServiceImpl;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation.RoleServiceImpl;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
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
@RequestMapping(value = "/role")
@Tag(name = "Role API", description = "Endpoints are managing role related operations")

public class RoleController {

    @Autowired
    private RoleServiceImpl roleService;
    @Autowired
    private PersonServiceImpl personService;

    /**
     * Retrieves a list of all roles.
     *
     * @return List of Role entities
     */
    @Operation(
            summary = "Get all roles",
            description = "Retrieves all roles from the database"
    )
    @PostMapping(value = "/list")
    public List<Role> listAllRoles() {
        return roleService.getAllRoles();
    }

    /**
     * Finds the roles associated with a person by their ID.
     *
     * @param personId the ID of the person
     * @return ResponseEntity containing list of RoleDTOs or appropriate HTTP status
     */
    @Operation(
            summary = "Get roles by person ID",
            description = "Finds and returns all roles assigned to a person using their ID"
    )
    @PostMapping(value = "/find/list")
    public ResponseEntity<List<RoleDTO>> findRolesByPersonId(
            @Parameter(description = "Identifier of person", required = true)
            @RequestParam int personId) {
        if(personId < 0){
            System.out.println("person id is" + personId);
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
        PersonDTO personDTO = personService.findPersonById(personId);
        if (personDTO == null) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }

        return new ResponseEntity<>(roleService.getRoleListByPersonId(personDTO), HttpStatus.OK);
    }

    /**
     * Authenticates a user using registration number and phone number.
     *
     * @param regNo the registration number
     * @param phone the phone number
     * @return ResponseEntity containing list of RoleDTOs if login is successful
     */
    @Operation(
            summary = "Login by registration number and phone",
            description = "Logs in a user and returns their associated roles based on registration number and phone number"
    )
    @PostMapping(value = "/login")
    public ResponseEntity<List<RoleDTO>> login(
            @Parameter(description = "Registration number of person", required = true)
            @RequestParam String regNo,
            @Parameter(description = "Phone number of person", required = true)
            @RequestParam String phone) {
        List<RoleDTO> dtoList = roleService.findRolesByRegNoAndPhone(regNo, phone);
        if (dtoList == null || dtoList.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(dtoList, HttpStatus.OK);
    }

}
