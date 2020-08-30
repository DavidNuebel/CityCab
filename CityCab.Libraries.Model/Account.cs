using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CityCab.Libraries.Model
{
    public class Account
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("PersonID")]
        public Person Person { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public List<Connection> Connections { get; set; }

        public Guid AccessToken { get; set; }
    }
}
