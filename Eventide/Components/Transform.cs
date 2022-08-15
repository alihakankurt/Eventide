namespace Eventide.Components;

/// <summary>
/// Transfrom component.
/// </summary>
public class Transform : ComponentBase
{
    #region Properties

    /// <summary>
    /// Gets the position of the <see cref="Entity"/>.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets the size of the <see cref="Entity"/>.
    /// </summary>
    public Vector2 Size { get; set; }

    /// <summary>
    /// Gets the scale of the <see cref="Entity"/>.
    /// </summary>
    public Vector2 Scale { get; set; }

    /// <summary>
    /// Gets the rotation of the <see cref="Entity"/>.
    /// </summary>
    public float Rotation { get; set; }

    #endregion

    #region Constructors

    /// <inheritdoc/>
    public Transform (Entity owner) : base (owner)
    {
        Position = Vector2.Zero;
        Size = Vector2.Zero;
        Scale = Vector2.One;
        Rotation = 0f;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Moves <see cref="Position"/> smoothly using <paramref name="velocity"/> and <see cref="Time.DeltaTime"/>.
    /// </summary>
    /// <param name="velocity">The move velocity.</param>
    public void Move (Vector2 velocity)
    {
        Position += velocity * Time.DeltaTime;
    }

    /// <summary>
    /// Sets the size of the <see cref="Size"/> to new <see cref="Vector2"/> that have <paramref name="width"/> as <see cref="Vector2.X"/> and <paramref name="height"/> as <see cref="Vector2.Y"/>.
    /// </summary>
    /// <param name="width">The width value in X-plane.</param>
    /// <param name="height">The height value in Y-plane.</param>
    public void Resize (float width, float height)
    {
        Size = new (width, height);
    }

    /// <summary>
    /// Scales the <see cref="Scale"/> to new <see cref="Vector2"/> that have <paramref name="x"/> as <see cref="Vector2.X"/> and <paramref name="y"/> as <see cref="Vector2.Y"/>.
    /// </summary>
    /// <param name="x">The scale value in X-plane.</param>
    /// <param name="y">The scale value in Y-plane.</param>
    public void Rescale (float x, float y)
    {
        Scale = new (x, y);
    }

    /// <summary>
    /// Rotates the <see cref="Rotation"/> to the <paramref name="angle"/>.
    /// </summary>
    /// <param name="angle">The rotation angle in degrees.</param>
    public void Rotate (float angle)
    {
        Rotation += angle;
    }

    #endregion
}
