namespace Eventide.Content;

/// <summary>
/// A sprite class that contains texture data.
/// </summary>
public sealed class Sprite : ObjectBase
{
    #region Fields

    // The sprite object from native library.
    private readonly SFMLSprite _native;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the X-position of the <see cref="Sprite"/>.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the Y-position of the <see cref="Sprite"/>.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Gets the width of the <see cref="Sprite"/>.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the <see cref="Sprite"/>.
    /// </summary>
    public int Height { get; }

    // Internal property that returns the sprite from native library.
    internal SFMLSprite Native => _native;

    #endregion

    #region

    /// <summary>
    /// Initializes a new instance of <see cref="Sprite"/>.
    /// </summary>
    /// <param name="texture">The source texture to use in sprite.</param>
    /// <param name="x">The X-position of texture.</param>
    /// <param name="y">The Y-position of texture.</param>
    /// <param name="width">The width of sprite.</param>
    /// <param name="height">The height of sprite</param>
    public Sprite(Texture texture, int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;

        _native = new(texture.Native, new(x, y, width, height));
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy(bool disposing)
    {
        _native.Dispose();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Draws the sprite to the <see cref="Window"/>.
    /// </summary>
    /// <param name="window">The window to draw the sprite.</param>
    /// <param name="position">The center point of the sprite.</param>
    /// <param name="scale">The scale of the sprite.</param>
    /// <param name="rotation">The rotation angle of the sprite.</param>
    public void Draw(Window window, Vector2 position, Vector2 scale, float rotation)
    {
        window.Draw(this, position, scale, rotation);
    }

    /// <summary>
    /// Sets the area of <see cref="Sprite"/>.
    /// </summary>
    /// <param name="x">The X-position of texture.</param>
    /// <param name="y">The Y-position of texture.</param>
    /// <param name="width">The width of sprite.</param>
    /// <param name="height">The height of sprite</param>
    public void SetArea(int x, int y, int width, int height)
    {
        _native.TextureRect = new(x, y, width, height);
    }

    #endregion
}
