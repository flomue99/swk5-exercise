using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableReferenceTypes
{
    internal class Person(string? firtName, string lastName)
    {
        public string? FirstName { get; set; } = firtName;

        public string LastName { get; set; } = lastName ?? throw new ArgumentNullException("Lastname must not be null");
    }
}
