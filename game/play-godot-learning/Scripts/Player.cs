using Godot;

public partial class Player : CharacterBody3D
{
	private const float Speed = 5.0f;
	private const float Gravity = 20.0f;
	private const float MouseSensitivity = 0.0025f;

	private Node3D _cameraPivot;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		_cameraPivot = GetNode<Node3D>("CameraPivot");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

		if (@event is InputEventMouseMotion mouseMotion)
		{
			RotateY(-mouseMotion.Relative.X * MouseSensitivity);

			_cameraPivot.RotateX(
				-mouseMotion.Relative.Y * MouseSensitivity
			);

			Vector3 rotation = _cameraPivot.Rotation;

			rotation.X = Mathf.Clamp(
				rotation.X,
				Mathf.DegToRad(-89),
				Mathf.DegToRad(89)
			);

			_cameraPivot.Rotation = rotation;
		}
	}

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

		Vector3 direction =
			Transform.Basis *
			new Vector3(input.X, 0, input.Y);

		velocity.X = direction.X * Speed;
		velocity.Z = direction.Z * Speed;

		Velocity = velocity;

		MoveAndSlide();
	}
}
