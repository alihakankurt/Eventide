namespace Eventide;

/// <summary>
/// A helper class that contains math methods.
/// </summary>
public static class EventideMath
{
    #region Constants

    /// <summary>
    /// A contant value that represents π.
    /// </summary>
    public const float PI = 3.1415926535897931f;

    #endregion

    #region Methods

    /// <summary>
    /// Converts redians to degrees.
    /// </summary>
    /// <returns><see cref="float"/></returns>
    public static float ToDegrees (float radians)
    {
        return radians * PI / 180f;
    }

    /// <summary>
    /// Returns the smaller value of two <see cref="float"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><see cref="float"/></returns>
    public static float Min (float left, float right)
    {
        return (left < right) ? left : right;
    }

    /// <summary>
    /// Returns the bigger value of two <see cref="float"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns><see cref="float"/></returns>
    public static float Max (float left, float right)
    {
        return (left < right) ? right : left;
    }

    /// <summary>
    /// Returns the square root of <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns><see cref="float"/></returns>
    public static float Sqrt (float value)
    {
        return MathF.Sqrt (value);
    }

    /// <summary>
    /// Returns the sine of <paramref name="angle"/>.
    /// </summary>
    /// <param name="angle">The angle in degrees.</param>
    /// <returns><see cref="float"/></returns>
    public static float Sin (float angle)
    {
        return MathF.Sin (ToDegrees (angle));
    }

    /// <summary>
    /// Returns the cosine of <paramref name="angle"/>.
    /// </summary>
    /// <param name="angle">The angle in degrees.</param>
    /// <returns><see cref="float"/></returns>
    public static float Cos (float angle)
    {
        return MathF.Cos (ToDegrees (angle));
    }

    /// <summary>
    /// Clamps the <paramref name="value"/> between <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The min value of clamp operation.</param>
    /// <param name="max">The max value of clamp operation.</param>
    /// <returns><see cref="float"/></returns>
    public static float Clamp (float value, float min, float max)
    {
        return value < min ? min : value > max ? max : value;
    }

    /// <summary>
    /// Returns the magnitude of 2-dimensional vector using <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <returns><see cref="float"/></returns>
    public static float Magnitude (float x, float y)
    {
        return Sqrt (x * x + y * y);
    }

    #endregion
}
