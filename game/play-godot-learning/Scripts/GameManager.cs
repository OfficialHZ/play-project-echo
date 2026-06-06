using Godot;

public partial class GameManager : Node
{
	public static GameManager Instance;

	public int ScrapCollected = 0;

	private Label _scrapCounter;

	public override void _Ready()
	{
		Instance = this;

		_scrapCounter =
			GetNode<Label>(
                "../CanvasLayer/ScrapCounter"
			);

		UpdateUI();
	}

	public void AddScrap()
	{
		ScrapCollected++;

		UpdateUI();
	}

	private void UpdateUI()
	{
		_scrapCounter.Text =
			$"Scrap: {ScrapCollected}";
	}
}
