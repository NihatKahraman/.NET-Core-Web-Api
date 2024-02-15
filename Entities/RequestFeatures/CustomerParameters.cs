namespace Entities.RequestFeatures
{
    public class CustomerParameters : RequestParameters
    {

        public String? SearchTerm { get; set; }

        public CustomerParameters()
        {
            OrderBy = "id";
        }
    }
}

