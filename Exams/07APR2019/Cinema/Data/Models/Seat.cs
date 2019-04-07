namespace Cinema.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Seat
    {
        [Key]
        public int Id { get; set; }

        public int HallId { get; set; }
        public Hall Hall { get; set; }
    }
}
