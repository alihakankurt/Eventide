namespace Eventide.Content;

/// <summary>
/// A playable sound class that contains buffer data.
/// </summary>
public sealed class Sound : ContentBase
{
    #region Fields

    // The sound buffer object from native library.
    private readonly SFMLSoundBuffer _buffer;

    // The sound object from native library.
    private readonly SFMLSound _native;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets that the <see cref="Sound"/> should loop when reachs the end. As default it is false.
    /// </summary>
    public bool Loop
    {
        get => _native.Loop;
        set => _native.Loop = value;
    }

    /// <summary>
    /// Gets or sets the volume of the <see cref="Sound"/>.
    /// </summary>
    public float Volume
    {
        get => _native.Volume;
        set => _native.Volume = value;
    }

    #endregion

    #region Constructors

    /// <inheritdoc/>
    public Sound(string name, string path) : base(name, path)
    {
        _buffer = new (path);
        _native = new (_buffer);
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy(bool disposing)
    {
        _buffer.Dispose ();
        _native.Dispose();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Plays the sound.
    /// </summary>
    public void Play()
    {
        _native.Play();
    }

    /// <summary>
    /// Pauses the sound.
    /// </summary>
    public void Pause()
    {
        _native.Pause();
    }

    /// <summary>
    /// Stops the sounds and reset the position.
    /// </summary>
    public void Stop()
    {
        _native.Stop();
    }

    #endregion
}
