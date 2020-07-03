using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDCore3Ang7.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
    }
}