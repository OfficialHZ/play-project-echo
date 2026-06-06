using Godot;

public partial class Scrap : StaticBody3D, IInteractable
{
	public void Interact()
	{
		GameManager.Instance.AddScrap();

		QueueFree();
	}
}
