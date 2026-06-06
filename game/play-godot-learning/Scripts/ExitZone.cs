using Godot;

public partial class ExitZone : Area3D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body is Player)
		{
			if (GameManager.Instance.ScrapCollected >= 3)
			{
				GD.Print("YOU WIN!");
			}
			else
			{
				GD.Print("Need more scrap!");
			}
		}
	}
}
