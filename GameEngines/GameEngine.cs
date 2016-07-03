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
    using Global;

    public class GameEngine

    {   
       

        private IRenderer renderer;

        public IGameObject Pad { get; set; }

        public IMovable Ball { get; set; }

        public List<IGameObject> Bricks { get; set; }

        public GameEngine(IRenderer Renderer)
        {  
            //zaka4am se za eventa presingkey i pri vsqko vikane na presingkey se vika handlekeypressed
            
            this.renderer = Renderer;
            this.renderer.presingkey += HandleKeyPressed; 
        }

        private void HandleKeyPressed(object sender, KeyDownEventArgs key)
            //metoda koito e zaka4en za eventa presingkey
        {
            if (key.Command == GameComand.MoveLeft)
            {
                var left = this.Pad.Position.Left - 15;
                var top = this.Pad.Position.Top;
                var newPositon = new Position(left, top);
                if (this.renderer.isInBounds(newPositon))
                 {
                    this.Pad.Position = newPositon;
                 }
            }

            else if (key.Command == GameComand.MoveRight)
            {
                var right = this.Pad.Position.Left + 15;
                var top = this.Pad.Position.Top;
                var newPositon = new Position(right, top);
                if (this.renderer.isInBounds(newPositon))
                {
                    this.Pad.Position = newPositon;
                }
                
            }

            else if (key.Command == GameComand.Fire)
            {
            }
        }

        internal void InitGame()
        {
            int initPadLeftPosition = (this.renderer.ScreenWidth) / 2;
            int initPadTopPosition = ((this.renderer.ScreenHeight) - GlobalConstants.padHeight * GlobalConstants.distanceFromBottomRowPad);

            this.Pad = new PadGameObject()
            {
                Position = new Position(initPadLeftPosition, initPadTopPosition),   
                Bounds = new Size(GlobalConstants.padWidth, GlobalConstants.padHeight)
            };

            int initBallLeftPosition = (this.renderer.ScreenWidth) / 2;
            int initBallTopPosition = ((this.renderer.ScreenHeight) - GlobalConstants.padHeight * (GlobalConstants.distanceFromBottomRowPad) - GlobalConstants.padHeight);

            this.Ball = new BallGameObject()
            {
                BallSpeed = new Position(-2,-3),
                Position = new Position(initBallLeftPosition,
                (initBallTopPosition)),
                Bounds = new Size(GlobalConstants.ballSize, GlobalConstants.ballSize)
            };

            int initBrickLeftPosition = 50;
            int initBrickTopPosition = 50;
            int brickRows = 6;
            int brickCows = 8;
             
             this.Bricks = new List<IGameObject>();
             for (int j = 0; j < brickRows; j++)
             {
                 for (int i = 0; i < brickCows; i++)
                 {
                     Bricks.Add(new BricksGameObject()
                     {
                         Position = new Position(initBrickLeftPosition + i * GlobalConstants.brickWidth * 2,
                     (initBrickTopPosition+j* GlobalConstants.brickHright * 2)),
                         Bounds = new Size(GlobalConstants.brickWidth, GlobalConstants.brickHright)
                     });
                 }
             }

        }

        internal void StartGame()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(GlobalConstants.timerFramesIntervalInMiliSeconds);
            timer.Tick += (sender, args) =>
            {
                //granici na ball za udar 
                var ballTop = this.Ball.Position.Top + (this.Ball.Bounds.Height / 2);
                var ballLeft = this.Ball.Position.Left + (this.Ball.Bounds.Width / 2);

                CheckForPadCollision(ballTop, ballLeft);

                CheckForBoundariesRebound(ballTop, ballLeft);

                CheckForColissions(ballTop, ballLeft);

                this.Bricks.RemoveAll(x => x.IsAlive == false);

                this.renderer.Clear();

                this.Ball.MoveWithCurrentSpeed(); //premestva topkata sys segashnata i skorost

                //drawvame obektite
                this.renderer.Draw(this.Pad, this.Ball);
                foreach (var brick in this.Bricks)
                {
                    if (brick.IsAlive == true)
                        this.renderer.Draw(brick);
                
                }

            };
            timer.Start();

        }

        private void CheckForPadCollision(int ballTop, int ballLeft)
        {
            //granici na pada
            var PadLeftUppersideLeft = Pad.Position.Left;
            var PadLeftUppersideTop = Pad.Position.Top;
            var PadRightUppersideLeft = Pad.Position.Left + Pad.Bounds.Width;
            var PadRightUppersideTop = PadLeftUppersideTop;

            //proverka za udur
            if ((ballTop - this.Ball.Bounds.Height) >= PadLeftUppersideTop && ballLeft + this.Ball.Bounds.Width <= PadRightUppersideLeft &&
            ballLeft >= PadLeftUppersideLeft)
            {

                var newLeftSpeed = this.Ball.BallSpeed.Left;
                var newRightSpeed = -this.Ball.BallSpeed.Top;
                this.Ball.BallSpeed = new Position(newLeftSpeed, newRightSpeed);
            }
        }

        private void CheckForColissions(int ballTop, int ballLeft)
        {
            foreach (var brick in this.Bricks)
            {   //granici na brickovete 
                var brickLeftBottomLeft = brick.Position.Left;
                var brickLeftBottomTop = brick.Position.Top - brick.Bounds.Width;
                var brickRightBottomLeft = brick.Position.Left + brick.Bounds.Width;
                var brickRightBottomTop = brickLeftBottomTop;

                //proverka za udur
                if (ballTop <= brickLeftBottomTop && ballLeft <= brickRightBottomLeft &&
                ballLeft >= brickLeftBottomLeft)
                {
                    brick.IsAlive = false;
                    var newLeftSpeed = -this.Ball.BallSpeed.Left;
                    var newRightSpeed = -this.Ball.BallSpeed.Top;
                    this.Ball.BallSpeed = new Position(newLeftSpeed, newRightSpeed);
                }

            }

           
        }

        private void CheckForBoundariesRebound(int ballTop, int ballLeft)
        {
            if (ballLeft <= 0) this.Ball.BallSpeed = new Position(-this.Ball.BallSpeed.Left, this.Ball.BallSpeed.Top);
            else if (ballLeft + this.Ball.Bounds.Width >= this.renderer.ScreenWidth)
                this.Ball.BallSpeed = new Position(-this.Ball.BallSpeed.Left, this.Ball.BallSpeed.Top);
            else if (ballTop <= 0)
                this.Ball.BallSpeed = new Position(this.Ball.BallSpeed.Left, -this.Ball.BallSpeed.Top);
            // else if(ballTop+this.Ball.Bounds.Height==this.renderer.ScreenHeight)
        }
    }
}
