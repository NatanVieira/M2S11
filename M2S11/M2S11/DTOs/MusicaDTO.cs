using System.ComponentModel.DataAnnotations;

namespace M2S11.DTOs {
    public class MusicaDTO {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }
        [Required]
        public int ArtistaId { get; set; }
    }
}
