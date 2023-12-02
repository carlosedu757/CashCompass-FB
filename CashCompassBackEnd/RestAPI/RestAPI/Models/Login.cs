using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class Login
{
    [MaxLength(50)]
    public string Email { get; set; }

    public string Password { get; set; }
}
