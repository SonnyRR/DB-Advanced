using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarDealer.Models
{
    public class Customer
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }
       
        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}