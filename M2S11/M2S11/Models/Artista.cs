namespace M2S11.Models {
    public class Artista {
        public int Id { get; internal set; }
        public string Nome { get; set; }

        public List<Musica> Musicas { get; set; }
        public List<Album> Albuns { get; set; }
    }
}
