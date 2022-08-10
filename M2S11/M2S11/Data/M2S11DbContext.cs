using M2S11.Models;
using Microsoft.EntityFrameworkCore;

namespace M2S11.Data {
    public class M2S11DbContext : DbContext {

        private IConfiguration _configuration;
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Artista> Artista { get; set; }

        public M2S11DbContext(IConfiguration configuration) {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CONEXAO_BANCO"));
        }
    }
}
