using System.Collections.Generic;


namespace ClueBot.Resources.Datatypes
{
    public class Setting
    {
        public static string Token { get; set; }
        public static ulong Owner { get; set; }
        public static List<ulong> Log { get; set; }
        public static string Version { get; set; }
        public static List<ulong> Banned { get; set; }
    }
}
