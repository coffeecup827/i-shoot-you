using Godot;

public partial class EventBus : Node
{
    public static EventBus Instance { get; private set; }

    public static void Initialise(SceneTree rootTree)
    {
        if(Instance  != null) return;

        var node = new Node { Name="EventBus" };
        var eventBus = new EventBus();
        node.AddChild(eventBus);

        rootTree.Root.CallDeferred(Node.MethodName.AddChild, node);

        Instance = eventBus;
    }

    [Signal]
    public delegate void MeteorHitEventHandler(Node2D body);
}