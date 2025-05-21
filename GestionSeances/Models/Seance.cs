using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionSeances.Models
{
    public class Seance
    {
        [Key]
        public int SeanceId { get; set; }

        [Required]
        public int IdK { get; set; } // Foreign key to Kine

        [Required]
        public int IdP { get; set; } // Foreign key to Patient

        [Required]
        public DateTime DateS { get; set; }

        [Required]
        [MaxLength(50)]
        public string HeureS { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeSoin { get; set; }

        public string? ReservedBy { get; set; } // UserId or username of the user who reserved

        // Navigation properties
        public Kine Kine { get; set; }
        public Patient Patient { get; set; }
    }
}