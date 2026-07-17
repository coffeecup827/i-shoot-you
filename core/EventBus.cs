using Godot;

public partial class EventBus : Node
{
    [Signal]
    public delegate void MeteorHitEventHandler(Node2D body);
}