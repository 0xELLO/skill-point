#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Public.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Game RESTful service
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly ILogger<GameController> _logger;
        private readonly WebAutoMapper _webAutoMapper;

        public GameController(IAppBll bll, ILogger<GameController> logger, WebAutoMapper webAutoMapper)
        {
            _bll = bll;
            _logger = logger;
            _webAutoMapper = webAutoMapper;
        }

        /// <summary>
        /// Gets all games
        /// </summary>
        /// <returns>All games as GameDTO</returns>
        // GET: api/Game
        [HttpGet]
        public async Task<IEnumerable<GameDTO>> GetGame()
        {
            var bllEntity = (await _bll.Games.GetAllAsync());
            var res = bllEntity.Select(bll => _webAutoMapper.GameMapper.Map(bll));    
            return res;
        }
        
        /// <summary>
        /// Gets one game based on its id
        /// </summary>
        /// <param name="id">Game id willing to get</param>
        /// <returns>Found Game</returns>
        // GET: api/Game/5
        [Produces( "application/json" )]    
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGame(Guid id)
        {
            var game = await _bll.Games.FirstOrDefaultAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return new GameDTO
            {
                Id = game.Id,
                Title = game.Title.Translate()!,
                ShortDescription = game.ShortDescription.Translate()!,
                LongDescription = game.LongDescription.Translate()!,
                LogoUrl = game.LogoUrl
            };
        }
        /// <summary>
        /// Modifies Game
        /// </summary>
        /// <param name="id">Game id willing to modify</param>
        /// <param name="game">New representation of game</param>
        /// <returns>Modified game</returns>
        // PUT: api/Game/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(Guid id, App.Bll.DTO.Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }
            
            try
            {
                _bll.Games.Update(game);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound("something");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Creates game
        /// </summary>
        /// <param name="game">Game object willing to add</param>
        /// <returns>Created game</returns>
        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<GameDTO>> PostGame(App.Bll.DTO.Game game)
        {
            game.Id = new Guid();
            _bll.Games.Add(game);
            await _bll.SaveChangesAsync();
            return new GameDTO
            {
                Id = game.Id,
                Title = game.Title,
                ShortDescription = game.ShortDescription,
                LongDescription = game.LongDescription,
                LogoUrl = game.LogoUrl
            };
        }
        /// <summary>
        /// Deletes game
        /// </summary>
        /// <param name="id">Game id willing to delete</param>
        /// <returns>Status Code</returns>
        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await _bll.Games.FirstOrDefaultAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _bll.Games.Remove(game);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(Guid id)
        {
            return _bll.Games.Exists(id);
        }
    }
}
