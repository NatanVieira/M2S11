using System.ComponentModel.DataAnnotations;

namespace M2S11.DTOs {
    public class AlbumDTO {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public int ArtistaId { get; set; }
        [Required]
        public List<int> MusicasIds { get; set; }
    }
}
