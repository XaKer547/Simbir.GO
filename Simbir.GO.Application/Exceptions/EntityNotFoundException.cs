namespace Simbir.GO.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type) : base($"Entity {type} not found")
        { }
    }
}
