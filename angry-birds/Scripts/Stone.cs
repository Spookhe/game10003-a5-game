using Godot;
using System;

public partial class Stone : Area2D
{
    public Sprite2D StoneSprite;
    public CollisionShape2D CollisionShape;

    public override void _Ready()
    {
        //Initializes Collision and Sprites
        if (CollisionShape == null)
        {
            CollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        }

        if (StoneSprite == null)
        {
            StoneSprite = GetNode<Sprite2D>("Sprite2D");
        }

        Connect("body_entered", new Callable(this, "_OnBodyEntered"));
    }
    private void _OnBodyEntered(Node body)
    {
        // Check if stone collides with pickaxe
        if (body is Pickaxe)
        {
            GD.Print("Debug: Stone is Hit");
            QueueFree(); // Removes stone from scene
        }
    }
}
