using Godot;
using System;

public partial class Meteor : Area2D
{
	private void _on_body_entered(Node2D body)
	{
		GD.Print("Meteor hit: " + body.Name);
	}
}
