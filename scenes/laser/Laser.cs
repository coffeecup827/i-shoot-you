using Godot;
using System;

public partial class Laser : Area2D
{
    [Export]
    private int laserSpeed;

    [Export]
    private Sprite2D _laserSprite;

    public override void _Ready()
    {
        CreateTween().TweenProperty(_laserSprite, "scale", new Vector2(0.1f, 0.2f), 0.15f).From(new Vector2(0f, 0f));
    }

    public override void _Process(double delta)
    {
        Position += new Vector2(0, -1) * (float)delta * laserSpeed;
    }
}
