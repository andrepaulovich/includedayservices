using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace IncludeDay.Data.Entities
{
    public class Feedback
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [StringLength(128)]
        public string UserName { get; set; }

    }
}

