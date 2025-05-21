using System.ComponentModel.DataAnnotations;

public class Patient
{
    [Key]
    public int IdP { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nomp { get; set; }

    [Required]
    [MaxLength(50)]
    public string PrenomP { get; set; }

    [Phone]
    public string NumTel { get; set; }
}