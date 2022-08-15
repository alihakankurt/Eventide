namespace Eventide;

/// <summary>
/// Base class for all <see href="Eventide"/> classes.
/// </summary>
public abstract class ObjectBase : IDisposable
{
    #region Fields

    // A bool that determines whether object is disposed.
    private bool _disposed;

    #endregion

    #region Finalizers

    /// <summary>
    /// A finalizer that disposes unmanaged resources.
    /// </summary>
    ~ObjectBase () => Dispose (false);

    #endregion

    #region Methods

    /// <inheritdoc/>
    public void Dispose ()
    {
        Dispose (true);
        GC.SuppressFinalize (this);
    }

    /// <summary>
    /// Overridable <see cref="Destroy(bool)"/> method that calling in dispose.
    /// </summary>
    protected virtual void Destroy (bool disposing)
    {
    }

    // Private dispose method.
    private void Dispose (bool disposing)
    {
        if (_disposed)
            return;

        Destroy (disposing);
        _disposed = true;
    }

    #endregion
}
