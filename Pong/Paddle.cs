using Eventide;
using Eventide.Components.Colliders;
using Eventide.Input;

namespace Pong;

internal class Paddle : Entity<PaddleConfig>
{
    private const float velocityMultiplier = 1.08f;

    private PaddleConfig _config;
    private float velocity;

    private Ball _ball;

    public override void OnConfigure (PaddleConfig configuration)
    {
        _config = configuration;
    }

    protected override void OnInitialize ()
    {
        velocity = 150f;
        AddComponent<BoxCollider> ();
        Transform.Size = new (8f, 200f);
        Transform.Position = new ((Window.Width + _config.XOffset) % Window.Width, Window.Height / 2);
    }

    protected override void OnStart ()
    {
        _ball = GetEntity<Ball> ();
    }

    protected override void OnUpdate ()
    {
        if (_config.IsPlayer)
        {
            Vector2 direction = new (0f, (Input.IsKeyDown (_config.UpKey) ? -1f : 0f) + (Input.IsKeyDown (_config.DownKey) ? 1f : 0f));
            Transform.Move (direction * velocity);
        }
        else if (_ball.Transform.Position.X >= Window.Width / 2 && _ball.Direction.X > 0f)
        {
            Vector2 direciton = new (0f, EventideMath.Clamp (_ball.Transform.Position.Y - Transform.Position.Y, -1f, 1f));
            Transform.Move (direciton * velocity);
        }
        Transform.Position = Vector2.Clamp (Transform.Position, new (Transform.Position.X, 0f), new (Transform.Position.X, Window.Height));
    }

    protected override void OnDraw ()
    {
        Window.DrawRectangle (Transform.Size, Transform.Position, _config.Color);
    }

    protected override void OnCollision (Entity entity, ICollider collider)
    {
        velocity *= velocityMultiplier;
    }
}

internal struct PaddleConfig
{
    internal float XOffset;
    internal Color Color;
    internal Key UpKey;
    internal Key DownKey;
    internal bool IsPlayer;
}
