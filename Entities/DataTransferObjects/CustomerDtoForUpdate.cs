using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CustomerDtoForUpdate : CustomerDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }


}
