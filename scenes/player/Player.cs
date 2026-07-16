using Godot;
using System;

public partial class Player : Node2D
{

	[Export] 
	private Node2D _playerShip;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_playerShip.Rotation += 1 * (float)delta;
	}
}
