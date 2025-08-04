using Entities.Enums;
namespace Entities.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Genders Gender { get; set; }
        public BloodGroups BloodGroup { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string RegistrationNumber { get; set; }
        public DateOnly DateOfStart { get; set; }
        public bool IsDeleted { get; set; }
        public Driver Driver { get; set; }

    }
}
