namespace Entities.Exceptions
{
    public sealed class ReservationNotFoundException : NotFoundException
    {
        public ReservationNotFoundException(int id)
            : base($"The reservation with id:{id} could not found.")
        {

        }
    }
}
