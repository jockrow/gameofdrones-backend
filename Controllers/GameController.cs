using Microsoft.AspNetCore.Mvc;
using GameOfDrones.Models;
using GameOfDrones.Services;

namespace GameOfDronesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("dbpath")]
        public string GetDbPath()
        {
            var dbPath = Environment.GetEnvironmentVariable("DB_PATH");
            Console.WriteLine($"DB_PATH environment variable: {dbPath}");
            return dbPath;
        }

        [HttpGet("moves")]
        public async Task<ActionResult<List<Move>>> GetMoves()
        {
            var moves = await _gameService.GetMovesAsync();
            if (moves == null || moves.Count == 0)
            {
                return NotFound("Moves not found");
            }
            return Ok(moves);
        }

        [HttpGet("move/{id}")]
        public async Task<ActionResult<Move>> GetMoveById(int id)
        {
            var move = await _gameService.GetMoveByIdAsync(id);
            if (move == null)
            {
                return NotFound($"Move with ID {id} not found");
            }
            return Ok(move);
        }

        [HttpPost("move")]
        public async Task<ActionResult<Move>> AddMove(Move move)
        {
            var createdMove = await _gameService.AddMoveAsync(move);
            if (createdMove == null)
            {
                return BadRequest("Failed to create move");
            }
            return Created("api/game/move", createdMove);
        }

        [HttpPut("move/{id}")]
        public async Task<ActionResult<Move>> UpdateMove(int id, Move move)
        {
            var updatedMove = await _gameService.UpdateMoveAsync(id, move);
            if (updatedMove == null)
            {
                return NotFound($"Move with ID {id} not found or could not be updated");
            }
            return Ok(updatedMove);
        }

        [HttpDelete("move/{id}")]
        public async Task<IActionResult> DeleteMove(int id)
        {
            var success = await _gameService.DeleteMoveAsync(id);
            if (!success)
            {
                return NotFound($"Move with ID {id} not found or could not be deleted");
            }
            return Ok(success);
        }

        [HttpPost("winner")]
        public async Task<ActionResult<Winner>> AddWinner([FromBody] Winner request)
        {
            var winner = await _gameService.AddWinnerAsync(request.WinnerName);
            if (winner == null)
            {
                return BadRequest("Error registering the Winner");
            }
            return Created("api/game/winner", winner);
        }

        [HttpGet("winners")]
        public async Task<ActionResult<List<Winner>>> GetWinners()
        {
            var winners = await _gameService.GetWinnersAsync();
            if (winners == null || winners.Count == 0)
            {
                return NotFound("Winners not found");
            }
            return Ok(winners);
        }
    }
}
