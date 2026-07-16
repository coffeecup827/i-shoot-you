using Godot;
using System;

public partial class Level : Node2D
{

	[Export]
	private Node2D _meteors;

    private void OnMeteorTimerTimeout()
	{
		var meteorScene = GD.Load<PackedScene>(Scenes.MeteorScene);
		var meteor = meteorScene.Instantiate<Meteor>();

		_meteors.AddChild(meteor);
	}
}
