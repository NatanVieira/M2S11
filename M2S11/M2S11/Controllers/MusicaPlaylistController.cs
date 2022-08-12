using M2S11.Data;
using M2S11.DTOs;
using M2S11.Models;
using Microsoft.AspNetCore.Mvc;

namespace M2S11.Controllers {
    [ApiController]
    [Route("api/musicasplaylists")]
    public class MusicaPlaylistController : ControllerBase {

        private readonly M2S11DbContext _context;

        public MusicaPlaylistController(M2S11DbContext context) {
            _context = context;
        }

        [HttpPost]
        public ActionResult<MusicaPlaylist> Post([FromBody] MusicaPlaylistDTO body) {
            MusicaPlaylist musicaPlaylist = new() {
                MusicaId = body.MusicaId,
                PlaylistId = body.PlaylistId,
                Musica = _context.Musicas.Find(body.MusicaId),
                Playlist = _context.Playlists.Find(body.PlaylistId),
            };
            _context.MusicasPlaylists.Add(musicaPlaylist);
            _context.SaveChanges();
            return Created("api/musicasplaylists",musicaPlaylist);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) {
            var musicaplaylist = _context.MusicasPlaylists.Find(id);
            _context.MusicasPlaylists.Remove(musicaplaylist);
            _context.SaveChanges();
            return Ok();
        }
    }
}
