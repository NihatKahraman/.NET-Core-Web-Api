namespace Entities.Exceptions
{
    public sealed class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(int id)
            : base($"The customer with id:{id} could not found.")
        {

        }
    }
}
