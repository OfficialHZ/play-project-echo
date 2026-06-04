using Godot;

public partial class Interactable : StaticBody3D, IInteractable
{
	public void Interact()
	{
		GD.Print("Interacted with cube!");
	}
}
