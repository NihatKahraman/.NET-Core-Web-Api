using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record CustomerDtoForManipulation
    {

        [Required(ErrorMessage = "Name is a required field.")]
        public String Name { get; init; }

        [Required(ErrorMessage = "Gender is a required field.")]
        public String Gender { get; init; }

        [Required(ErrorMessage = "PhoneNumber is a required field.")]
        public double PhoneNumber { get; init; }

        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; init; }
    }
}
