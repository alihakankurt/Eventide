namespace Eventide;

/// <summary>
/// A representation of 2-dimensional camera.
/// </summary>
public sealed class Camera : ObjectBase
{
    #region Fields

    // The window that camera attached to.
    private readonly Window _window;

    // The center positon of camera.
    private Vector2 _focus;

    // The rotation angle of camera.
    private float _rotationAngle;

    // The zoom factor of camera.
    private float _zoomFactor;

    // The update area of camera.
    private Rectangle _updateArea;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the center of the <see cref="Camera"/>.
    /// </summary>
    public Vector2 Focus => _focus;

    /// <summary>
    /// Gets the rotation angle of the <see cref="Camera"/>.
    /// </summary>
    public float RotationAngle => _rotationAngle;

    /// <summary>
    /// Gets the zoom factor of the <see cref="Camera"/>.
    /// </summary>
    public float ZoomFactor => _zoomFactor;

    /// <summary>
    /// Gets the update are of the <see cref="Camera"/>.
    /// </summary>
    public Rectangle UpdateArea => _updateArea;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="Camera"/>.
    /// </summary>
    /// <param name="window">The window instance to update view.</param>
    internal Camera (Window window)
    {
        _window = window;
        _focus = new (window.Width / 2, window.Height / 2);
        _rotationAngle = 0f;
        _zoomFactor = 1f;
        CreateUpdateArea ();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets the center to a new <see cref="Vector2"/> that uses <paramref name="x"/> as <see cref="Vector2.X"/> and <paramref name="y"/> as <see cref="Vector2.Y"/>.
    /// </summary>
    /// <param name="x">The focus position in X-plane.</param>
    /// <param name="y">The focus position in Y-plane.</param>
    public void FocusOn (float x, float y)
    {
        _focus = new (x, y);
        _window.UpdateView (v => v.Center = new (_focus.X, _focus.Y));
        CreateUpdateArea ();
    }

    /// <summary>
    /// Sets the center to the <paramref name="focus"/>.
    /// </summary>
    /// <param name="focus">The new focus position.</param>
    public void FocusOn (Vector2 focus)
    {
        _focus = focus;
        _window.UpdateView (v => v.Center = new (_focus.X, _focus.Y));
        CreateUpdateArea ();
    }

    /// <summary>
    /// Rotates the view by <paramref name="angle"/> degrees.
    /// </summary>
    /// <param name="angle">Rotation angle in degrees.</param>
    public void Rotate (float angle)
    {
        _rotationAngle = angle;
        _window.UpdateView (v => v.Rotation = _rotationAngle);
    }

    /// <summary>
    /// Zooms in or out depends on <paramref name="factor"/>.
    /// </summary>
    /// <param name="factor">Zoom factor.</param>
    public void Zoom (float factor)
    {
        _zoomFactor = factor;
        _window.UpdateView (v => v.Zoom (1f / _zoomFactor));
        CreateUpdateArea ();
    }

    // Creates the update area.
    internal void CreateUpdateArea ()
    {
        var zoomedFocus = _focus * _zoomFactor;
        _updateArea = new (zoomedFocus.X - _window.Width, zoomedFocus.X + _window.Width, zoomedFocus.Y - _window.Height, zoomedFocus.Y + _window.Height);
    }

    #endregion
}
