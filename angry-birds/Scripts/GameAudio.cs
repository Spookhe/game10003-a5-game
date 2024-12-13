using Godot;
using System;

public partial class GameAudio : Node
{
    // Declare an AudioStreamPlayer to handle the audio
    private AudioStreamPlayer _audioPlayer;

    public override void _Ready()
    {
        // Create an AudioStreamPlayer node and add it to the scene
        _audioPlayer = new AudioStreamPlayer();
        AddChild(_audioPlayer);

        // Loads AngryBirds.mp3 into AudioStream2D node
        AudioStream stream = (AudioStream)GD.Load("res://AngryBirds.mp3");
        _audioPlayer.Stream = stream;

        // Plays the audio
        _audioPlayer.Play();
    }
}
