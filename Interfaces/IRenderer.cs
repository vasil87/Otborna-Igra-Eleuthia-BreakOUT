
namespace OtbornaIgra.Interfaces
{
    using System;
    using OtbornaIgra.GameObjects;
    using Misc;
    using Renderers;
    public interface IRenderer
    {

        int ScreenWidth { get; }
        int ScreenHeight { get; }
        void Clear();
        void Draw(params IGameObject[] drawObject);

        event KeyDownEventHander presingkey;

        bool isInBounds(Position position);

        void ShowStartGameScreen();

        void ShowEndGameScreen();

        void ShowWinGameScreen(string highScore);
    }
}
