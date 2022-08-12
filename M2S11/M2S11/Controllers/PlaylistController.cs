using M2S11.Data;
using M2S11.DTOs;
using M2S11.Models;
using Microsoft.AspNetCore.Mvc;

namespace M2S11.Controllers {
    [ApiController]
    [Route("api/playlists")]
    public class PlaylistController : ControllerBase {

        private readonly M2S11DbContext _context;

        public PlaylistController(M2S11DbContext context) {
            _context = context;
        }

        //GET
        [HttpGet]
        public ActionResult<List<Playlist>> Get([FromQuery] string nomeFiltro) {
            var query = _context.Playlists.AsQueryable();
            if(!string.IsNullOrEmpty(nomeFiltro)) {
                query = query.Where(p => p.Nome == nomeFiltro);
            }
            return Ok(query.ToList());
        }

        //GET com ID
        [HttpGet("{id}")]
        public ActionResult<Playlist> GetById([FromRoute] int id) {
            var playlist = _context.Playlists.Find(id);
            return Ok(playlist);
        }

        //Post
        [HttpPost]
        public ActionResult<Playlist> Post([FromBody] PlaylistDTO body) {
            Playlist playlist = new(){
                Nome = body.Nome,
            };
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
            return Created("api/playlists",playlist);
        }

        //Put
        [HttpPut("{id}")]
        public ActionResult<Playlist> Put([FromRoute] int id,
                                          [FromBody] PlaylistDTO body) {
            var playlist = _context.Playlists.Find(id);
            if(playlist != null) {
                playlist.Nome = body.Nome;
            }
            _context.SaveChanges();
            return Ok(playlist);
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) {
            var playlist = _context.Playlists.Find(id);
            _context.Playlists.Remove(playlist);
            _context.SaveChanges();
            return Ok();
        }
    }
}
