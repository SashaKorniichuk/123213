using AspNetCore.Identity.MongoDbCore.Models;

namespace Data.Entities
{
    public class User : MongoIdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
