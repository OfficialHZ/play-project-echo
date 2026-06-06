using Godot;

public partial class GameManager : Node
{
	public static GameManager Instance;

	public int ScrapCollected = 0;

	public override void _Ready()
	{
		Instance = this;
	}

	public void AddScrap()
	{
		ScrapCollected++;

		GD.Print($"Scrap: {ScrapCollected}");
	}
}
