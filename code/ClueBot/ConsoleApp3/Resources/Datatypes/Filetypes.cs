using System.Collections.Generic;


namespace ClueBot.Resources.Datatypes
{
    public class Setting
    {
        public string token { get; set; }
        public ulong owner { get; set; }
        //public List<ulong> log { get; set; }
        public string version { get; set; }
    }
}
