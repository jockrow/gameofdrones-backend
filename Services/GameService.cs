using GameOfDrones.Models;
using GameOfDrones.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GameOfDrones.Services
{
	public class GameService
	{
		private readonly GameContext _context;

		public GameService(GameContext context)
		{
				_context = context;
		}

		public async Task<List<Move>> GetMovesAsync()
		{
			try
			{
				return await _context.Moves.ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching moves: {ex.Message}");
				return null;
			}
		}

		public async Task<Move> GetMoveByIdAsync(int id)
		{
			try
			{
				return await _context.Moves.FindAsync(id);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching move by id: {ex.Message}");
				return null;
			}
		}

		public async Task<Move> AddMoveAsync(Move move)
		{
			try
			{
				_context.Moves.Add(move);
				await _context.SaveChangesAsync();
				return move;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding move: {ex.Message}");
				return null;
			}
		}

		public async Task<Move> UpdateMoveAsync(int id, Move move)
		{
			try
			{
				var existingMove = await _context.Moves.FindAsync(id);
				if (existingMove == null) return null;

				existingMove.Name = move.Name;
				existingMove.Kills = move.Kills;

				await _context.SaveChangesAsync();
				return existingMove;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating move: {ex.Message}");
				return null;
			}
		}

		public async Task<bool> DeleteMoveAsync(int id)
		{
			try
			{
				var move = await _context.Moves.FindAsync(id);
				if (move == null) return false;

				_context.Moves.Remove(move);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting move: {ex.Message}");
				return false;
			}
		}

		public async Task<Winner> AddWinnerAsync(string winnerName)
		{
			try
			{
				var winner = new Winner
				{
					WinnerName = winnerName
				};
				_context.Winners.Add(winner);
				await _context.SaveChangesAsync();
				return winner;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding winner: {ex.Message}");
				return null;
			}
		}

		public async Task<List<Winner>> GetWinnersAsync()
		{
			try
			{
				return await _context.Winners.ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching winners: {ex.Message}");
				return null;
			}
		}
	}
}
