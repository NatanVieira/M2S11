namespace M2S11.Models {
    public class Album {
        public int Id { get; internal set; }
        public Artista Artista { get; set; }
        public List<Musica> Musicas { get; set; }
        public string Nome { get; set; }
    }
}
