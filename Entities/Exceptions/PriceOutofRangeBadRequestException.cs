namespace Entities.Exceptions
{
    public abstract partial class BadRequestException
    {
        public class PriceOutofRangeBadRequestException : BadRequestException
        {
            public PriceOutofRangeBadRequestException() 
                : base("Maxiumum price should ve less than 1000 and greater than 10.")
            {

            }
        }
    }
}
