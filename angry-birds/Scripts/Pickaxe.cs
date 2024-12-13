using Godot;
using System;

public partial class Pickaxe : Node2D
{
    //Initialize Rigidbody, Collision and Sprite
    public Sprite2D PickaxeSprite;
    public CollisionShape2D CollisionShape;
    public RigidBody2D RigidBody;

    private Vector2 _startDragPosition;
    private Vector2 _currentDragPosition;
    private bool _isDragging = false;
    private Vector2 _initialPosition = new Vector2(0, 194); // Respawn position for pickaxe
    private float _launchSpeed = 1500f; // Launch speed for pickaxe
    private Vector2 _launchVelocity; // Velocity when pickaxe is launched
    private bool _isLaunched = false;

    private float respawnTime = 1.0f; // Time the pickaxe respawns
    private Timer respawnTimer;
    private Rect2 extendedVisibleBounds;

    public override void _Ready()
    {
        if (RigidBody == null)
        {
            RigidBody = GetNode<RigidBody2D>("RigidBody2D");
        }

        if (PickaxeSprite == null)
        {
            PickaxeSprite = RigidBody.GetNode<Sprite2D>("Sprite2D");
        }

        if (CollisionShape == null)
        {
            CollisionShape = RigidBody.GetNode<CollisionShape2D>("CollisionShape2D");
        }

        // Respawn pickaxe setup
        respawnTimer = new Timer();
        AddChild(respawnTimer);
        respawnTimer.WaitTime = respawnTime;
        respawnTimer.OneShot = true;
        respawnTimer.Timeout += RespawnPickaxe;

        // Increases scene padding so the pickaxe is not despawning out of bounds
        Rect2 viewportRect = GetViewport().GetVisibleRect();
        extendedVisibleBounds = new Rect2(
            viewportRect.Position - new Vector2(400, 100),
            viewportRect.Size + new Vector2(400, 200)
        );
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (mouseEvent.Pressed)
                {
                    // Start dragging if pickaxe is clicked on
                    if (PickaxeSprite.GetRect().HasPoint(PickaxeSprite.ToLocal(GetGlobalMousePosition())))
                    {
                        _startDragPosition = GetGlobalMousePosition();
                        _isDragging = true;
                    }
                }
                else if (_isDragging)
                {
                    // Launch the pickaxe when the mouse is released
                    LaunchPickaxe();
                    _isDragging = false;
                }
            }
        }
    }

    public override void _Process(double delta)
    {
        if (_isDragging)
        {
            // Position of the pickaxe moves from mouse position
            _currentDragPosition = GetGlobalMousePosition();
            Vector2 dragVector = _currentDragPosition - _startDragPosition;
            RigidBody.Position = _startDragPosition + dragVector;
        }

        if (_isLaunched)
        {
            //Checks pickaxe boundries
            RigidBody.Position += _launchVelocity * (float)delta;
            Rect2 pickaxeBounds = PickaxeSprite.GetRect();
            Rect2 viewportRect = GetViewport().GetVisibleRect();
            if (!extendedVisibleBounds.HasPoint(RigidBody.Position + new Vector2(pickaxeBounds.Size.X / 2, pickaxeBounds.Size.Y / 2)))
            {
                StartRespawn();
            }
        }
    }

    private void LaunchPickaxe()
    {
        Vector2 direction = _startDragPosition - _currentDragPosition;
        direction = direction.Normalized();
        _launchVelocity = direction * _launchSpeed; //Elastic release effect

        // Checks if pickaxe is launched by player
        _isLaunched = true;
    }

    private void StartRespawn()
    {
        // Respawn pickaxe
        RigidBody.Position = new Vector2(-1000, -1000); // Moves pickaxe off-screen when out of bounds
        _isLaunched = false;
        respawnTimer.Start();
    }

    private void RespawnPickaxe()
    {
        // Moves pickaxe to its idle position
        RigidBody.Position = _initialPosition;
        _launchVelocity = Vector2.Zero;
        _isLaunched = false;
        GD.Print("Debug: Pickaxe respawned!");
    }
}
