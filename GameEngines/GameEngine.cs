namespace OtbornaIgra.GameEngines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Renderers;
    using Interfaces;
    using GameObjects;
    using Misc;
    using System.Windows.Threading;
    using System.Threading;

    public class GameEngine
    {
        private const int distanceFromBottomRowPad = 5; //na kakvo razstoqnie da e pada ot dunoto  
        public const int padWidth = 150;
        public const int padHeight = 15;
        public const int ballSize = 30;
        public const int brickWidth = ballSize*2;
        public const int brickHright = 20;
        private const double timerFramesIntervalInMiliSeconds = 10; //tova e speeda na igrata
        private IRenderer renderer;

        public GameObjects Pad { get; set; }

        public GameObjects Ball { get; set; }

        public IList<GameObjects> Bricks { get; set; }

        public GameEngine(IRenderer Renderer)
        {
            this.renderer = Renderer;
            this.renderer.presingkey += HandleKeyPressed;
        }

        private void HandleKeyPressed(object sender, KeyDownEventArgs key)
        {
            if (key.Command == GameComand.MoveLeft)
            {
                var left = this.Pad.Position.Left - 15;
                var top = this.Pad.Position.Top;
                this.Pad.Position = new Position(left, top);
            }

            else if (key.Command == GameComand.MoveRight)
            {
                var right = this.Pad.Position.Left + 15;
                var top = this.Pad.Position.Top;
                this.Pad.Position = new Position(right, top);
            }

            else if (key.Command == GameComand.Fire)
            {
            }
        }

        internal void InitGame()
        {
            int initPadLeftPosition = (this.renderer.ScreenWidth) / 2;
            int initPadTopPosition = ((this.renderer.ScreenHeight) - padHeight * distanceFromBottomRowPad);

            this.Pad = new PadGameObject()
            {
                Position = new Position(initPadLeftPosition, initPadTopPosition),   
                Bounds = new Size(padWidth, padHeight)
            };

            int initBallLeftPosition = (this.renderer.ScreenWidth) / 2;
            int initBallTopPosition = ((this.renderer.ScreenHeight) - padHeight * (distanceFromBottomRowPad) - padHeight);

            this.Ball = new BallGameObject()
            {
                Position = new Position(initBallLeftPosition,
                (initBallTopPosition)),
                Bounds = new Size(ballSize, ballSize)
            };

            int initBrickLeftPosition = 50;
            int initBrickTopPosition = 50;
            this.Bricks = new List<GameObjects>(15);
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Bricks.Add(new BricksGameObject()
                    {
                        Position = new Position(initBrickLeftPosition + i * brickWidth*2,
                    (initBrickTopPosition+j*brickHright*2)),
                        Bounds = new Size(brickWidth,brickHright)
                    });
                }
            }

        }

        internal void StartGame()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(timerFramesIntervalInMiliSeconds);
            timer.Tick += (sender, args) =>
             {
                 this.renderer.Clear();
                 this.renderer.Draw(this.Pad, this.Ball);
                 foreach (var item in this.Bricks)
                 {
                     this.renderer.Draw(item);
                 }

             };
            timer.Start();

        }
    }
}
