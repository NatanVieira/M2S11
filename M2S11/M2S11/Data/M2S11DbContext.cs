using M2S11.Models;
using Microsoft.EntityFrameworkCore;

namespace M2S11.Data {
    public class M2S11DbContext : DbContext {

        private readonly IConfiguration _configuration;
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<MusicaPlaylist> MusicasPlaylists { get; set; }
        public M2S11DbContext(IConfiguration configuration) {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CONEXAO_BANCO"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Artista
            modelBuilder.Entity<Artista>().ToTable("Artistas");
            modelBuilder.Entity<Artista>().HasKey(a => a.Id);
            modelBuilder.Entity<Artista>()
                .Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            //Musicas
            modelBuilder.Entity<Musica>().ToTable("Musicas");
            modelBuilder.Entity<Musica>().HasKey(m => m.Id);
            modelBuilder.Entity<Musica>()
                .Property(m => m.Nome)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Musica>()
                .HasOne<Artista>(m => m.Artista)
                .WithMany(a => a.Musicas)
                .HasForeignKey(m => m.ArtistaId);

            modelBuilder.Entity<Musica>()
                .HasOne<Album>(m => m.Album)
                .WithMany(a => a.Musicas)
                .HasForeignKey(m => m.AlbumId);

            //Albuns
            modelBuilder.Entity<Album>().ToTable("Albums");
            modelBuilder.Entity<Album>().HasKey(a => a.Id);
            modelBuilder.Entity<Album>()
                .Property(a => a.Nome)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Album>()
                .HasOne<Artista>(al => al.Artista)
                .WithMany(ar => ar.Albuns)
                .HasForeignKey(al => al.ArtistaId);

            //Playlists
            modelBuilder.Entity<Playlist>().ToTable("Playlists");
            modelBuilder.Entity<Playlist>().HasKey(p => p.Id);
            modelBuilder.Entity<Playlist>()
                .Property(p => p.Nome)
                .HasMaxLength(200)
                .IsRequired();

            //MusicasPlaylists
            modelBuilder.Entity<MusicaPlaylist>().ToTable("MusicasPlaylists");
            modelBuilder.Entity<MusicaPlaylist>()
                .HasKey(mp => new { mp.MusicaId,mp.PlaylistId });
            
            modelBuilder.Entity<MusicaPlaylist>()
                .HasOne<Musica>(mp => mp.Musica)
                .WithMany()
                .HasForeignKey(m => m.MusicaId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MusicaPlaylist>()
                .HasOne<Playlist>(mp => mp.Playlist)
                .WithMany()
                .HasForeignKey(mp => mp.PlaylistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
