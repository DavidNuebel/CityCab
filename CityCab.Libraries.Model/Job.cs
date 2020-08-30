using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CityCab.Libraries.Model
{
    public class Job
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(75)]
        public string Origin { get; set; }

        [Required]
        [MaxLength(75)]
        public string Destination { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [ForeignKey("DriverID")]
        public Driver Diver { get; set; }
    }
}
