namespace VaporStore.Data.Models
{
    using System.Collections.Generic;

    public class GameTag
    {
        public int GameId { get; set; }
        public Game Game { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
