using Eventide;
using Eventide.Components.Colliders;
using Eventide.Content;
using Eventide.Input;

namespace Pong;

internal class Ball : Entity
{
    private const float radius = 8f;
    private const float startingVelocity = 150f;
    private const float velocityMultiplier = 1.1f;

    public Vector2 Direction;

    private Random _random;
    private BoxCollider _collider;

    private float _velocity;
    private bool _moving;

    private Sound _hitSound;
    private Sound _scoreSound;

    private UI _ui;

    protected override void OnInitialize ()
    {
        _random = new ();
        _collider = AddComponent<BoxCollider> ();

        _hitSound = Content.LoadSound (@"Sounds\BallHit");
        _scoreSound = Content.LoadSound (@"Sounds\BallScore");

        Reset ();
    }

    protected override void OnStart ()
    {
        _ui = GetEntity<UI> ();
    }

    protected override void OnUpdate ()
    {
        if (!_moving)
        {
            _moving = Input.IsKeyPressed (Key.Space);
            return;
        }

        Transform.Move (_velocity * Direction);

        if (_collider.Bounds.Top <= 0 || _collider.Bounds.Bottom >= Window.Height)
        {
            Hit ();
        }

        if (_collider.Bounds.Right <= 0)
        {
            Score (1);
        }
        else if (_collider.Bounds.Left >= Window.Width)
        {
            Score (0);
        }
    }

    protected override void OnDraw ()
    {
        Window.DrawCircle (Transform.Position, radius, Color.White);
    }

    protected override void OnCollision (Entity entity, ICollider collider)
    {
        Direction.X *= -1f;
        _velocity *= velocityMultiplier;
        _hitSound.Play ();
    }

    private void Reset ()
    {
        Transform.Position = Window.Center;
        _velocity = startingVelocity;
        Direction = RandomDireciton ();
        _moving = false;
    }

    private Vector2 RandomDireciton () => _random.Next (4) switch
    {
        0 => Vector2.Opposite,
        1 => Vector2.One,
        2 => -Vector2.Opposite,
        _ => -Vector2.One
    };

    private void Hit ()
    {
        Direction.Y *= -1f;
        _hitSound.Play ();
    }

    private void Score (int index)
    {
        _scoreSound.Play ();
        Reset ();
        _ui.IncreaseScore (index);
    }
}
