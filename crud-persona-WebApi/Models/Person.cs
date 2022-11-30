using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CrudPersonasWebApi.Models
{
    public partial class Person
    {
        public Person()
        {
            ContactMeansPeople = new HashSet<ContactMeansPerson>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public DateTime Birth { get; set; }

        public virtual ICollection<ContactMeansPerson> ContactMeansPeople { get; set; }
    }
}
