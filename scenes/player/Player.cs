using Godot;
using iShootYou;

public partial class Player : CharacterBody2D
{
	[Export]
	private int _speed;

	[Export]
	private Sprite2D _playerShip;

	[Signal]
	public delegate void PlayerShootEventHandler(Vector2 position);

	public override void _Process(double delta)
	{

		var input = Input.GetVector(InputActions.MoveLeft, InputActions.MoveRight, InputActions.MoveUp, InputActions.MoveDown);
		
		Velocity = input * _speed;
		MoveAndSlide();

		if (Input.IsActionPressed(InputActions.Shoot))
		{
			EmitSignal(SignalName.PlayerShoot, Position);
		}
	}
}
