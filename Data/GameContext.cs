using Microsoft.EntityFrameworkCore;
using GameOfDrones.Models;

namespace GameOfDrones.Data
{
	public class GameContext : DbContext
	{
			public GameContext(DbContextOptions<GameContext> options) : base(options) {}

			public DbSet<Move> Moves { get; set; }
			public DbSet<Winner> Winners { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				 modelBuilder.Entity<Winner>(entity =>
					{
							entity.HasKey(e => e.WinnerId);
							entity.Property(e => e.DatePlayed)
										.HasDefaultValueSql("GETDATE()");
					});
			}
	}
}
