using System.ComponentModel.DataAnnotations;

namespace M2S11.DTOs {
    public class ArtistaDTO {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }
    }
}
