namespace Eventide;

/// <summary>
/// A representation of rectangle.
/// </summary>
public struct Rectangle
{
    #region Properties

    /// <summary>
    /// Gets or sets the left side of the <see cref="Rectangle"/>.
    /// </summary>
    public float Left { get; set; }

    /// <summary>
    /// Gets or sets the right side of the <see cref="Rectangle"/>.
    /// </summary>
    public float Right { get; set; }

    /// <summary>
    /// Gets or sets the top side of the <see cref="Rectangle"/>.
    /// </summary>
    public float Top { get; set; }

    /// <summary>
    /// Gets or sets the bottom side of the <see cref="Rectangle"/>.
    /// </summary>
    public float Bottom { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="Rectangle"/> structure.
    /// </summary>
    /// <param name="left">The left side.</param>
    /// <param name="right">The right side.</param>
    /// <param name="top">The top side.</param>
    /// <param name="bottom">The bottom side.</param>
    public Rectangle (float left, float right, float top, float bottom)
    {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
    }

    #endregion

    #region Methods


    /// <summary>
    /// Determines whether the <paramref name="coordinate"/> overlaps the <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="coordinate">The coordinate to compare.</param>
    /// <param name="vertical">The bool that determines the <paramref name="coordinate"/> is vertical or horizontal.</param>
    /// <returns><see cref="bool"/></returns>
    public bool Overlaps (float coordinate, bool vertical = false)
    {
        return !(vertical ? (coordinate < Top || coordinate > Bottom) : (coordinate < Left || coordinate > Right));
    }

    /// <summary>
    /// Determines whether the <paramref name="point"/> overlaps the <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="point">The <see cref="Vector2"/> to compare.</param>
    /// <returns><see cref="bool"/></returns>
    public bool Overlaps (Vector2 point)
    {
        return !(point.X < Left || point.X > Right || point.Y < Top || point.Y > Bottom);
    }

    /// <summary>
    /// Determines whether the <paramref name="other"/> collider overlaps the <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="other">The other <see cref="Rectangle"/> to compare.</param>
    /// <returns><see cref="bool"/></returns>
    public bool Overlaps (Rectangle other)
    {
        return !(Left > other.Right || other.Left > Right || Top > other.Bottom || other.Top > Bottom);
    }

    #endregion
}
