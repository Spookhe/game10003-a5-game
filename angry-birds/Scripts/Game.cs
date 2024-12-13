using Godot;
using System;

public partial class Game : Node2D
{
    public Label Score;

    // Score variable
    private int _collectedOre = 0;

    //GameAudio.cs plays AngryBirds.mp3
    private GameAudio _gameAudio;

    // Called when the node enters the scene
    public override void _Ready()
    {
        if (Score != null)
        {
            Score.Text = "Ores Collected: " + _collectedOre;
        }

        _gameAudio = new GameAudio();
        AddChild(_gameAudio);
    }

    // Updates the score when an ore is collected
    public void UpdateScore(int points)
    {
        _collectedOre += points;
        if (Score != null)
        {
            Score.Text = "Ores Collected: " + _collectedOre;
        }
    }
}
