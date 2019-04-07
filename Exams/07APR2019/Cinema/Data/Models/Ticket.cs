namespace Cinema.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ProjectionId { get; set; }
        public Projection Projection { get; set; }
    }
}
