using Godot;
using System;

public partial class Meteor : Area2D
{

	[Export]
	private float meteorDefaultSpeed;

	[Export]
	private Sprite2D _meteorSprite;

	int meteorXDirection = 0;
	float meteorSpeed = 0;
	float meteorSpeedMultiplier = (float)GD.RandRange(1f, 3f);

	public override void _Ready()
	{
		_meteorSprite.Texture = GD.Load<Texture2D>(Paths.GetMeteorsPath(Random.Shared.Next(1, 24)));

		Position = new Vector2(GD.Randf() * GetViewportRect().Size.X, Random.Shared.Next(-150, -50));
		meteorXDirection = Random.Shared.Next(-1, 1);
		meteorSpeed = meteorDefaultSpeed * meteorSpeedMultiplier;
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(meteorXDirection, 1) * (float)delta * meteorSpeed;
		RotationDegrees += (float)delta * 100;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			AudioManager.Instance.PlaySfx(AudioCue.PlayerHit);
			EventBus.Instance.EmitSignal(EventBus.SignalName.MeteorHit, body);
			QueueFree();
		}
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is Laser)
		{
			AudioManager.Instance.PlayRandomExplosionSound();
			EventBus.Instance.EmitSignal(EventBus.SignalName.MeteorHit, area);
			area.QueueFree();
			QueueFree();
		}
	}
}
