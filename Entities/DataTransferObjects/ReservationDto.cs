using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ReservationDto
    {
        public int Id { get; init; }
        public String Book { get; init; }
        public String Customer { get; init; }
        public DateTime BorrowDate { get; init; }
        public DateTime BorrowEndDate { get; init; }
    }
}
