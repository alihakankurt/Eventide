using Eventide.Components.Colliders;

namespace Eventide;

/// <summary>
/// Base class for your game.
/// </summary>
public abstract class Game : ObjectBase
{
    #region Properties

    /// <summary>
    /// Gets the current window that attached to the <see cref="Game"/>.
    /// </summary>
    public Window Window { get; }

    /// <summary>
    /// Gets the current world that attached to the <see cref="Game"/>.
    /// </summary>
    public World World { get; }

    /// <summary>
    /// Gets the current camera that attached to the <see cref="World"/>.
    /// </summary>
    public Camera Camera => World.Camera;

    /// <summary>
    /// Gets the input manager that attached to the <see cref="Game"/>.
    /// </summary>
    public InputManager Input { get; }

    /// <summary>
    /// Gets the content manager that attached to the <see cref="Game"/>.
    /// </summary>
    public ContentManager Content { get; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="Game"/>.
    /// </summary>
    public Game ()
    {
        Input = new ();
        Content = new ("Content");
        Window = new (this);
        World = new (this);
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy (bool disposing)
    {
        OnDestroy ();
        Input.Dispose ();
        World.Dispose ();
        Content.Dispose ();
        Window.Dispose ();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Runs the game.
    /// </summary>
    public void Run ()
    {
        Content.LoadContent ();
        World.Initialize ();
        OnInitialize ();

        Time.Start ();
        while (Window.Running)
        {
            // Calculate delta time
            Time.Restart ();

            // Dispatch events
            Window.DispatchEvents ();

            // Update objects
            Input.Update ();
            World.Update ();
            BoxCollider.Update ();
            OnUpdate ();
            
            // Draw objects
            Window.Clear ();
            World.Draw ();
            OnDraw ();
            Window.Display ();
        }

        Dispose ();
    }

    /// <summary>
    /// Calls just after game initialized.
    /// </summary>
    public virtual void OnInitialize ()
    {
    }

    /// <summary>
    /// Calls every frame before drawing.
    /// </summary>
    public virtual void OnUpdate ()
    {
    }

    /// <summary>
    /// Calls every frame after update.
    /// </summary>
    public virtual void OnDraw ()
    {
    }

    /// <summary>
    /// Calls just before game closes. Game is disposing window, world, entities, input and content by default. So dispose your own generated class before program closes to prevent memory leak.
    /// </summary>
    public virtual void OnDestroy ()
    {
    }

    #endregion
}
