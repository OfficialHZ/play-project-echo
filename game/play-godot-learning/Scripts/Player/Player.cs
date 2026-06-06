using Godot;

public partial class Player : CharacterBody3D
{
	private const float WalkSpeed = 5.0f;
	private const float SprintSpeed = 8.0f;
	private const float JumpVelocity = 7.0f;
	private const float Gravity = 20.0f;
	private const float MouseSensitivity = 0.0025f;
	private RayCast3D _interactRay;
	private Node3D _cameraPivot;
	private Label _interactionLabel;
	private SpotLight3D _flashlight;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		_cameraPivot = GetNode<Node3D>("CameraPivot");

		_interactRay = GetNode<RayCast3D>("CameraPivot/Camera3D/InteractRay");

		_interactionLabel = GetTree().Root.GetNode<Label>("Main/CanvasLayer/InteractionLabel");

		_flashlight = GetNode<SpotLight3D>("CameraPivot/Camera3D/Flashlight");

		_flashlight.Visible = false;
	}

	public override void _Input(InputEvent @event)
	{
		// Release mouse with ESC
		if (@event.IsActionPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

		// Capture mouse with Left Click
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
			{
				Input.MouseMode = Input.MouseModeEnum.Captured;
			}
		}

		// Camera movement
		if (
			@event is InputEventMouseMotion mouseMotion
			&& Input.MouseMode == Input.MouseModeEnum.Captured
		)
		{
			RotateY(-mouseMotion.Relative.X * MouseSensitivity);

			_cameraPivot.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);

			Vector3 rotation = _cameraPivot.Rotation;

			rotation.X = Mathf.Clamp(rotation.X, Mathf.DegToRad(-89), Mathf.DegToRad(89));

			_cameraPivot.Rotation = rotation;
		}

		if (@event.IsActionPressed("interact"))
		{
			if (_interactRay.IsColliding())
			{
				Node collider =
					_interactRay.GetCollider() as Node;

				if (collider is IInteractable interactable)
				{
					interactable.Interact();
				}
			}
		}
		
		if (@event.IsActionPressed("flashlight"))
		{
			_flashlight.Visible =
				!_flashlight.Visible;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Gravity
		if (!IsOnFloor())
		{
			velocity.Y -= Gravity * (float)delta;
		}

		// Jump
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Sprint
		float currentSpeed = WalkSpeed;

		if (Input.IsActionPressed("sprint"))
		{
			currentSpeed = SprintSpeed;
		}

		// Interaction Detection
		if (_interactRay.IsColliding())
		{
			Node collider =
				_interactRay.GetCollider() as Node;

			_interactionLabel.Visible =
				collider is IInteractable;
		}
		else
		{
			_interactionLabel.Visible = false;
		}

		// Movement Input
		Vector2 input = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

		// Move relative to player's rotation
		Vector3 direction = Transform.Basis * new Vector3(input.X, 0, input.Y);

		velocity.X = direction.X * currentSpeed;
		velocity.Z = direction.Z * currentSpeed;

		Velocity = velocity;

		MoveAndSlide();
	}
}
