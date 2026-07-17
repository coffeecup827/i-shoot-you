using Godot;
using iShootYou;
using System;

public partial class GameOver : Control
{
	public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed(InputActions.Shoot))
        {
            GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, Scenes.LevelScene);
        }
    }
}
