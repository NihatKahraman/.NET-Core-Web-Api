namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
	{
        
        public String? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "id";
        }
    }
}
