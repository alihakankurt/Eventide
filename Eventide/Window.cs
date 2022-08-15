namespace Eventide;

/// <summary>
/// The window class.
/// </summary>
public sealed class Window : ObjectBase
{
    #region Fields

    // The game that window serves.
    private readonly Game _game;

    // The texture to use in display method from native library.
    private readonly SFMLRenderTexture _texture;

    // The text instance to use in draw method from native library.
    private readonly SFMLText _text;

    // The rectangle instance to use in draw method from native library.
    private readonly SFMLRectangle _rectangle;

    // The circle instance to use in draw method from native library.
    private readonly SFMLCircle _circle;

    // The render window from native library.
    private SFMLRenderWindow _window;

    // The title of the window.
    private string _title;

    // The width of the window.
    private uint _width;

    // The height of the window.
    private uint _height;

    // The position in X-plane of the window.
    private int _x;

    // The position in Y-plane of the window.
    private int _y;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the primary screen size.
    /// </summary>
    public static Vector2 ScreenSize => new (VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height);

    /// <summary>
    /// Gets the title of the <see cref="Window"/>.
    /// </summary>
    public string Title => _title;

    /// <summary>
    /// Gets the width of the <see cref="Window"/>.
    /// </summary>
    public uint Width => _width;

    /// <summary>
    /// Gets the height of the <see cref="Window"/>.
    /// </summary>
    public uint Height => _height;

    /// <summary>
    /// Gets the center point of the <see cref="Window"/>.
    /// </summary>
    public Vector2 Center => new (_width / 2, _height / 2);

    /// <summary>
    /// Gets the position in X-plane of the <see cref="Window"/>.
    /// </summary>
    public int X => _x;

    /// <summary>
    /// Gets the position in Y-plane of the <see cref="Window"/>.
    /// </summary>
    public int Y => _y;

    /// <summary>
    /// Gets the whether <see cref="Window"/> is open.
    /// </summary>
    public bool Running => _window.IsOpen;

    #endregion

    #region Constructors

