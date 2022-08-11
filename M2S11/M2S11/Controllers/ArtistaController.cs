using M2S11.Data;
using M2S11.DTOs;
using M2S11.Models;
using Microsoft.AspNetCore.Mvc;

namespace M2S11.Controllers {
    [ApiController]
    [Route("api/artistas")]
    public class ArtistaController : ControllerBase {

        private readonly M2S11DbContext _context;

        public ArtistaController(M2S11DbContext context) {
            _context = context;
        }

        //GET
        [HttpGet]
        public ActionResult<List<Artista>> Get() {
            return Ok(_context.Artistas.ToList());
        }

        //GET com Id
        [HttpGet("{id}")]
        public ActionResult<Artista> Get([FromRoute] int id) {
            var artista = _context.Artistas.Find(id);
            return Ok(artista);
        }

        //GET com filtro de nome
        [HttpGet]
        public ActionResult<List<Artista>> Get([FromQuery] string nomeFiltro) {
            var query = _context.Artistas.AsQueryable();
            if(!string.IsNullOrEmpty(nomeFiltro)) {
                query = query.Where(a => a.Nome == nomeFiltro);
            }
            return Ok(query.ToList());
        }

        //Post
        [HttpPost]
        public ActionResult<Artista> Post([FromBody] ArtistaDTO body) {
            Artista artista = new(){
                Nome = body.Nome
            };
            _context.Artistas.Add(artista);
            _context.SaveChanges();
            return Created("api/artistas",artista);
        }

        //Put
        [HttpPut("{id}")]
        public ActionResult<Artista> Put([FromRoute] int id,
                                         [FromBody]  ArtistaDTO body) {
            var artista = _context.Artistas.Find(id);
            if(artista != null) {
                artista.Nome = body.Nome;
            }
            _context.SaveChanges();
            return Ok(artista);
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete( [FromRoute] int id) {
            var artista = _context.Artistas.Find(id);
            _context.Artistas.Remove(artista);
            _context.SaveChanges();
            return Ok();
        }
    }
}
