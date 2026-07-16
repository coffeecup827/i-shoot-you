using Godot;
using iShootYou;

public partial class Player : Node2D
{
	[Export]
	private int _speed;

	[Export] 
	private Node2D _playerShip;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		var input = Input.GetVector(InputActions.MoveLeft, InputActions.MoveRight, InputActions.MoveUp, InputActions.MoveDown);
		_playerShip.Position += input * _speed * (float)delta;
	}
}
