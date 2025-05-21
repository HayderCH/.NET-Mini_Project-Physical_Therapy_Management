using System.ComponentModel.DataAnnotations;

public class Kine
{
    [Key]
    public int IdK { get; set; }

    [Required]
    [MaxLength(50)]
    public string NomK { get; set; }

    [Required]
    [MaxLength(50)]
    public string PrenomK { get; set; }
}