using Godot;
using System;

public partial class Level : Node2D
{

	[Export]
	private Node2D _meteors;
	[Export]
	private Node2D _lasers;
	PackedScene meteorScene = GD.Load<PackedScene>(Scenes.MeteorScene);
	PackedScene laserScene = GD.Load<PackedScene>(Scenes.LaserScene);
    private void OnMeteorTimerTimeout()
	{
		var meteor = meteorScene.Instantiate<Meteor>();

		_meteors.AddChild(meteor);
	}

	private void OnPlayerShoot(Vector2 position)
	{
		GD.Print($"Player shoot at position: {position}");
		

	}
}
