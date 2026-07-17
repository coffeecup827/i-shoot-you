using Godot;
using System;

public partial class Level : Node2D
{

	[Export]
	private Node2D _meteors;
	[Export]
	private Node2D _lasers;
	[Export]
	private Node2D _stars;

	PackedScene meteorScene = GD.Load<PackedScene>(Scenes.MeteorScene);
	PackedScene laserScene = GD.Load<PackedScene>(Scenes.LaserScene);
	PackedScene starScene = GD.Load<PackedScene>(Scenes.StarScene);
    private void OnMeteorTimerTimeout()
	{
		var meteor = meteorScene.Instantiate<Meteor>();

		_meteors.AddChild(meteor);
	}

	private void OnPlayerShoot(Vector2 position)
	{
		var laser = laserScene.Instantiate<Laser>();
		laser.Position = position;

		_lasers.AddChild(laser);
	}

	private void OnStarTimerTimeout()
	{
		var star = starScene.Instantiate<Star>();
		
		_stars.AddChild(star);
	}
}
