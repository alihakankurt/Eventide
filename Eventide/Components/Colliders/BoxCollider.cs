namespace Eventide.Components.Colliders;

/// <summary>
/// Box collider component.
/// </summary>
public class BoxCollider : ComponentBase, ICollider
{
    #region Fields

    // A static collection that holds all instances of collider.
    private static readonly List<BoxCollider> _instances = new ();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the center position of the <see cref="BoxCollider"/>.
    /// </summary>
    public Vector2 Center => Owner.Transform.Position;

    /// <summary>
    /// Gets the size of the <see cref="BoxCollider"/>.
    /// </summary>
    public Vector2 Size => Owner.Transform.Size * Owner.Transform.Scale;

    /// <summary>
    /// Gets the half of the <see cref="Size"/>.
    /// </summary>
    public Vector2 HalfSize => Size * 0.5f;

    /// <summary>
    /// Gets the bounds of the <see cref="BoxCollider"/>.
    /// </summary>
    public Rectangle Bounds => new (Center.X - HalfSize.X, Center.X + HalfSize.X, Center.Y - HalfSize.Y, Center.Y + HalfSize.Y);

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="BoxCollider"/>.
    /// </summary>
    /// <param name="owner"></param>
    public BoxCollider (Entity owner) : base (owner)
    {
        _instances.Add (this);
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy (bool disposing)
    {
        if (disposing)
        {
            _instances.Remove (this);
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Determines whether the <paramref name="coordinate"/> overlaps the <see cref="BoxCollider"/>.
    /// </summary>
    /// <param name="coordinate">The coordinate to compare.</param>
    /// <param name="vertical">The bool that determines the <paramref name="coordinate"/> is vertical or horizontal.</param>
    /// <returns><see cref="bool"/></returns>
    public bool Overlaps (float coordinate, bool vertical = false)
    {
        return Bounds.Overlaps (coordinate, vertical);
    }

    /// <summary>
    /// Determines whether the <paramref name="point"/> overlaps the <see cref="BoxCollider"/>.
    /// </summary>
    /// <param name="point">The point to compare.</param>
    /// <returns><see cref="bool"/></returns>
    public bool Overlaps (Vector2 point)
    {
        return Bounds.Overlaps (point);
    }

    /// <summary>
    /// Determines whether the <paramref name="other"/> collider overlaps the <see cref="BoxCollider"/>.
    /// </summary>
    /// <param name="other">The other box-collider to compare.</param>
    /// <returns><see cref="bool"/></returns>
    public bool Overlaps (BoxCollider other)
    {
        return Bounds.Overlaps (other.Bounds);
    }

    // Detects the collision between objects.
    internal static void Update ()
    {
        if (_instances.Count == 0)
            return;

        for (int i = 0; i < _instances.Count; i++)
        {
            var instance = _instances[i];
            if (!instance.Owner.World?.EntitiesToUpdate.Contains (instance.Owner) ?? false)
                continue;

            for (int j = i + 1; j < _instances.Count; j++)
            {
                if (_instances[j].Overlaps (instance))
                {
                    instance.Owner.Collision (_instances[j].Owner, _instances[j]);
                    _instances[j].Owner.Collision (instance.Owner, instance);
                }
            }
        }
    }

    #endregion
}