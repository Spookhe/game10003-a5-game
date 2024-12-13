using Godot;
using System;

public partial class Ore : StaticBody2D
{
    [Export] public Sprite2D GoldOre;
    [Export] public Sprite2D EmeraldOre;
    [Export] public Sprite2D DiamondOre;

    private int _score = 0;

    // Collecting ore increases score points
    public void CollectOre(string oreType)
    {
        if (oreType == "Gold")
        {
            _score += 100;  // Gold = 100 score
            GD.Print("Gold collected! Score: " + _score);
        }
        else if (oreType == "Emerald")
        {
            _score += 300;  // Emerald = 300 score
            GD.Print("Emerald collected! Score: " + _score);
        }
        else if (oreType == "Diamond")
        {
            _score += 500;  // Diamond = 500 score
            GD.Print("Diamond collected! Score: " + _score);
        }
    }
}
