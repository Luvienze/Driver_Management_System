package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.PersonRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.PersonDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Person;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.BloodGroups;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.enums.Genders;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.PersonService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class PersonServiceImpl implements PersonService {

    private final String PLACEHOLDER_IMAGE = "placeholder.jpg";

    @Value("${external.images.path.url:/images/}")
    private String imagesUrlPrefix;

    @Autowired
    private PersonRepository personRepository;

    /**
     * Finds a Person by their ID.
     *
     * @param id The unique identifier of the Person.
     * @return PersonDTO representing the found Person, or null if not found.
     */
    @Override
    public PersonDTO findPersonById(int id) {
        Person person = personRepository.findPersonById(id);
        return convertToDto(person);
    }

    /**
     * Finds the Person associated with a given Driver ID.
     *
     * @param id The Driver's unique identifier.
     * @return PersonDTO representing the Person linked to the Driver, or null if not found.
     */
    @Override
    public PersonDTO findPersonByDriverId(int id) {
        Person person = personRepository.findPersonByDriverId(id);
        return convertToDto(person);

    }

    /**
     * Finds a Person by their registration number.
     *
     * @param registrationNumber The registration number of the Person.
     * @return PersonDTO representing the found Person.
     * @throws IllegalArgumentException if registrationNumber is null or empty.
     */
    @Override
    public PersonDTO findPersonByRegistrationNumber(String registrationNumber) {
        if (registrationNumber.isEmpty()) {
            System.out.println("Registration number is empty");
        }
        Person person = personRepository.findPersonByRegistrationNumber(registrationNumber);
        return convertToDto(person);
    }

    /**
     * Retrieves all Persons as a list of PersonDTOs.
     *
     * @return List of PersonDTO objects representing all Persons in the repository.
     */
    @Override
    public List<PersonDTO> getAllPersonsDTO() {
        return personRepository.findAll()
                .stream()
                .map(this::convertToDto)
                .collect(Collectors.toList());
    }

    /**
     * Saves a new Person or updates an existing one based on the provided PersonDTO.
     *
     * @param personDTO The PersonDTO containing data to save or update.
     * @return PersonDTO representing the saved or updated Person.
     * @throws RuntimeException if trying to update a Person that does not exist.
     */
    @Override
    public PersonDTO saveOrUpdatePerson(PersonDTO personDTO) {
        Person person;
        Integer id = personDTO.getId();

        if (id != null && id > 0) {
            person = personRepository.findPersonById(id);

            if (person == null) {
                throw new RuntimeException("Person with id " + id + " not found.");
            }
            person.setFirstName(personDTO.getFirstName());
            person.setLastName(personDTO.getLastName());
            person.setAddress(personDTO.getAddress());
            person.setPhone(personDTO.getPhone());
            person.setGender(Genders.values()[personDTO.getGender()]);
            person.setBloodGroup(BloodGroups.values()[personDTO.getBloodGroup()]);
            person.setDateOfBirth(personDTO.getDateOfBirth());
            person.setRegistrationNumber(personDTO.getRegistrationNumber());
            person.setImageUrl(personDTO.getImageUrl());
        } else {
            person = new Person();
            person.setFirstName(personDTO.getFirstName());
            person.setLastName(personDTO.getLastName());
            person.setPhone(personDTO.getPhone());
            person.setAddress(personDTO.getAddress());
            person.setGender(Genders.values()[personDTO.getGender()]);
            person.setBloodGroup(BloodGroups.values()[personDTO.getBloodGroup()]);
            person.setRegistrationNumber(personDTO.getRegistrationNumber());
            person.setDateOfBirth(personDTO.getDateOfBirth());
            person.setImageUrl(personDTO.getImageUrl());
        }
        person.setDateOfStart(personDTO.getDateOfStart());
        person.setIsDeleted(personDTO.getIsDeleted());

        Person savedPerson = personRepository.save(person);
        return convertToDto(savedPerson);
    }

    /**
     * Deletes a Person by their registration number.
     *
     * @param registrationNumber The registration number of the Person to delete.
     * @throws IllegalArgumentException if registrationNumber is null or empty.
     */
    @Override
    public void deletePersonByRegistrationNumber(String registrationNumber) {
        if(registrationNumber.isEmpty()){
            System.out.println("Registration number is empty");
        }

        Person person = personRepository.findPersonByRegistrationNumber(registrationNumber);
        if(person == null){
            System.out.println("Person with registration number " + registrationNumber + " not found.");
        }
        personRepository.deletePersonByRegistrationNumber(registrationNumber);
    }

    /**
     * Converts a Person entity to a PersonDTO.
     *
     * @param person The Person entity to convert.
     * @return The corresponding PersonDTO.
     */
    private PersonDTO convertToDto(Person person) {
        PersonDTO dto = new PersonDTO();
        dto.setId(person.getId());
        dto.setFirstName(person.getFirstName());
        dto.setLastName(person.getLastName());
        dto.setGender(person.getGender().ordinal());
        dto.setBloodGroup(person.getBloodGroup().ordinal());
        dto.setDateOfBirth(person.getDateOfBirth());
        dto.setPhone(person.getPhone());
        dto.setAddress(person.getAddress());
        String imageName = person.getImageUrl();
        if (imageName == null || imageName.isEmpty()) {
            imageName = PLACEHOLDER_IMAGE;  // Resim yoksa placeholder kullan
        }

        dto.setImageUrl(imagesUrlPrefix + imageName);
        dto.setRegistrationNumber(person.getRegistrationNumber());
        dto.setDateOfStart(person.getDateOfStart());
        dto.setIsDeleted(person.getIsDeleted());
        return dto;
    }
}
