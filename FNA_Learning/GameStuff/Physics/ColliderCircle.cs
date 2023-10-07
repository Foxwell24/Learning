using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.GameStuff.Physics
{
    internal class ColliderCircle
    {
        public float radius { get; private set; }
        public Vector2 position { get; private set; }

        public ColliderCircle(Vector2 position, float radius)
        {
            this.position = position;
            this.radius = radius;
        }

        public bool IsCollidingCircle(ColliderCircle other) => Vector2.Distance(position, other.position) <= radius + other.radius;
        //public bool IsCollidingRect(ColliderRect other) => other.collision.Contains(position.X, position.Y);
    }
}
