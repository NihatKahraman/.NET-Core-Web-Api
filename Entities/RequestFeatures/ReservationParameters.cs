namespace Entities.RequestFeatures
{
    public class ReservationParameters : RequestParameters
    {

        public String? SearchTerm { get; set; }

        public ReservationParameters()
        {
            OrderBy = "id";
        }
    }
}

