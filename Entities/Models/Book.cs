using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Book
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public String Writer { get; init; }
        public String Genre { get; init; }
        public int PageNumber { get; init; }
        public DateTime RecordDate { get; init; }
    }
}
