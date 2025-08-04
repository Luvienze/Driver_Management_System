using Entities.Enums;
namespace Entities.Models
{
    public class Role
    {
        public int Id { get; set; }
        public Roles RoleName{ get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
