using Eventide;
using Eventide.Content;

namespace Pong;

internal class UI : Entity
{
    private const float oneSecond = 1f;
    private const uint characterSize = 32;

    private Font _yellowSun;

    private Vector2 _rectangleSize;
    private Vector2 _rectanglePosition;

    private int _frames;
    private int _frameCounter;
    private float _deltaTimeCounter;
    private Vector2 _fpsPosition;

    private int[] _score;
    private Vector2[] _scorePositions;
    private Color[] _scoreColors;

    public void IncreaseScore (int index)
    {
        _score[index]++;
    }

    protected override void OnInitialize ()
    {
        _yellowSun = Content.LoadFont (@"Fonts\Yellow Sun");

        _rectangleSize = new (8f, Window.Height - 200f);
        _rectanglePosition = Window.Center;

        _frames = 0;
        _frameCounter = 0;
        _deltaTimeCounter = 0f;
        _fpsPosition = new (Window.Width / 2, 14f);

        _score = new int[2];
        _scorePositions = new Vector2[] { new (60f, 14f), new (Window.Width - 60f, 14f) };
        _scoreColors = new Color[] { Color.Blue, Color.Red };
    }

    protected override void OnUpdate ()
    {
        _frameCounter++;
        _deltaTimeCounter += Time.DeltaTime;
        if (_deltaTimeCounter >= oneSecond)
        {
            _frames = _frameCounter;
            _frameCounter = 0;
            _deltaTimeCounter -= oneSecond;
        }
    }

    protected override void OnDraw ()
    {
        Window.DrawRectangle (_rectangleSize, _rectanglePosition, Color.Gray);
        Window.DrawText (_fpsPosition, $"FPS: {_frames}", _yellowSun, characterSize, Color.Magenta, Color.White, 2f, 3f, 1f, 0f);
        for (int i = 0; i < 2; i++)
        {
            Window.DrawText (_scorePositions[i], _score[i].ToString (), _yellowSun, characterSize, _scoreColors[i], Color.White, 2f, 3f, 1f, 0f);
        }
    }
}
