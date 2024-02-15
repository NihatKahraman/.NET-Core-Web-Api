using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ReservationDtoForManipulation
    {
        [Required(ErrorMessage = "Book is a required field.")]
        public String Book { get; init; }

        [Required(ErrorMessage = "Customer is a required field.")]
        public String Customer { get; init; }

        [Required(ErrorMessage = "BorrowDate is a required field.")]
        public DateTime BorrowDate { get; init; }

        [Required(ErrorMessage = "BorrowEndDate is a required field.")]
        public DateTime BorrowEndDate { get; init; }
    }
}
