using System.Collections.Generic;

namespace CrudPersonasWebApi.Dtos
{
    public class ContactMeansPersonDto
    {
        public int Id { get; set; }
        public int? ContactMeansId { get; set; }
        public int? PersonId { get; set; }
        public string Contact { get; set; }
        public ContactMeanDto? ContactMeans { get; set; }
}
}
