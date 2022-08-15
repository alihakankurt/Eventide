namespace Eventide;

/// <summary>
/// A representation of color structure.
/// </summary>
public struct Color
{
    #region Constants

    public static readonly Color Transparent = new (0, 0, 0, 0);
    public static readonly Color Black = new (0, 0, 0);
    public static readonly Color White = new (255, 255, 255);
    public static readonly Color Gray = new (127, 127, 127);
    public static readonly Color Red = new (255, 0, 0);
    public static readonly Color Green = new (0, 255, 0);
    public static readonly Color Blue = new (0, 0, 255);
    public static readonly Color Yellow = new (255, 255, 0);
    public static readonly Color Magenta = new (255, 0, 255);
    public static readonly Color Cyan = new (0, 255, 255);

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the alpha value of the <see cref="Color"/>.
    /// </summary>
    public byte A { get; set; }

    /// <summary>
    /// Gets or sets the red value of the <see cref="Color"/>.
    /// </summary>
    public byte R { get; set; }

    /// <summary>
    /// Gets or sets the green value of the <see cref="Color"/>.
    /// </summary>
    public byte G { get; set; }

    /// <summary>
    /// Gets or sets the blue value of the <see cref="Color"/>.
    /// </summary>
    public byte B { get; set; }

    /// <summary>
    /// Gets the raw value of the <see cref="Color"/>.
    /// </summary>
    public uint RawValue => (uint)((A << 24) | (R << 16) | (G << 8) | (B << 0));

    /// <summary>
    /// Gets the raw value of the <see cref="Color"/> in order RGBA.
    /// </summary>
    public uint RawValueRGBA => (uint)((R << 24) | (G << 16) | (B << 8) | (A << 0));

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="Color"/> structure with specified <paramref name="r"/>, <paramref name="g"/> and <paramref name="b"/> values.
    /// </summary>
    public Color (byte r, byte g, byte b) : this (r, g, b, 255)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Color"/> structure with specified <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> and <paramref name="a"/> values.
    /// </summary>
    public Color (byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Color"/> structure with specified <paramref name="rawValue"/>.
    /// </summary>
    /// <param name="rawValue">The raw value of color. (ARGB)</param>
    public Color (uint rawValue) : this ((byte)(rawValue >> 16), (byte)(rawValue >> 8), (byte)(rawValue >> 0), (byte)(rawValue >> 24))
    {
    }

    #endregion

    #region Methods

    public override string ToString ()
    {
        return Convert.ToString (RawValue, 16);
    }

    #endregion
}
