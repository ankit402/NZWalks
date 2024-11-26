using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTO
{
    public class LoginRequestDTo
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
