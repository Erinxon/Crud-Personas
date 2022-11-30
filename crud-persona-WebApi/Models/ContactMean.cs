using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CrudPersonasWebApi.Models
{
    public partial class ContactMean
    {
        public ContactMean()
        {
            ContactMeansPeople = new HashSet<ContactMeansPerson>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactMeansId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ContactMeansPerson> ContactMeansPeople { get; set; }
    }
}
