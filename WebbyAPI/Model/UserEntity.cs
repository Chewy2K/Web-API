using System.ComponentModel.DataAnnotations;

namespace WebbyAPI.Model
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Emailaddress { get; set; }
        public string Mobilenumber { get; set; }
        public string Password { get; set; }



    }
}
