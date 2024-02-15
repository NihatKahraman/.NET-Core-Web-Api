using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record CustomerDto
    {
        public int Id { get; init; }
        public String Name { get; init; }
        public String Gender { get; init; }
        public double PhoneNumber { get; init; }
        public int Age { get; init; }
    }
}
