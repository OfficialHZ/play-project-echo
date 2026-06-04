using Godot;

public partial class Interactable : StaticBody3D
{
	public void Interact()
	{
		GD.Print("Interacted with cube!");
	}
}
