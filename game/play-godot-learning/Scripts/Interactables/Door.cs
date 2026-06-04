using Godot;

public partial class Door : StaticBody3D, IInteractable
{
    private bool _isOpen = false;

    public void Interact()
    {
        if (_isOpen)
        {
            RotationDegrees -= new Vector3(0, 90, 0);
        }
        else
        {
            RotationDegrees += new Vector3(0, 90, 0);
        }

        _isOpen = !_isOpen;
    }
}