namespace Eventide;

/// <summary>
/// A static class that holds the time values.
/// </summary>
public static class Time
{
    #region Constants

    // The constant value which used in calculating deltatime.
    private const float ticksPerSecond = 10000000f;

    #endregion

    #region Fields

    // The stopwatch to use in calculating deltatime.
    private static Stopwatch _stopwatch;

    // The float value that holds sum of deltatimes.
    private static float _totalTime;

    // The float value that holds the frame time.
    private static float _deltaTime;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the sum of deltatimes.
    /// </summary>
    public static double TotalTime => _totalTime;

    /// <summary>
    /// Gets the frame time.
    /// </summary>
    public static float DeltaTime => _deltaTime;

    /// <summary>
    /// Gets or sets the time scale.
    /// </summary>
    public static float Scale { get; set; }

    #endregion

    #region Constructors

    // Static constructor that assign values of fields.
    static Time ()
    {
        _stopwatch = new ();
        _totalTime = 0f;
        _deltaTime = 0f;
        Scale = 1f;
    }

    #endregion

    #region Methods

    // Starts the stopwatch.
    internal static void Start ()
    {
        _stopwatch.Start ();
    }

    // Restarts the stopwatch and calculates the deltatime.
    internal static void Restart ()
    {
        _deltaTime = _stopwatch.ElapsedTicks / ticksPerSecond * Time.Scale;
        _totalTime += _deltaTime;
        _stopwatch.Restart ();
    }

    #endregion
}
