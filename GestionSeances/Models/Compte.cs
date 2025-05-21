using System.ComponentModel.DataAnnotations;

public class Compte
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Login { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } // "admin" ou "user"
}