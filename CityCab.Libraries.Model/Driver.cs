using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace CityCab.Libraries.Model
{
    public class Driver
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Account Account { get; set; }
    }
}
