using Godot;
using System;

public partial class HeadsUpDisplay : Node2D
{
    [Export]
    private Label _scoreLabel;
    [Export]
    private int _hitpoints;
    [Export]
    private HBoxContainer _hitpointsBlock;

    private int _score;
    
    private EventBus eventBus;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>(Paths.eventBusPath);
        _hitpointsBlock.LayoutDirection = Control.LayoutDirectionEnum.Ltr;
        _hitpointsBlock.AddThemeConstantOverride(GodotProperty.separation, 20);

        SetScore(0);
        AddHitpoints(_hitpoints);

        eventBus.MeteorHit += OnMeteorHit;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        eventBus.MeteorHit -= OnMeteorHit;
    }

    private void OnMeteorHit(Node2D body)
    {
        if (body is Player)
        {
            _hitpoints--;
            _hitpointsBlock.GetChild<TextureRect>(_hitpoints).QueueFree();
        }
        else if (body is Laser)
        {
            SetScore(_score + 1);
        }
    }

    private void AddHitpoints(int hitpoints)
    {
        var heartTexture = GD.Load<Texture2D>(Paths.playerShipPath);
        var heartScale = new Vector2(0.05f, 0.05f);
        var heartSize = heartTexture.GetSize() * heartScale;

        for (int i = 0; i < hitpoints; i++)
        {
            var heart = new TextureRect
            {
                Texture = heartTexture,
                ExpandMode = TextureRect.ExpandModeEnum.FitWidth,
                StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
                CustomMinimumSize = heartSize,
                Size = heartSize
            };

            _hitpointsBlock.AddChild(heart);

            Tween tween = CreateTween();
            tween.TweenProperty(heart, GodotProperty.position, new Vector2(i * heartSize.X, 0f), (i + 1) * 0.2f).SetEase(Tween.EaseType.In);
        }
    }


    private void SetScore(int v)
    {
        _score = v;
        _scoreLabel.Text = $"{_score} KILLS";
    }

}
