using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CrudPersonasWebApi.Models
{
    public partial class ContactMeansPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ContactMeansId { get; set; }
        public int? PersonId { get; set; }
        public string Contact { get; set; }

        public virtual ContactMean ContactMeans { get; set; }
        public virtual Person Person { get; set; }
    }
}
