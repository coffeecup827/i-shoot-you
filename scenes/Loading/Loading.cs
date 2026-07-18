using Godot;
using System;

public partial class Loading : Control
{
    public override void _EnterTree()
    {
        AudioManager.Initialise(GetTree());
        EventBus.Initialise(GetTree());
        GlobalState.Initialise(GetTree());
    }

    public override void _Ready()
    {
        GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, Scenes.LevelScene);
    }

}
