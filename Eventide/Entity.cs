using Eventide.Components.Colliders;

namespace Eventide;

/// <summary>
/// Base class for all entity objects.
/// </summary>
public abstract class Entity : ObjectBase
{
    #region Fields

    // A readonly collection that holds the components of entity.
    private readonly List<IComponent> _components;

    // The world that entity exits in.
    private World? _world;

    // The game that worlds attached to.
    private Game? _game;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets name of entity.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets draw layer of entity.
    /// </summary>
    public int Layer { get; set; }

    /// <summary>
    /// Gets or sets the wheter entity is not effected by <see cref="Camera.UpdateArea"/>.
    /// </summary>
    public bool UpdateAlways { get; set; }

    /// <summary>
    /// Gets the transform component of entity.
    /// </summary>
    public Transform Transform { get; }

    /// <summary>
    /// Gets the world that entity exists in.
    /// </summary>
    public World? World => _game?.World;

    /// <summary>
    /// Gets the game that entity attached to.
    /// </summary>
    public Game? Game => _game;

    /// <summary>
    /// Gets the window that attached to <see cref="Game"/>.
    /// </summary>
    public Window? Window => _game?.Window;

    /// <summary>
    /// Gets the input manager that attached to <see cref="Game"/>.
    /// </summary>
    public InputManager? Input => _game?.Input;

    /// <summary>
    /// Gets the content manager that attached to <see cref="Game"/>.
    /// </summary>
    public ContentManager? Content => _game?.Content;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of <see cref="Entity"/>.
    /// </summary>
    public Entity ()
    {
        _components = new List<IComponent> ();

        Name = null;
        Layer = 0;
        UpdateAlways = false;
        Transform = AddComponent<Transform> ()!;
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy (bool disposing)
    {
        OnDestroy ();
        for (int i = 0; i < _components.Count; i++)
        {
            _components[i].Dispose ();
        }
        _components.Clear ();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns component of type <typeparamref name="TComponent"/> that attached to the <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="TComponent">A component type that implements <see cref="IComponent"/>.</typeparam>
    /// <returns><typeparamref name="TComponent"/></returns>
    public TComponent? GetComponent<TComponent> () where TComponent : class, IComponent
    {
        return _components.FirstOrDefault (c => c.GetType () == typeof (TComponent)) as TComponent;
    }

    /// <summary>
    /// Attachs a new instance of <typeparamref name="TComponent"/> and returns it.
    /// </summary>
    /// <typeparam name="TComponent">A component type that implements <see cref="IComponent"/>.</typeparam>
    /// <returns><typeparamref name="TComponent"/></returns>
    protected TComponent? AddComponent<TComponent> () where TComponent : class, IComponent
    {
        if (_components.Any (c => c.GetType () == typeof (TComponent)) || Activator.CreateInstance (typeof (TComponent), this) is not TComponent component)
            return null;

        _components.Add (component);
        return component;
    }


    /// <summary>
    /// Returns the first <see cref="Entity"/> of type <typeparamref name="TEntity"/> that exists in the <see cref="World"/>.
    /// </summary>
    /// <typeparam name="TEntity">A entity type that derives from <see cref="Entity"/>.</typeparam>
    /// <returns><typeparamref name="TEntity"/></returns>
    protected TEntity? GetEntity<TEntity> () where TEntity : Entity
    {
        return World?.Get<TEntity> (null);
    }

    /// <summary>
    /// Returns the <see cref="Entity"/> of type <typeparamref name="TEntity"/> that exists in the <see cref="World"/>.
    /// </summary>
    /// <typeparam name="TEntity">A entity type that derives from <see cref="Entity"/>.</typeparam>
    /// <param name="name">The name of the <typeparamref name="TEntity"/>.</param>
    /// <returns><typeparamref name="TEntity"/></returns>
    protected TEntity? GetEntity<TEntity> (string name) where TEntity : Entity
    {
        return World?.Get<TEntity> (name);
    }

    /// <summary>
    /// Calls just after the <see cref="Entity"/> initialized. Use to assign values and load content.
    /// </summary>
    protected virtual void OnInitialize ()
    {
    }

    /// <summary>
    /// Calls just <see cref="OnInitialize"/> befor the first frame.
    /// </summary>
    protected virtual void OnStart ()
    {
    }

    /// <summary>
    /// Calls every frame before <see cref="OnDraw"/>. Use to update values of <see cref="Entity"/>.
    /// </summary>
    protected virtual void OnUpdate ()
    {
    }

    /// <summary>
    /// Calls every frame after <see cref="OnUpdate"/>. Use to draw sprites, texts etc.
    /// </summary>
    protected virtual void OnDraw ()
    {
    }

    /// <summary>
    /// Calls when collision detected with <paramref name="entity"/>.
    /// </summary>
    /// <param name="entity"><see cref="Entity"/> that collision detected.</param>
    protected virtual void OnCollision (Entity entity, ICollider collider)
    {
    }

    /// <summary>
    /// Calls just before <see cref="Entity"/> destroyed. Use to dispose content etc.
    /// </summary>
    protected virtual void OnDestroy ()
    {
    }

    // Initializes the entity with world and game.
    internal void Initialize (World world, Game game)
    {
        _world = world;
        _game = game;
        OnInitialize ();
    }

    // Calls the on start method.
    internal void Start () => OnStart ();

    // Calls the on update method.
    internal void Update () => OnUpdate ();

    // Calls the on draw method.
    internal void Draw () => OnDraw ();

    // Calls the on collision method.
    internal void Collision (Entity entity, ICollider collider) => OnCollision (entity, collider);

    #endregion
}

/// <summary>
/// Base class for all entites with configuration.
/// </summary>
/// <typeparam name="TConfiguration">An object to use in <see cref="OnConfigure(TConfiguration)"/></typeparam>
public abstract class Entity<TConfiguration> : Entity
{
    #region Methods

    /// <summary>
    /// Calls just after entity instance created.
    /// </summary>
    /// <param name="configuration"></param>
    public virtual void OnConfigure (TConfiguration configuration)
    {
    }

    // Calls the on configure method.
    internal void Configure (TConfiguration configuration)
    {
        OnConfigure (configuration);
    }

    #endregion
}
