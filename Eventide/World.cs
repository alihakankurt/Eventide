namespace Eventide;

/// <summary>
/// The world class that entities exists in.
/// </summary>
public sealed class World : ObjectBase
{
    #region Fields

    // A readonly collection that contains entities.
    private readonly List<Entity> _entities;

    // A readonly collection that contains entities to update.
    private readonly List<Entity> _entitiesToUpdate;

    // A readonly comparison delegate that using to sort entities.
    private readonly Comparison<Entity> sortDelegate = (e1, e2) => e1.Layer.CompareTo (e2.Layer);

    // The game that world attached to.
    private readonly Game? _game;

    // Determines whether world is initialized.
    private bool _initialized;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the camera of the <see cref="World"/>.
    /// </summary>
    public Camera Camera { get; }

    // Internal property that returns entities to update.
    internal List<Entity> EntitiesToUpdate => _entitiesToUpdate;

    #endregion

    #region Constructors

    public World (Game game)
    {
        _game = game;

        _entities = new ();
        _entitiesToUpdate = new ();

        Camera = new (_game.Window);
    }

    #endregion

    #region Finalizers

    protected override void Destroy (bool disposing)
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            _entities[i].Dispose ();
        }
        if (disposing)
        {
            _entities.Clear ();
            _entitiesToUpdate.Clear ();
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns the <see cref="Entity"/> of type <typeparamref name="TEntity"/> that exists in the <see cref="World"/>.
    /// </summary>
    /// <typeparam name="TEntity">A entity type that derives from <see cref="Entity"/>.</typeparam>
    /// <param name="name">The name of the <typeparamref name="TEntity"/>. It can be null.</param>
    /// <returns><typeparamref name="TEntity"/></returns>
    public TEntity? Get<TEntity> (string? name) where TEntity : Entity
    {
        return _entities.FirstOrDefault (e => e.GetType () == typeof (TEntity) && (name == null || e.Name == name)) as TEntity;
    }

    /// <summary>
    /// Spawns the entity of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">A entity type that derives from <see cref="Entity"/>.</typeparam>
    public void Spawn<TEntity> () where TEntity : Entity
    {
        Spawn<TEntity> (out _);
    }

    /// <summary>
    /// Spawns the entity of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">A entity type that derives from <see cref="Entity"/>.</typeparam>
    /// <param name="entity">The out parameter that holds reference of instantiaded.</param>
    public void Spawn<TEntity> (out TEntity entity) where TEntity : Entity
    {
        entity = Activator.CreateInstance<TEntity> ();
        if (_initialized)
        {
            entity.Initialize (this, _game!);
            entity.Start ();
        }
        _entities.Add (entity);
    }

    /// <summary>
    /// Spawns the entity of type <typeparamref name="TEntity"/> wich need configuration of type <typeparamref name="TConfiguration"/>.
    /// </summary>
    /// <typeparam name="TEntity">A entity type that derives from <see cref="Entity"/>.</typeparam>
    /// <typeparam name="TConfiguration">The configuration object.</typeparam>
    /// <param name="configuration">An instance of <typeparamref name="TConfiguration"/>.</param>
    public void Spawn<TEntity, TConfiguration> (TConfiguration configuration) where TEntity : Entity<TConfiguration>
    {
        Spawn<TEntity, TConfiguration> (configuration, out _);
    }

    /// <summary>
    /// Spawns the entity of type <typeparamref name="TEntity"/> wich need configuration of type <typeparamref name="TConfiguration"/>.
    /// </summary>
    /// <typeparam name="TEntity">A entity type that derives from <see cref="Entity"/>.</typeparam>
    /// <typeparam name="TConfiguration">The configuration object.</typeparam>
    /// <param name="configuration">An instance of <typeparamref name="TConfiguration"/>.</param>
    /// <param name="entity">The out parameter that holds reference of instantiaded.</param>
    public void Spawn<TEntity, TConfiguration> (TConfiguration configuration, out TEntity entity) where TEntity : Entity<TConfiguration>
    {
        entity = Activator.CreateInstance<TEntity> ();
        entity.Configure (configuration);
        if (_initialized)
        {
            entity.Initialize (this, _game!);
            entity.Start ();
        }
        _entities.Add (entity);
    }

    /// <summary>
    /// Kills the entity that exists in the <see cref="World"/>.
    /// </summary>
    /// <param name="entity">The entity instance to kill.</param>
    public void Kill (Entity entity)
    {
        if (_entities.Remove (entity))
        {
            entity.Dispose ();
        }
    }

    /// <summary>
    /// Kills the entity that exists in the <see cref="World"/>.
    /// </summary>
    /// <param name="name">The name of the entity to kill.</param>
    public void Kill (string name)
    {
        if (_entities.FirstOrDefault (e => e.Name == name) is Entity entity)
        {
            _entities.Remove (entity);
            entity.Dispose ();
        }
    }

    // initializes the world.
    internal void Initialize ()
    {
        if (_initialized)
            return;

        _initialized = true;

        int n = _entities.Count;

        for (int i = 0; i < n; i++)
        {
            _entities[i].Initialize (this, _game!);
        }

        for (int i = 0; i < n; i++)
        {
            _entities[i].Start ();
        }
    }

    // Updates the entities each frame.
    internal void Update ()
    {
        _entitiesToUpdate.Clear ();
        for (int i = 0; i < _entities.Count; i++)
        {
            var entity = _entities[i];
            if (Camera.UpdateArea.Overlaps (entity.Transform.Position))
            {
                _entitiesToUpdate.Add (entity);
            }
        }
        _entitiesToUpdate.Sort (sortDelegate);

        for (int i = 0; i < _entitiesToUpdate.Count; i++)
        {
            _entitiesToUpdate[i].Update ();
        }
    }

    // Draws entities on every frame.
    internal void Draw ()
    {
        for (int i = 0; i < _entitiesToUpdate.Count; i++)
        {
            _entitiesToUpdate[i].Draw ();
        }
    }

    #endregion
}
