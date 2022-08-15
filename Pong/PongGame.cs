using Eventide;
using Eventide.Input;

namespace Pong;

public class PongGame : Game
{
    private bool _multiplayer;

    internal void Start(bool multiplayer)
    {
        _multiplayer = multiplayer;
        Run ();
    }

    public override void OnInitialize ()
    {
        Window.SetTitle ("Pong with Eventide: Also try Terraria!");
        Window.SetSize (1280, 720);
        Window.CenterPosition ();
        Window.EnableVSync ();

        World.Spawn<UI> ();

        World.Spawn<Ball> ();

        World.Spawn<Paddle, PaddleConfig> (new PaddleConfig
        {
            XOffset = 10f,
            Color = Color.Blue,
            UpKey = Key.W,
            DownKey = Key.S,
            IsPlayer = true,
        });

        World.Spawn<Paddle, PaddleConfig> (new PaddleConfig
        {
            XOffset = -10f,
            Color = Color.Red,
            UpKey = Key.Up,
            DownKey = Key.Down,
            IsPlayer = _multiplayer,
        });
    }
}
