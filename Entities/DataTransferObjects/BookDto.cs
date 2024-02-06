namespace Entities.DataTransferObjects
{
    public record BookDto
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public String Writer { get; init; }
        public String Genre { get; init; }
        public int PageNumber { get; init; }
        public DateTime RecordDate { get; init; }
    }
}
