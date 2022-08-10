namespace M2S11.Models {
    public class Musica {
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public Artista Artista { get; set; }
        public Album Album { get; set; }

    }
}
