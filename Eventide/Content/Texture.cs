namespace Eventide.Content;

/// <summary>
/// Represents a image file.
/// </summary>
public sealed class Texture : ContentBase
{
    #region Fields

    // The texture from native library.
    private readonly SFMLTexture _native;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the width of the <see cref="Texture"/>.
    /// </summary>
    public int Width => (int)_native.Size.X;

    /// <summary>
    /// Gets the height of the <see cref="Texture"/>.
    /// </summary>
    public int Height => (int)_native.Size.Y;

    // Internal property that returns sound buffer from native library.
    internal SFMLTexture Native => _native;

    #endregion

    #region Constructors

    /// <inheritdoc/>
    internal Texture (string name, string path) : base (name, path)
    {
        _native = new (path);
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy (bool disposing)
    {
        _native.Dispose ();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns a new instance of <see cref="Sprite"/> using all data.
    /// </summary>
    /// <returns><see cref="Sprite"/></returns>
    public Sprite ToSprite ()
    {
        return new (this, 0, 0, Width, Height);
    }

    #endregion
}
