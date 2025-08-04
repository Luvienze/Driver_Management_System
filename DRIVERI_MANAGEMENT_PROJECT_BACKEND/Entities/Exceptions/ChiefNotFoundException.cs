
namespace Entities.Exceptions
{
    public class ChiefNotFoundException : NotFoundException
    {
        public ChiefNotFoundException(int id) : base($"Chief with id {id} not found.")
        { }
    }
}
