using Godot;
using System;

public partial class Laser : Area2D
{
    [Export]
    private int laserSpeed;
    public override void _Process(double delta)
    {
        Position += new Vector2(0, -1) * (float)delta * laserSpeed;
    }
}
