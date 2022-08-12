using System.ComponentModel.DataAnnotations;

namespace M2S11.DTOs {
    public class MusicaPlaylistDTO {
        [Required]
        public int MusicaId { get; set; }
        [Required]
        public int PlaylistId { get; set; }
    }
}
