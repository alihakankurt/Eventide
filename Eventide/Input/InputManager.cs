namespace Eventide.Input;

/// <summary>
/// A manager class that manages input.
/// </summary>
public sealed class InputManager : ObjectBase
{
    #region Fields

    /// <summary>
    /// A readonly collection that contains keys and states.
    /// </summary>
    public readonly Dictionary<Key, InputState> KeyStates;

    /// <summary>
    /// A readonly collection that contains button and stats.
    /// </summary>
    public readonly Dictionary<Button, InputState> ButtonStates;

    // Current mouse position.
    private Vector2 _mousePosition;

    // Current mouse wheel delta.
    private float _wheelDelta;

    // The frame time counter.
    private float _frameTime;

    // The time between two input updates.
    private float _frameTimeToUpdate;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the current position of mouse.
    /// </summary>
    public Vector2 MousePosition => _mousePosition;

    /// <summary>
    /// Gets the wheel delta of mouse.
    /// </summary>
    public float WheelDelta => _wheelDelta;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="InputManager"/>.
    /// </summary>
    internal InputManager ()
    {
        KeyStates = Enum.GetValues<Key> ().Where (k => k is not Key.Any or Key.Unknown).ToDictionary (k => k, _ => InputState.Up);
        ButtonStates = Enum.GetValues<Button> ().Where (b => b is not Button.Any or Button.Unknown).ToDictionary (b => b, _ => InputState.Up);

        _mousePosition = Vector2.Zero;
        _wheelDelta = 0f;

        _frameTime = 0f;
        _frameTimeToUpdate = 0.1666667f;
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy (bool disposing)
    {
        KeyStates.Clear ();
        ButtonStates.Clear ();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns the raw value of horizontal axis. -1 for left, +1 for right and 0 as default.
    /// </summary>
    /// <returns><see cref="int"/></returns>
    public int GetHorizontalAxis ()
    {
        int axis = 0;
        axis -= IsKeyDown (Key.A) || IsKeyDown (Key.Left) ? 1 : 0;
        axis += IsKeyDown (Key.D) || IsKeyDown (Key.Right) ? 1 : 0;
        return axis;
    }

    /// <summary>
    /// Returns the raw value of vertical axis. -1 for up, +1 for down and 0 as default.
    /// </summary>
    /// <returns><see cref="int"/></returns>
    public int GetVerticalAxis ()
    {
        int axis = 0;
        axis -= IsKeyDown (Key.W) || IsKeyDown (Key.Up) ? 1 : 0;
        axis += IsKeyDown (Key.S) || IsKeyDown (Key.Down) ? 1 : 0;
        return axis;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="key"/> is <see cref="InputState.Up"/> or <see cref="InputState.Released"/>.
    /// </summary>
    /// <param name="key">Key to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsKeyUp (Key key)
    {
        if (key == Key.Unknown)
            return false;

        if (key == Key.Any)
            return KeyStates.Values.Any (k => k is InputState.Up or InputState.Released);

        return KeyStates[key] is InputState.Up or InputState.Released;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="key"/> is <see cref="InputState.Down"/> or <see cref="InputState.Pressed"/>.
    /// </summary>
    /// <param name="key">Key to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsKeyDown (Key key)
    {
        if (key == Key.Unknown)
            return false;

        if (key == Key.Any)
            return KeyStates.Values.Any (k => k is InputState.Down or InputState.Pressed);

        return KeyStates[key] is InputState.Down or InputState.Pressed;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="key"/> is <see cref="InputState.Released"/>.
    /// </summary>
    /// <param name="key">Key to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsKeyReleased (Key key)
    {
        if (key == Key.Unknown)
            return false;

        if (key == Key.Any)
        {
            key = KeyStates.FirstOrDefault (k => k.Value is InputState.Released).Key;
            if (key == Key.Unknown)
                return false;
        }

        if (KeyStates[key] != InputState.Released)
            return false;

        KeyStates[key] = InputState.Up;
        return true;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="key"/> is <see cref="InputState.Pressed"/>.
    /// </summary>
    /// <param name="key">Key to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsKeyPressed (Key key)
    {
        if (key == Key.Unknown)
            return false;

        if (key == Key.Any)
        {
            key = KeyStates.FirstOrDefault (k => k.Value is InputState.Pressed).Key;
            if (key == Key.Unknown)
                return false;
        }

        if (KeyStates[key] != InputState.Pressed)
            return false;

        KeyStates[key] = InputState.Down;
        return true;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="button"/> is <see cref="InputState.Up"/> or <see cref="InputState.Released"/>.
    /// </summary>
    /// <param name="button">Button to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsButtonUp (Button button)
    {
        if (button == Button.Unknown)
            return false;

        if (button == Button.Any)
            return ButtonStates.Values.Any (k => k is InputState.Up or InputState.Released);

        return ButtonStates[button] is InputState.Up or InputState.Released;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="button"/> is <see cref="InputState.Down"/> or <see cref="InputState.Pressed"/>.
    /// </summary>
    /// <param name="button">Button to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsButtonDown (Button button)
    {
        if (button == Button.Unknown)
            return false;

        if (button == Button.Any)
            return ButtonStates.Values.Any (k => k is InputState.Down or InputState.Pressed);

        return ButtonStates[button] is InputState.Down or InputState.Pressed;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="button"/> is <see cref="InputState.Released"/>.
    /// </summary>
    /// <param name="button">Button to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsButtonReleased (Button button)
    {
        if (button == Button.Unknown)
            return false;

        if (button == Button.Any)
        {
            button = ButtonStates.FirstOrDefault (b => b.Value is InputState.Released).Key;
            if (button == Button.Unknown)
                return false;
        }

        if (ButtonStates[button] != InputState.Released)
            return false;

        ButtonStates[button] = InputState.Up;
        return true;
    }

    /// <summary>
    /// Determines wheter the state of the <paramref name="button"/> is <see cref="InputState.Pressed"/>.
    /// </summary>
    /// <param name="button">Button to check.</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsButtonPressed (Button button)
    {
        if (button == Button.Unknown)
            return false;

        if (button == Button.Any)
        {
            button = ButtonStates.FirstOrDefault(b => b.Value is InputState.Pressed).Key;
            if (button == Button.Unknown)
                return false;
        }

        if (ButtonStates[button] != InputState.Pressed)
            return false;

        ButtonStates[button] = InputState.Down;
        return true;
    }

    // Updates the mouse position.
    internal void MouseMove (int x, int y)
    {
        _mousePosition = new Vector2 (x, y);
    }

    // Updates the mouse wheel delta.
    internal void MouseWheel (float delta)
    {
        _wheelDelta = delta;
    }

    // Updates the key state to released.
    internal void ReleaseKey (Key key)
    {
        if (key is Key.Any or Key.Unknown)
            return;

        if (KeyStates[key] == InputState.Down || KeyStates[key] == InputState.Pressed)
        {
            KeyStates[key] = InputState.Released;
        }
    }

    // Updates the key state to pressed.
    internal void PressKey (Key key)
    {
        if (key is Key.Any or Key.Unknown)
            return;

        if (KeyStates[key] == InputState.Up || KeyStates[key] == InputState.Released)
        {
            KeyStates[key] = InputState.Pressed;
        }
    }

    // Updates the button state to released.
    internal void ReleaseButton (Button button)
    {
        if (button is Button.Any or Button.Unknown)
            return;

        if (ButtonStates[button] == InputState.Down || ButtonStates[button] == InputState.Pressed)
        {
            ButtonStates[button] = InputState.Released;
        }
    }

    // Updates the button state to pressed.
    internal void PressButton (Button button)
    {
        if (button is Button.Any or Button.Unknown)
            return;

        if (ButtonStates[button] == InputState.Up || ButtonStates[button] == InputState.Released)
        {
            ButtonStates[button] = InputState.Pressed;
        }
    }

    // Updates all states to default.
    internal void Update ()
    {
        _frameTime += Time.DeltaTime;
        if (_frameTime < _frameTimeToUpdate)
            return;

        _frameTime -= _frameTimeToUpdate;

        _wheelDelta = 0f;
        UpdateStates<Key> (KeyStates);
        UpdateStates<Button> (ButtonStates);
    }

    // Updates state for a state collection.
    internal static void UpdateStates<TKey> (Dictionary<TKey, InputState> states) where TKey : notnull
    {
        foreach (var (key, value) in states)
        {
            states[key] = value switch
            {
                InputState.Released => InputState.Up,
                InputState.Pressed => InputState.Down,
                _ => value
            };
        }
    }

    #endregion
}
