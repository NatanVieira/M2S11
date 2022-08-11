using M2S11.Data;
using M2S11.DTOs;
using M2S11.Models;
using Microsoft.AspNetCore.Mvc;

namespace M2S11.Controllers {
    [ApiController]
    [Route("api/albuns")]
    public class AlbumController : ControllerBase {

        private readonly M2S11DbContext _context;

        public AlbumController(M2S11DbContext context) {
            _context = context;
        }

        //GET
        [HttpGet]
        public ActionResult<List<Album>> Get() {
            return Ok(_context.Albums.ToList());
        }

        //GET com ID
        [HttpGet("{id}")]
        public ActionResult<Album> GetById([FromRoute] int id) {
            var album = _context.Albums.Find(id);
            return Ok(album);
        }

        //GET com nome
        [HttpGet]
        public ActionResult<List<Album>> GetByname([FromQuery] string nomeFiltro) {
            var query = _context.Albums.AsQueryable();
            if(!string.IsNullOrEmpty(nomeFiltro)) {
                query = query.Where(a => a.Nome == nomeFiltro);
            }
            return Ok(query.ToList());
        }

        //Post
        [HttpPost]
        public ActionResult<Album> Post([FromBody] AlbumDTO body) {
            Album album = new(){
                Nome = body.Nome,
                Artista = _context.Artistas.Find(body.ArtistaId),
                ArtistaId = body.ArtistaId,
            };
            body.MusicasIds.ForEach(id =>
            {
                var musica = _context.Musicas.Find(id);
                if(musica != null)
                    album.Musicas.Add(musica);
            });

            _context.Albums.Add(album);
            _context.SaveChanges();
            return Created("api/albums",album);
        }
        //Put
        [HttpPut("{id}")]
        public ActionResult<Album> Put([FromBody] AlbumDTO body,
                                       [FromRoute] int id) {
            var album = _context.Albums.Find(id);
            if(album != null) {
                album.Nome = body.Nome;
            }
            _context.SaveChanges();
            return Ok(album);
        }
        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) {
            var album = _context.Albums.Find(id);
            _context.Albums.Remove(album);
            _context.SaveChanges();
            return Ok();
        }

    }
}