    // Initializes a new instance of window.
    internal Window (Game game)
    {
        _game = game;
        _title = "Eventide";
        _width = 1280;
        _height = 720;
        _x = 320;
        _y = 230;

        _texture = new (_width, _height);
        _text = new ();
        _rectangle = new ();
        _circle = new ();
        
        _window = new (new (_width, _height), _title, Styles.Close)
        {
            Position = new (_x, _y),
        };
        AssignEvents ();
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy (bool disposing)
    {
        _rectangle.Dispose ();
        _circle.Dispose ();
        _texture.Dispose ();
        _window.Dispose ();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets the title of the <see cref="Window"/>.
    /// </summary>
    /// <param name="title">The new title.</param>
    public void SetTitle (string title)
    {
        _title = title;

        _window.SetTitle (_title);
    }

    /// <summary>
    /// Sets the size of the <see cref="Window"/>.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    public void SetSize (uint width, uint height)
    {
        _width = width;
        _height = height;

        _window.Size = new (_width, _height);
        UpdateView (v => v.Size = new (_width, _height));
    }

    /// <summary>
    /// Sets the position of the <see cref="Window"/>.
    /// </summary>
    /// <param name="x">The new X-position.</param>
    /// <param name="y">The new Y-position.</param>
    public void SetPosition (int x, int y)
    {
        _x = x;
        _y = y;

        _window.Position = new Vector2i (_x, _y);
    }

    /// <summary>
    /// Sets the frame limit of the <see cref="Window"/>.
    /// </summary>
    /// <param name="frameLimit">The ew frame limit. Set it to 0 to disable.</param>
    public void SetFrameLimit (uint frameLimit)
    {
        _window.SetFramerateLimit (frameLimit);
    }

    /// <summary>
    /// Enables the vertical synchronization of the <see cref="Window"/>.
    /// </summary>
    public void EnableVSync ()
    {
        _window.SetVerticalSyncEnabled (true);
    }

    /// <summary>
    /// Disables the vertical synchronization of the <see cref="Window"/>.
    /// </summary>
    public void DisableVSync ()
    {
        _window.SetVerticalSyncEnabled (false);
    }

    /// <summary>
    /// Shows the <see cref="Window"/>.
    /// </summary>
    public void Show ()
    {
        _window.SetVisible (true);
    }

    /// <summary>
    /// Hides the <see cref="Window"/>.
    /// </summary>
    public void Hide ()
    {
        _window.SetVisible (false);
    }

    /// <summary>
    /// Closes the <see cref="Window"/>.
    /// </summary>
    public void Close ()
    {
        _window.Close ();
    }

    /// <summary>
    /// Shows the cursor of the <see cref="Window"/>.
    /// </summary>
    public void ShorCursor ()
    {
        _window.SetMouseCursorVisible (true);
    }

    /// <summary>
    /// Hides the cursor of the <see cref="Window"/>.
    /// </summary>
    public void HideCursor ()
    {
        _window.SetMouseCursorVisible (false);
    }

    public void GoFullscreen ()
    {
        _window.Close ();
        _window = new (VideoMode.FullscreenModes[0], _title, Styles.Fullscreen);
        AssignEvents ();
    }

    public void GoWindowed ()
    {
        _window.Close ();
        _window = new (new (_width, _height), _title, Styles.Default);
        AssignEvents ();
    }

    /// <summary>
    /// Centers the position of the <see cref="Window"/>.
    /// </summary>
    public void CenterPosition ()
    {
        SetPosition ((int)((ScreenSize.X - Width) / 2), (int)((ScreenSize.Y - Height) / 2));
    }

    /// <summary>
    /// Draws <paramref name="sprite"/> to the window.
    /// </summary>
    /// <param name="sprite">The sprite to draw.</param>
    /// <param name="position">The center point of the sprite.</param>
    public void Draw (Sprite? sprite, Vector2 position)
    {
        Draw (sprite, position, Vector2.One, 0f);
    }

    /// <summary>
    /// Draws the <paramref name="sprite"/> to the window.
    /// </summary>
    /// <param name="sprite">The sprite to draw.</param>
    /// <param name="position">The center point of the sprite.</param>
    /// <param name="scale">The scale of the sprite.</param>
    public void Draw (Sprite? sprite, Vector2 position, Vector2 scale)
    {
        Draw (sprite, position, scale, 0f);
    }

    /// <summary>
    /// Draws the <paramref name="sprite"/> to window.
    /// </summary>
    /// <param name="sprite">The sprite to draw.</param>
    /// <param name="position">The center point of the sprite.</param>
    /// <param name="scale">The scale of the sprite.</param>
    /// <param name="rotation">The rotation angle of the sprite.</param>
    public void Draw (Sprite? sprite, Vector2 position, Vector2 scale, float rotation)
    {
        if (sprite == null)
            return;

        sprite.Native.Position = new (position.X, position.Y);
        sprite.Native.Scale = new (scale.X, scale.Y);
        sprite.Native.Rotation = -rotation;
        sprite.Native.Origin = new (sprite.Width / 2, sprite.Height / 2);
        _texture.Draw (sprite.Native);
    }

    /// <summary>
    /// Draws a white text to the window.
    /// </summary>
    /// <param name="position">The center point of the text.</param>
    /// <param name="text">The string to display.</param>
    /// <param name="font">The font of the text.</param>
    /// <param name="characterSize">The character size of the text.</param>
    public void DrawText (Vector2 position, string text, Font font, uint characterSize)
    {
        DrawText (position, text, font, characterSize, Color.White, Color.Transparent, 0f, 1f, 1f, 0f);
    }

    /// <summary>
    /// Draws a text to the window.
    /// </summary>
    /// <param name="position">The center point of the text.</param>
    /// <param name="text">The string to display.</param>
    /// <param name="font">The font of the text.</param>
    /// <param name="characterSize">The character size of the text.</param>
    /// <param name="color">The color of the text.</param>
    public void DrawText (Vector2 position, string text, Font font, uint characterSize, Color color)
    {
        DrawText (position, text, font, characterSize, color, Color.Transparent, 0f, 1f, 1f, 0f);
    }

    /// <summary>
    /// Draws a text to the window.
    /// </summary>
    /// <param name="position">The center point of the text.</param>
    /// <param name="text">The string to display.</param>
    /// <param name="font">The font of the text.</param>
    /// <param name="characterSize">The character size of the text.</param>
    /// <param name="color">The color of the text.</param>
    /// <param name="outlineColor">The outline color of the text.</param>
    /// <param name="outlineThickness">The outline thickness of the text.</param>
    public void DrawText (Vector2 position, string text, Font font, uint characterSize, Color color, Color outlineColor, float outlineThickness)
    {
        DrawText (position, text, font, characterSize, color, outlineColor, outlineThickness, 1f, 1f, 0f);
    }

    /// <summary>
    /// Draws a text to the window.
    /// </summary>
    /// <param name="position">The center point of the text.</param>
    /// <param name="text">The string to display.</param>
    /// <param name="font">The font of the text.</param>
    /// <param name="characterSize">The character size of the text.</param>
    /// <param name="color">The color of the text.</param>
    /// <param name="outlineColor">The outline color of the text.</param>
    /// <param name="outlineThickness">The outline thickness of the text.</param>
    /// <param name="letterSpacing">The spacing between letters.</param>
    /// <param name="lineSpacing">The spacing between lines.</param>
    public void DrawText (Vector2 position, string text, Font font, uint characterSize, Color color, Color outlineColor, float outlineThickness, float letterSpacing, float lineSpacing)
    {
        DrawText (position, text, font, characterSize, color, outlineColor, outlineThickness, letterSpacing, lineSpacing, 0f);
    }

    /// <summary>
    /// Draws a text to the window.
    /// </summary>
    /// <param name="position">The center point of the text.</param>
    /// <param name="text">The string to display.</param>
    /// <param name="font">The font of the text.</param>
    /// <param name="characterSize">The character size of the text.</param>
    /// <param name="color">The color of the text.</param>
    /// <param name="outlineColor">The outline color of the text.</param>
    /// <param name="outlineThickness">The outline thickness of the text.</param>
    /// <param name="letterSpacing">The spacing factor between letters.</param>
    /// <param name="lineSpacing">The spacing factor between lines.</param>
    /// <param name="rotation">The rotation angle of the text.</param>
    public void DrawText (Vector2 position, string text, Font font, uint characterSize, Color color, Color outlineColor, float outlineThickness, float letterSpacing, float lineSpacing, float rotation)
    {
        if (string.IsNullOrEmpty (text))
            return;

        _text.Position = new (position.X, position.Y);
        _text.DisplayedString = text;
        _text.Font = font.Native;
        _text.CharacterSize = characterSize;
        _text.FillColor = new (color.RawValueRGBA);
        _text.OutlineColor = new (outlineColor.RawValueRGBA);
        _text.OutlineThickness = outlineThickness;
        _text.LetterSpacing = letterSpacing;
        _text.LineSpacing = lineSpacing;
        _text.Rotation = -rotation;
        var localBounds = _text.GetLocalBounds ();
        _text.Origin = new (localBounds.Width / 2, localBounds.Height / 2);
        _texture.Draw (_text);
    }

    /// <summary>
    /// Draws a white rectangle to the window.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="position">The center point of the rectangle.</param>
    public void DrawRectangle (Vector2 size, Vector2 position)
    {
        DrawRectangle (size, position, Color.White, Color.Transparent, 0f);
    }

    /// <summary>
    /// Draws a rectangle to the window.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="position">The center point of the rectangle.</param>
    /// <param name="color">The fill color of the rectangle.</param>
    public void DrawRectangle (Vector2 size, Vector2 position, Color color)
    {
        DrawRectangle (size, position, color, Color.Transparent, 0f);
    }

    /// <summary>
    /// Draws a rectangle to the window.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="position">The center point of the rectangle.</param>
    /// <param name="color">The fill color of the rectangle.</param>
    /// <param name="outlineColor">The outline color of the rectangle.</param>
    /// <param name="outlineThickness">The outline thicknes of the rectangle.</param>
    public void DrawRectangle (Vector2 size, Vector2 position, Color color, Color outlineColor, float outlineThickness)
    {
        _rectangle.Size = new (size.X, size.Y);
        _rectangle.Position = new (position.X, position.Y);
        _rectangle.FillColor = new (color.RawValueRGBA);
        _rectangle.OutlineColor = new (outlineColor.RawValueRGBA);
        _rectangle.OutlineThickness = outlineThickness;
        _rectangle.Origin = new (size.X / 2, size.Y / 2);
        _texture.Draw (_rectangle);
    }

    /// <summary>
    /// Draws a white circle to the window.
    /// </summary>
    /// <param name="position">The center position of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    public void DrawCircle (Vector2 position, float radius)
    {
        DrawCircle (position, radius, Color.White, Color.Transparent, 0f);
    }

    /// <summary>
    /// Draws a circle to the window.
    /// </summary>
    /// <param name="position">The center position of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The fill color of the circle.</param>
    public void DrawCircle (Vector2 position, float radius, Color color)
    {
        DrawCircle (position, radius, color, Color.Transparent, 0f);
    }

    /// <summary>
    /// Draws a circle to the window.
    /// </summary>
    /// <param name="position">The center position of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The fill color of the circle.</param>
    /// <param name="outlineColor">The outline color of the circle.</param>
    /// <param name="outlineThickness">The outline thickness of the circle.</param>
    public void DrawCircle (Vector2 position, float radius, Color color, Color outlineColor, float outlineThickness)
    {
        _circle.Position = new (position.X, position.Y);
        _circle.Radius = radius;
        _circle.FillColor = new (color.RawValueRGBA);
        _circle.OutlineColor = new (outlineColor.RawValueRGBA);
        _circle.OutlineThickness = outlineThickness;
        var bounds = _circle.GetLocalBounds ();
        _circle.Origin = new (bounds.Width / 2, bounds.Height / 2);
        _texture.Draw (_circle);
    }

    // Assign window events.
    internal void AssignEvents ()
    {
        _window.Closed += (_, _) => Close ();
        _window.Resized += (_, e) => SetSize (e.Width, e.Height);
        _window.KeyReleased += (_, e) => _game.Input.ReleaseKey ((Key)(int)(e.Code + 1));
        _window.KeyPressed += (_, e) => _game.Input.PressKey ((Key)(int)(e.Code + 1));
        _window.MouseButtonReleased += (_, e) => _game.Input.ReleaseButton ((Button)(int)(e.Button + 1));
        _window.MouseButtonPressed += (_, e) => _game.Input.PressButton ((Button)(int)(e.Button + 1));
        _window.MouseMoved += (_, e) => _game.Input.MouseMove (e.X, e.Y);
        _window.MouseWheelScrolled += (_, e) => _game.Input.MouseWheel (e.Delta);
    }

    // Dispatches the window events.
    internal void DispatchEvents ()
    {
        _window.DispatchEvents ();
    }


    // Clears the buffer.
    internal void Clear ()
    {
        _texture.Clear ();
        _window.Clear ();
    }

    // Displays the buffer.
    internal void Display ()
    {
        _texture.Display ();
        using SFMLSprite sprite = new (_texture.Texture);
        _window.Draw (sprite);
        _window.Display ();
    }

    // Updates view of the window.
    internal void UpdateView (Action<SFMLView> updateAction)
    {
        var view = _window.DefaultView;
        updateAction (view);
        _window.SetView (view);
    }

    #endregion
}
