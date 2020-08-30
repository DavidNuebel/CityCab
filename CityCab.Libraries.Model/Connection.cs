using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CityCab.Libraries.Model
{
    public class Connection
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("AccountID")]
        public Account Account { get; set; }

        [Required]
        public string ConnectionID { get; set; }
    }
}
