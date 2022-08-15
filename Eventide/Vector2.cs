namespace Eventide;

/// <summary>
/// A representation a 2-dimensional vector.
/// </summary>
public struct Vector2 : IEquatable<Vector2>
{
    #region Constants

    /// <summary>
    /// Static instance of the <see cref="Vector2"/> that <see cref="X"/> and <see cref="Y"/> is 0.
    /// </summary>
    public static readonly Vector2 Zero = new (0f, 0f);

    /// <summary>
    /// Static instance of the <see cref="Vector2"/> that <see cref="X"/> and <see cref="Y"/> is 1.
    /// </summary>
    public static readonly Vector2 One = new (1f, 1f);

    /// <summary>
    /// Static instance of the <see cref="Vector2"/> that <see cref="X"/> is 1 and <see cref="Y"/> is -1.
    /// </summary>
    public static readonly Vector2 Opposite = new (1f, -1f);

    /// <summary>
    /// Static instance of the <see cref="Vector2"/> that <see cref="X"/> is 1.
    /// </summary>
    public static readonly Vector2 UnitX = new (1f, 0f);

    /// <summary>
    /// Static instance of the <see cref="Vector2"/> that <see cref="Y"/> is 1.
    /// </summary>
    public static readonly Vector2 UnitY = new (0f, 1f);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the position in X-plane of the <see cref="Vector2"/>.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Gets or sets position in Y-plane of the <see cref="Vector2"/>.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Gets the magnitude of the <see cref="Vector2"/>.
    /// </summary>
    public float Magnitude => EventideMath.Magnitude (X, Y);

    #endregion

    #region Constructors

    /// <summary>
    /// Use <see cref="Zero"/> instead of this.
    /// </summary>
    public Vector2 () : this (0f, 0f)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Vector2"/> structure.
    /// </summary>
    /// <param name="x">The position in X-plane.</param>
    /// <param name="y">The position in Y-plane.</param>
    public Vector2 (float x, float y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Vector2"/> structure using <paramref name="other"/>'s X and Y value.
    /// </summary>
    /// <param name="other">The <see cref="Vector2"/> instance to clone.</param>
    public Vector2 (Vector2 other) : this (other.X, other.Y)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns the <paramref name="instance"/> with negative <see cref="X"/> and <see cref="Y"/>.
    /// </summary>
    /// <param name="instance">The <see cref="Vector2"/> instance to negate.</param>
    /// <returns><see cref="Vector2"/></returns>
    public static Vector2 Negate (Vector2 instance)
    {
        return -instance;
    }

    /// <summary>
    /// Returns the <paramref name="instance"/> as normalized.
    /// </summary>
    /// <param name="instance">The <see cref="Vector2"/> instance to normalize.</param>
    /// <returns><see cref="Vector2"/></returns>
    public static Vector2 Normalize (Vector2 instance)
    {
        return new (instance.X / instance.Magnitude, instance.Y / instance.Magnitude);
    }

    /// <summary>
    /// Returns the <paramref name="instance"/> as rotated with specified <paramref name="angle"/>.
    /// </summary>
    /// <param name="instance">The <see cref="Vector2"/> instance to rotate.</param>
    /// <param name="angle">Rotation angle in degrees.</param>
    /// <returns><see cref="Vector2"/></returns>
    public static Vector2 Rotate (Vector2 instance, float angle)
    {
        float sin = EventideMath.Sin (angle);
        float cos = EventideMath.Cos (angle);
        float rotatedX = (instance.X * cos) - (instance.Y * sin);
        float rotatedY = (instance.X * sin) + (instance.Y * cos);
        return new (rotatedX, rotatedY);
    }

    /// <summary>
    /// Returns <paramref name="instance"/> as clamped between <paramref name="minimum"/> and <paramref name="maximum"/>.
    /// </summary>
    /// <param name="instance">The <see cref="Vector2"/> instance to clamp.</param>
    /// <param name="minimum">Minimum value of clamp operation.</param>
    /// <param name="maximum">Maximum value of clamp operation.</param>
    /// <returns><see cref="Vector2"/></returns>
    public static Vector2 Clamp (Vector2 instance, Vector2 minimum, Vector2 maximum)
    {
        float clampedX = EventideMath.Clamp (instance.X, EventideMath.Min (minimum.X, maximum.X), EventideMath.Max (minimum.X, maximum.X));
        float clampedY = EventideMath.Clamp (instance.Y, EventideMath.Min (minimum.Y, maximum.Y), EventideMath.Max (minimum.Y, maximum.Y));
        return new (clampedX, clampedY);
    }

    /// <summary>
    /// Returns distance between <paramref name="left"/> and <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Vector2"/> instance to calculate.</param>
    /// <param name="right">The second <see cref="Vector2"/> instance to calculate.</param>
    /// <returns><see cref="float"/></returns>
    public static float Distance (Vector2 left, Vector2 right)
    {
        float x = left.X - right.X;
        float y = left.Y - right.Y;
        return EventideMath.Magnitude (x, y);
    }

    /// <summary>
    /// Returns dot product of <paramref name="left"/> and <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Vector2"/> instance to calculate.</param>
    /// <param name="right">The second <see cref="Vector2"/> instance to calculate.</param>
    /// <returns><see cref="float"/></returns>
    public static float Dot (Vector2 left, Vector2 right)
    {
        return left.X * right.X + left.Y * right.Y;
    }

    /// <summary>
    /// Returns cross product of <paramref name="left"/> and <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Vector2"/> instance to calculate.</param>
    /// <param name="right">The second <see cref="Vector2"/> instance to calculate.</param>
    /// <returns><see cref="float"/></returns>
    public static float Cross (Vector2 left, Vector2 right)
    {
        return left.X * right.Y - left.Y * right.X;
    }

    /// <inheritdoc/>
    public bool Equals (Vector2 other)
    {
        return X == other.X && Y == other.Y;
    }

    /// <inheritdoc/>
    public override bool Equals (object? obj)
    {
        return obj is Vector2 vector && Equals (vector);
    }

    /// <inheritdoc/>
    public override int GetHashCode ()
    {
        return HashCode.Combine (X, Y);
    }

    /// <inheritdoc/>
    public override string ToString ()
    {
        return $"<{X}, {Y}>";
    }

    #endregion

    #region Operators

    public static Vector2 operator + (Vector2 left, Vector2 right) => new (left.X + right.X, left.Y + right.Y);

    public static Vector2 operator - (Vector2 left, Vector2 right) => new (left.X - right.X, left.Y - right.Y);

    public static Vector2 operator - (Vector2 left) => new (-left.X, -left.Y);

    public static Vector2 operator * (Vector2 left, Vector2 right) => new (left.X * right.X, left.Y * right.Y);

    public static Vector2 operator * (Vector2 left, float right) => new (left.X * right, left.Y * right);

    public static Vector2 operator * (float left, Vector2 right) => new (left * right.X, left * right.Y);

    public static bool operator == (Vector2 left, Vector2 right) => left.Equals (right);

    public static bool operator != (Vector2 left, Vector2 right) => !(left == right);

    #endregion
}
