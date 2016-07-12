namespace OtbornaIgra.GameEngines
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using GameObjects;
    using Misc;
    using System.Windows.Threading;
    using Global;
    using Collision_Detection;
    using System.Linq;
    public class GameEngine:IGameEngine

    {
        private ICollection<IRebouncable> rebouncableObjects;
        private ICollection<IMovable> movingObjects;
        private List<IDestroyable> bricks;
        private IRebouncable Pad;
        private IMovable Ball;
        private DispatcherTimer timer;

        private IRenderer renderer;
        public ICollection<IRebouncable> RebouncableObjects
        {
            get { return this.rebouncableObjects; }
        }
        public ICollection<IMovable> MovingObjects
        {
            get { return this.movingObjects; }
        }
        public List<IDestroyable> Bricks
        {
            get { return this.bricks; }
        }
        public ITextGameObject HighScore { get; set; }
        public GameEngine(IRenderer Renderer)
        {
            //zaka4am se za eventa presingkey i pri vsqko vikane na presingkey se vika handlekeypressed
            timer = new DispatcherTimer();
            this.renderer = Renderer;
            this.renderer.presingkey += HandleKeyPressed; 
           
           
        }

        private void HandleKeyPressed(object sender, KeyDownEventArgs key)
        //metoda koito e zaka4en za eventa presingkey
        {
            try {
                if (key.Command == GameComand.MoveLeft)
                {
                    var Speed = GameObjectsFactory.GenerateNewPosition(GlobalConstants.PadSpeedLeft, GlobalConstants.PadSpeedTop);
                    var newPositon = GameObjectsFactory.GenerateNewPosition(Speed, this.Pad);
                    if (this.renderer.isInBounds(newPositon))
                    {
                        this.Pad.Position = newPositon;
                    }
                }

                else if (key.Command == GameComand.MoveRight)
                {
                    var Speed = GameObjectsFactory.GenerateNewPosition(-GlobalConstants.PadSpeedLeft, GlobalConstants.PadSpeedTop);
                    var newPositon = GameObjectsFactory.GenerateNewPosition(Speed, this.Pad);
                    if (this.renderer.isInBounds(newPositon))
                    {
                        this.Pad.Position = newPositon;
                    }

                }

                else if (key.Command == GameComand.Pause)
                {
                    if (this.timer.IsEnabled)
                        this.timer.Stop();
                    else { this.timer.Start(); }

                }

                else
                {
                    throw new WrongKeyException("Wrong key Pressed");
                }
            }
            catch (WrongKeyException ex)
            {
                var position = GameObjectsFactory.GenerateNewPosition(GlobalConstants.msgLeftPosition, GlobalConstants.msgTopPosition);
                var size = GameObjectsFactory.GenerateNewSize(GlobalConstants.msgWidth, GlobalConstants.msgHeight);
                var wrongKeyExceptionObject = GameObjectsFactory.GenerateNewErrorText(position, size, ex.Message);
                this.renderer.Draw(wrongKeyExceptionObject);
            }
        }
        
        //incializiram vsi4ki obekti tuk i vkarvam v colectiite ,sled koeto presmqta lista s coordinati
    public void InitGame()
        {
            this.renderer.ShowStartGameScreen();


            //int Pad
            this.rebouncableObjects = new List<IRebouncable>();
            int initPadLeftPosition = (this.renderer.ScreenWidth) / 2 - GlobalConstants.padWidth / 2;
            int initPadTopPosition = ((this.renderer.ScreenHeight) - GlobalConstants.padHeight*2);
            var padPosition = GameObjectsFactory.GenerateNewPosition(initPadLeftPosition, initPadTopPosition);
            var padSize = GameObjectsFactory.GenerateNewSize(GlobalConstants.padWidth, GlobalConstants.padHeight);
            this.Pad = GameObjectsFactory.GeneratePad(padPosition, padSize);
            this.rebouncableObjects.Add(this.Pad);

            //int Ball
            this.movingObjects = new List<IMovable>();
            int initBallLeftPosition = (this.renderer.ScreenWidth) / 2;
            int initBallTopPosition = ((this.renderer.ScreenHeight) - GlobalConstants.padHeight * (GlobalConstants.distanceFromBottomRowPad) - GlobalConstants.padHeight-GlobalConstants.ballSize);
            var ballSpeed = GameObjectsFactory.GenerateNewPosition(GlobalConstants.BallInitSpeedLeft,GlobalConstants.BallInitSpeedTop);
            var ballPosition = GameObjectsFactory.GenerateNewPosition(initBallLeftPosition,(initBallTopPosition));
            var ballSize = GameObjectsFactory.GenerateNewSize(GlobalConstants.ballSize, GlobalConstants.ballSize);
            this.Ball = GameObjectsFactory.GenerateNewBall(ballPosition, ballSize, ballSpeed);
            this.movingObjects.Add(this.Ball);

            //init HighScore
            int initHighScoreLeftPosition = (this.renderer.ScreenWidth) - GlobalConstants.highScoreLeft;
            int initHighScorePosition = GlobalConstants.distanceFromBottomRowPad;
            var highScorePosition = GameObjectsFactory.GenerateNewPosition(initHighScoreLeftPosition, initHighScorePosition);
            var highScoreSize = GameObjectsFactory.GenerateNewSize(GlobalConstants.highScoreWidht, GlobalConstants.highScoreHeight);
            this.HighScore = GameObjectsFactory.GenerateNewHighScore(highScorePosition, highScoreSize);


            //init Bricks
            this.bricks = new List<IDestroyable>();
            for (int j = 0; j < GlobalConstants.brickRows; j++)
             {
                 for (int i = 0; i < GlobalConstants.brickCows; i++)
                 {

                    var brickPosition = GameObjectsFactory.GenerateNewPosition(GlobalConstants.initBrickLeftPosition + i * GlobalConstants.brickWidth * 2,
                        (GlobalConstants.initBrickTopPosition + j * GlobalConstants.brickHright * 2));
                    var brickSize = GameObjectsFactory.GenerateNewSize(GlobalConstants.brickWidth, GlobalConstants.brickHright);
                    var brick = GameObjectsFactory.GenerateNewBrick(brickPosition, brickSize);
                    Bricks.Add(brick);
                 }
            }

            CollisionDetector.GetBoundariesForDestroiableObjects(this.Bricks);

        }

        //cikula na igrata
        public void StartGame()
        {
            
            timer.Interval = TimeSpan.FromMilliseconds(GlobalConstants.timerFramesIntervalInMiliSeconds);
            timer.Tick += (sender, args) =>
            {

                CollisionDetector.CheckForRebounce(this.rebouncableObjects,this.movingObjects );

                var screenSize = GameObjectsFactory.GenerateNewSize(this.renderer.ScreenWidth, this.renderer.ScreenHeight);

                if(CollisionDetector.CheckForBoundariesRebound(movingObjects, screenSize))
                {
                    timer.Stop();
                    this.renderer.ShowEndGameScreen();
                }

                //proverka za udar i smqna na skorosta i posokata pri udar //maha udarenite ot bricks
                foreach (var movingObject in this.movingObjects)
                {  
                   var posibleCollision= CollisionDetector.CheckForCollisionWithBricks(movingObject, this.Bricks, screenSize);
                    if (posibleCollision.CollideteStaticElementIndex != -1)
                    {
                        if (posibleCollision.Side == HitTypeEnum.horizontal)
                            movingObject.Speed = GameObjectsFactory.GenerateNewPosition(movingObject.Speed.Left, -movingObject.Speed.Top);
                        else { movingObject.Speed = GameObjectsFactory.GenerateNewPosition(-movingObject.Speed.Left, movingObject.Speed.Top); }
                        this.bricks[posibleCollision.CollideteStaticElementIndex].DestroyMe();
                        

                    }

                }

                //za vsqko ubito increase na highscore 

                int score = 0;

                foreach (var brick in this.bricks)
                {
                    if (brick.IsAlive == false)
                    {
                        score += brick.PointsForBraking;
                    }
                }
               
                (this.HighScore as HighScore).IncreaseHighScore(score);

                this.bricks.RemoveAll(x => x.IsAlive == false);

                if (this.bricks.Count == 0)
                {
                    timer.Stop();
                    this.renderer.ShowWinGameScreen(HighScore.Text);
                }
               
                this.renderer.Clear();

                foreach (var bal in movingObjects)
                {
                    bal.MoveWithCurrentSpeed(); //premestva topkata sys segashnata i skorost
                }

                //draw bricks,pad and ball
                this.renderer.Draw(this.HighScore);
                this.renderer.Draw(this.RebouncableObjects.ToArray());
                this.renderer.Draw(this.movingObjects.ToArray()); 
                this.renderer.Draw(this.bricks.Where(x => x.IsAlive == true).ToArray());
            };
            timer.Start();
        }
    }
}
