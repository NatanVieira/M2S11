using M2S11.Data;
using M2S11.DTOs;
using M2S11.Models;
using Microsoft.AspNetCore.Mvc;

namespace M2S11.Controllers {
    [ApiController]
    [Route("api/musicas")]
    public class MusicaController : ControllerBase {

        private readonly M2S11DbContext _context;

        public MusicaController(M2S11DbContext context) {
            _context = context;
        }

        //GET
        [HttpGet]
        public ActionResult<List<Musica>> Get() {
            return Ok(_context.Musicas.ToList());
        }

        //GET com Id
        [HttpGet("{id}")]
        public ActionResult<Musica> Get([FromRoute] int id) {
            var musica = _context.Musicas.Find(id);
            return Ok(musica);
        }

        //GET com nome
        [HttpGet]
        public ActionResult<List<Musica>> Get([FromQuery] string nomeFiltro) {
            var query = _context.Musicas.AsQueryable();
            if(!string.IsNullOrEmpty(nomeFiltro)) {
                query = query.Where(m => m.Nome == nomeFiltro);
            }

            return Ok(query.ToList());
        }

        //Post
        [HttpPost]
        public ActionResult<Musica> Post([FromBody] MusicaDTO body) {
            Musica musica = new(){
                Nome = body.Nome,
                Artista = _context.Artistas.Find(body.ArtistaId),
                ArtistaId = body.ArtistaId,
            };
            _context.Musicas.Add(musica);
            _context.SaveChanges();
            return Created("api/musicas",musica);
        }

        //Put
        [HttpPut("{id}")]
        public ActionResult<Musica> Put([FromBody] MusicaDTO body,
                                        [FromRoute] int id) {
            var musica = _context.Musicas.Find(id);
            if(musica != null) {
                musica.ArtistaId = body.ArtistaId;
                musica.Nome = body.Nome;
                musica.Artista = _context.Artistas.Find(body.ArtistaId);
            }
            _context.SaveChanges();
            return Ok(musica);
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) {
            var musica = _context.Musicas.Find(id);
            _context.Musicas.Remove(musica);
            _context.SaveChanges();
            return Ok();
        }
    }
}
