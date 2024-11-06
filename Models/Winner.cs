namespace GameOfDrones.Models
{
	public class Winner
	{
			public int WinnerId { get; set; }
			public string WinnerName { get; set; }
			public DateTime? DatePlayed { get; set; } = DateTime.Now;
	}
}
