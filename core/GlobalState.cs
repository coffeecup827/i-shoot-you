using Godot;
using System;

public partial class GlobalState : Node
{
	public static GlobalState Instance { get; private set; }

    public static void Initialise(SceneTree rootTree)
    {
        if(Instance  != null) return;

        var node = new Node { Name="Global" };
        var global = new GlobalState();
        node.AddChild(global);

        rootTree.Root.CallDeferred(Node.MethodName.AddChild, node);

        Instance = global;
    }

	public int Score { get; set; } = 0;
}
