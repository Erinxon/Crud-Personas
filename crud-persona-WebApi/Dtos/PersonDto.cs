using System;
using System.Collections.Generic;

namespace CrudPersonasWebApi.Dtos
{
    public class PersonDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public DateTime Birth { get; set; }
        public virtual List<ContactMeansPersonDto> ContactMeansPeople { get; set; }
    }
}
