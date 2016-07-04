using OtbornaIgra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtbornaIgra.Collision_Detection
{
    public static class CollisionDetector
    {   
        public static List<List<MatrixCoords>> staticOjectCoords;
        public static void GetBoundariesForStaticObjects(List<IGameObject> staticObjects)
        {
            staticOjectCoords = new List<List<MatrixCoords>>();

            for (int p = 0; p < staticObjects.Count(); p++)
            {
                staticOjectCoords.Add(new List<MatrixCoords>());
                for (int i = 0; i <= staticObjects[p].Bounds.Height; i++)
                {
                    for (int j = 0; j <= staticObjects[p].Bounds.Width; j++)
                    {
                        if (i != 0 && i != staticObjects[p].Bounds.Height && j != 0 && j != staticObjects[p].Bounds.Width ) continue;

                        staticOjectCoords[p].Add(new MatrixCoords(staticObjects[p].Position.Top + i, staticObjects[p].Position.Left + j));

                    }

                }
            }

        }
        public static void CheckForCollisionWithBricks(IMovable movingObject, List<IGameObject> staticObjects)
        {

            List<MatrixCoords> movingObjectCoords = new List<MatrixCoords>();
   
            for (int i = 0; i < movingObject.Bounds.Height; i=i+1)
            {
                for (int j = 0; j < movingObject.Bounds.Width; j=j+1)
                {
                    if (i != 0 && i != movingObject.Bounds.Height-1 && j != 0 && j != movingObject.Bounds.Width) continue;

                    movingObjectCoords.Add
                        (new MatrixCoords(movingObject.Position.Top + i, movingObject.Position.Left + j));
                }
            }

            CollisionData posibleCollision = CheckWhichBrickWhichSide(movingObjectCoords, staticObjects);

            if (posibleCollision.CollideteStaticElementIndex != -1)
            {
                if (posibleCollision.Side == HitType.horizontal)
                    movingObject.Speed = new GameObjects.Position(movingObject.Speed.Left, -movingObject.Speed.Top);
                else { movingObject.Speed = new GameObjects.Position(-movingObject.Speed.Left, movingObject.Speed.Top); }
                staticObjects[posibleCollision.CollideteStaticElementIndex].IsAlive = false;
                staticOjectCoords.RemoveAt(posibleCollision.CollideteStaticElementIndex);
            }
             

            

        }

        public static CollisionData CheckWhichBrickWhichSide(List<MatrixCoords> movingObjectCoords, List<IGameObject> staticObjects)
        {
            HitType side=HitType.horizontal;
            for (int k = 0; k < movingObjectCoords.Count; k++)
            {
                for (int i = 0; i < staticObjects.Count; i++)
                {

                    for (int p = 0; p < staticOjectCoords[i].Count; p = p + 5)
                    {
                        if (movingObjectCoords[k] == staticOjectCoords[i][p])
                        {
                            if (p >= 0 && p < 60 || p >= 80 && p < 140) side = HitType.horizontal;
                            else { side = HitType.vertical; }
                            return new CollisionData(i, side);

                        }


                    }

                }

            }
            return new CollisionData(-1, side);
        }
        }
    }


