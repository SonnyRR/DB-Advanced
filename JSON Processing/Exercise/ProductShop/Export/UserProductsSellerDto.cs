using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProductShop.Export
{
    public class UserProductsSellerDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public List<UserSoldProductsDto> SoldProducts { get; set; }
    }
}
