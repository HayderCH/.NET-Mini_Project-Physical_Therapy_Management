using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Seance
{
    [Key]
    public int SeanceId { get; set; }

    [Required]
    public int IdK { get; set; } // Foreign key vers Kine

    [Required]
    public int IdP { get; set; } // Foreign key vers Patient

    [Required]
    public DateTime DateS { get; set; }

    [Required]
    [MaxLength(50)]
    public string HeureS { get; set; }

    [Required]
    [MaxLength(100)]
    public string TypeSoin { get; set; }

    // Navigation properties
    public Kine Kine { get; set; }
    public Patient Patient { get; set; }
}