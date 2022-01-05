using System.ComponentModel.DataAnnotations;

namespace api.DTOS
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password{ get; set; }
    }
}
