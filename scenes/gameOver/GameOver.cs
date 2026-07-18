using Godot;
using iShootYou;
using System;

public partial class GameOver : Control
{
	[Export]
	private Label _lastScore;

    public override void _Ready()
    {
        _lastScore.Text = $"{GlobalState.Instance.Score} Kills";
    }

	public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed(InputActions.Shoot))
        {
            GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, Scenes.LevelScene);
        }
    }
}
