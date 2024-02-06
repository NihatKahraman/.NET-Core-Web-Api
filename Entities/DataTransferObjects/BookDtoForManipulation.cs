using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage = "Title is a required field.")]
        [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters")]
        public String Title { get; init; }

        [Required(ErrorMessage = "Writer is a required field.")]
        public String Writer { get; init; }

        [Required(ErrorMessage = "Genre is a required field.")]
        public String Genre { get; init; }

        [Required(ErrorMessage = "PageNumber is a required field.")]
        public int PageNumber { get; init; }

        [Required(ErrorMessage = "RecordDate is a required field.")]
        public DateTime RecordDate { get; init; }


    }
}
