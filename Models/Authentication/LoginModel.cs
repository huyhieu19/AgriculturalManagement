using System.ComponentModel.DataAnnotations;

namespace Models
{
    public record LoginModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string? Email { get; init; }
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; init; }
    }
}
