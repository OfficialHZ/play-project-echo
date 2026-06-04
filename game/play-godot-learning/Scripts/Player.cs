using Godot;

public partial class Player : CharacterBody3D
{
	private const float Gravity = 20.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y -= Gravity * (float)delta;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
