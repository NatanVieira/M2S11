using System.ComponentModel.DataAnnotations;

namespace M2S11.DTOs {
    public class PlaylistDTO {

        [Required]
        public string? Nome { get; set; }
    }
}
