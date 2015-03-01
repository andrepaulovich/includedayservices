using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncludeDay.Data.Entities
{
    public class Location
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string  Description { get; set; }

        public string LocationType { get; set; }

        [Required]
        [StringLength(128)]
        public string City { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }

    }
}

