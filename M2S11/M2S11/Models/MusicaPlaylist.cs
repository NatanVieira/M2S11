namespace M2S11.Models {
    public class MusicaPlaylist {

        public int MusicaId { get; set; }

        public int PlaylistId { get; set; }

        public Musica Musica { get; set; }
        public Playlist Playlist { get; set; }
    }
}
