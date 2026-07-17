using Godot;
using iShootYou;
using System;

public partial class GameOver : Control
{
	[Export]
	private Label _lastScore;

    public override void _Ready()
    {
        Global global = GetNode<Global>(Paths.globalPath);
        _lastScore.Text = $"{global.Score} Kills";
    }

	public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed(InputActions.Shoot))
        {
            GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, Scenes.LevelScene);
        }
    }
}
