using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace ClueBot.Resources.Database
{
    public class Experience
    {
        [Key]
        public ulong UserId { get; set; }
        public int Amount { get; set; }


    }
}
