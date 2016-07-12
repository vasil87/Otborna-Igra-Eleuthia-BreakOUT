using OtbornaIgra.GameObjects;
using OtbornaIgra.Global;
using OtbornaIgra.Interfaces;
using System;
using System.Collections.Generic;


namespace OtbornaIgra.Collision_Detection
{
    using System.Linq;
 
    public static class CollisionDetector   //statichen klass sys metodi za proverka za kolizii
    {   //coord na vsi4ki obekti za Chupene
        private static List<List<MatrixCoords>> staticOjectCoords;
        //tyk se populvat tezi koordinati na staticOjectCoords
        public static void GetBoundariesForDestroiableObjects(IList<IDestroyable> staticObjects)
        {
            
            staticOjectCoords = new List<List<MatrixCoords>>();

            for (int p = 0; p < staticObjects.Count(); p++)
            {
                staticOjectCoords.Add(new List<MatrixCoords>());
                for (int i = 0; i <= staticObjects[p].Bounds.Height; i=i+2)
                {
                    for (int j = 0; j <= staticObjects[p].Bounds.Width; j=j+2)
                    {
                        if (i != 0 && i != staticObjects[p].Bounds.Height && j != 0 && j != staticObjects[p].Bounds.Width ) continue;

                        staticOjectCoords[p].Add(new MatrixCoords(staticObjects[p].Position.Top + i, staticObjects[p].Position.Left + j));

                    }

                }
            }

        }

        //tuk se smenq posokata na dvijenie na movingObjecta
        public static CollisionData CheckForCollisionWithBricks(IMovable movingObject, IList<IDestroyable> staticObjects,Size screenSize)
        {

            if (movingObject.Position.Top >= screenSize.Height / 2) return new CollisionData(-1, HitTypeEnum.horizontal);
            List<MatrixCoords> movingObjectCoords = new List<MatrixCoords>();//here saves the moving ball coords

            MovingObjectCoordinates(movingObject, movingObjectCoords); //get the coordinates of the moving ball 
                                   //The real check for collision if no collision returns -1    
            var posibleCollision = CheckWhichBrickWhichSide(movingObjectCoords, staticObjects);
            if (posibleCollision.CollideteStaticElementIndex != -1)
            {
                staticOjectCoords.RemoveAt(posibleCollision.CollideteStaticElementIndex);
            }
                                              
            return posibleCollision;

        }

        private static void MovingObjectCoordinates(IMovable movingObject, List<MatrixCoords> movingObjectCoords)
        {
            for (int i = 0; i < movingObject.Bounds.Height; i = i + 1)
            {
                for (int j = 0; j < movingObject.Bounds.Width; j = j + 1)
                {
                    if (i != 0 && i != movingObject.Bounds.Height - 1 && j != 0 && j != movingObject.Bounds.Width) continue;

                    movingObjectCoords.Add
                        (new MatrixCoords(movingObject.Position.Top + i, movingObject.Position.Left + j));
                }
            }
        }

        private static CollisionData CheckWhichBrickWhichSide(List<MatrixCoords> movingObjectCoords, ICollection<IDestroyable> staticObjects)
        {
            HitTypeEnum side=HitTypeEnum.horizontal;
            for (int k = 0; k < movingObjectCoords.Count; k++)
            {
                for (int i = 0; i < staticObjects.Count; i++)
                {

                    for (int p = 0; p < staticOjectCoords[i].Count; p = p + 5)
                    {
                        if (movingObjectCoords[k] == staticOjectCoords[i][p])
                        {
                            if (p >= 0 && p < 60 || p >= 80 && p < 140) side = HitTypeEnum.horizontal;
                            else { side = HitTypeEnum.vertical; }
                            return new CollisionData(i, side);

                        }


                    }

                }

            }
            return new CollisionData(-1, side);
        }

        public static void CheckForRebounce(ICollection<IRebouncable> rebouncable,ICollection<IMovable> movable )
        {
            foreach (var Ball in movable)
            {  
                //granici na moveingObj za udar 
                var ballTop = Ball.Position.Top;
                var ballLeft = Ball.Position.Left;
                var ballBottom = Ball.Position.Top + (Ball.Bounds.Height);
                var ballRight = ballLeft + Ball.Bounds.Width;
                foreach (var Pad in rebouncable)
                {  //granici na pada
                    var PadLeftUppersideLeft = Pad.Position.Left;
                    var PadLeftUppersideTop = Pad.Position.Top;
                    var PadRightUppersideLeft = Pad.Position.Left + Pad.Bounds.Width;
                    var PadRightUppersideTop = PadLeftUppersideTop;
                    //proverka za udur

                    if ((ballBottom) == PadLeftUppersideTop && ((ballLeft <= PadRightUppersideLeft &&
                       ballRight > PadRightUppersideLeft) || (ballRight < PadRightUppersideLeft &&
                       ballLeft > PadLeftUppersideLeft) || (ballRight >= PadLeftUppersideLeft &&
                       ballLeft < PadLeftUppersideLeft)))
                    {
                        int newLeftSpeed;
                        int newRightSpeed;

                        if (ballRight <= (PadRightUppersideLeft - Pad.Bounds.Width / 4) && ballLeft >= PadLeftUppersideLeft + (Pad.Bounds.Width) / 4)
                        {
                            newLeftSpeed = Ball.Speed.Left / 2;
                            newRightSpeed = -Ball.Speed.Top;
                            Ball.Speed = GameObjectsFactory.GenerateNewPosition(newLeftSpeed, newRightSpeed);

                        }

                        else {
                            if (Ball.Speed.Left == 0)
                            {
                                if ((ballLeft + Ball.Bounds.Width / 2) <= PadLeftUppersideLeft + Pad.Bounds.Width / 2)
                                    newLeftSpeed = GlobalConstants.BallInitSpeedLeft;
                                else { newLeftSpeed = (-GlobalConstants.BallInitSpeedLeft); }
                            }
                            else { newLeftSpeed = Ball.Speed.Left < 0 ? GlobalConstants.BallInitSpeedLeft : (-GlobalConstants.BallInitSpeedLeft); }
                            newRightSpeed = -Ball.Speed.Top;
                            Ball.Speed = GameObjectsFactory.GenerateNewPosition(newLeftSpeed, newRightSpeed);
                          
                        }
                    }
                }
            }
          

        }

        public static bool CheckForBoundariesRebound(ICollection<IMovable> movingObjs,Size screen)
        {
            bool GameOverCondition = false;
            foreach (var Ball in movingObjs)
            {
                //granici na moveingObj za udar 
                var ballTop = Ball.Position.Top;
                var ballLeft = Ball.Position.Left;
                var ballBottom = Ball.Position.Top + (Ball.Bounds.Height);
                var ballRight = ballLeft + Ball.Bounds.Width;

                if (ballLeft <= 0)
                    Ball.Speed = new Position(-Ball.Speed.Left, Ball.Speed.Top);
                else if (ballLeft + Ball.Bounds.Width >= screen.Width - 10)
                    Ball.Speed = new Position(-Ball.Speed.Left, Ball.Speed.Top);
                else if (ballTop <= 0)
                    Ball.Speed = new Position(Ball.Speed.Left, -Ball.Speed.Top);

                else if (ballTop >= screen.Height)
                {
                    GameOverCondition = true;
                }
               
            }
            return GameOverCondition;
        }
    }
    }


