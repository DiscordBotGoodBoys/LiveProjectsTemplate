using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

/*I created this (backend) experience system to learn about databases. We can treat this
  as a sacrificial feature, we can implement it when we're done with everything else.*/ 



namespace ClueBot.Resources.Database
{
    public class Experience
    {
        [Key]
        public ulong UserId { get; set; }
        public int Amount { get; set; }


    }
}
