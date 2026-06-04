using Godot;

public partial class Player : CharacterBody3D
{
	private const float Speed = 5.0f;
	private const float Gravity = 20.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y -= Gravity * (float)delta;
		}

		Vector2 input = Input.GetVector(
			"move_left",
			"move_right",
			"move_forward",
            "move_backward"
		);

		Vector3 direction = new Vector3(
			input.X,
			0,
			input.Y
		);

		velocity.X = direction.X * Speed;
		velocity.Z = direction.Z * Speed;

		Velocity = velocity;
		MoveAndSlide();
	}
}
