using System.Text.Json.Serialization;

namespace WebApiProject.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string[] Roles { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
