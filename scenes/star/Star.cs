using Godot;
using System;

public partial class Star : Node2D
{
    [Export]
    private AnimatedSprite2D _animatedStar;

    public override void _Ready()
    {
        Position = new Vector2((float)GD.RandRange(0, GetViewportRect().Size.X), (float)GD.RandRange(0, GetViewportRect().Size.Y));

        _animatedStar.AnimationFinished += () => QueueFree();
        _animatedStar.Play("default");
    }

}
