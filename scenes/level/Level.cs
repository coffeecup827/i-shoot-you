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
	[Export]
	private VirtualJoystick _virtualJoystick;

	PackedScene meteorScene = GD.Load<PackedScene>(Scenes.MeteorScene);
	PackedScene laserScene = GD.Load<PackedScene>(Scenes.LaserScene);
	PackedScene starScene = GD.Load<PackedScene>(Scenes.StarScene);

    public override void _Ready()
    {
        _virtualJoystick.Position = new Vector2(GetViewportRect().Size.X/6, GetViewportRect().Size.Y/2);
    }


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
