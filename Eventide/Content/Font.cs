namespace Eventide.Content;

/// <summary>
/// Represents a font file.
/// </summary>
public sealed class Font : ContentBase
{
    #region Fields

    // The font from native library.
    private readonly SFMLFont _native;

    #endregion

    #region Properties

    // Internal property that returns font from native library.
    internal SFMLFont Native => _native;

    #endregion

    #region Constructors

    /// <inheritdoc/>
    internal Font (string name, string path) : base (name, path)
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
}
