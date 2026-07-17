using Godot;
using iShootYou;

public partial class Player : CharacterBody2D
{
	[Export]
	private int _speed;

	[Export]
	private Sprite2D _playerShip;

	[Export]
	private Marker2D _laserSpawnPoint;

	[Signal]
	public delegate void PlayerShootEventHandler(Vector2 position);

	private bool canShoot = true;

	public override void _Process(double delta)
	{

		var input = Input.GetVector(InputActions.MoveLeft, InputActions.MoveRight, InputActions.MoveUp, InputActions.MoveDown);
		
		Velocity = input * _speed;
		MoveAndSlide();

		if (Input.IsActionPressed(InputActions.Shoot) && canShoot)
		{
			EmitSignal(SignalName.PlayerShoot, _laserSpawnPoint.GlobalPosition);
			canShoot = false;
		}
	}

	private void OnLaserTimerTimeout()
	{
		canShoot = true;
	}
}
