using OtbornaIgra.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtbornaIgra.CollisionDetector
{
    public class CollisionData
    {
        public readonly Position CollisionForceDirection;
        public readonly List<string> hitObjectsCollisionGroupStrings;

        public CollisionData(Position collisionForceDirection, string objectCollisionGroupString)
        {
            this.CollisionForceDirection = collisionForceDirection;
            this.hitObjectsCollisionGroupStrings = new List<string>();
            this.hitObjectsCollisionGroupStrings.Add(objectCollisionGroupString);
        }

        public CollisionData(Position collisionForceDirection, List<string> hitObjectsCollisionGroupStrings)

        {
            this.CollisionForceDirection = collisionForceDirection;

            this.hitObjectsCollisionGroupStrings = new List<string>();

            foreach (var str in hitObjectsCollisionGroupStrings)
            {
                this.hitObjectsCollisionGroupStrings.Add(str);
            }
        }
    }
}
