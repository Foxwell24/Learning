using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.GameStuff.Physics
{
    internal struct ColliderRect
    {
        public RectangleF Collision { get; private set; }

        public ColliderRect(float x, float y, float width, float height) => Collision = new RectangleF(x, y, width, height);
        public ColliderRect(RectangleF collision) => Collision = collision;

        public bool CollidesRect(ColliderRect other) => Collision.IntersectsWith(other.Collision);

        public void SetPos(Vector2 newPos) => Collision = Collision with { X = newPos.X, Y = newPos.Y };
        public void SetPos(Microsoft.Xna.Framework.Vector2 newPos) => Collision = Collision with { X = newPos.X, Y = newPos.Y };

        public ColliderRect FutureCollider(Vector2 newPos) => this with { Collision = Collision with { X = newPos.X, Y = newPos.Y } };
        public ColliderRect FutureCollider(Microsoft.Xna.Framework.Vector2 newPos) => this with { Collision = Collision with { X = newPos.X, Y = newPos.Y } };

        public Microsoft.Xna.Framework.Vector2 BestMovementToCollider(ColliderRect collision, Microsoft.Xna.Framework.Vector2 desiredChangeInPosition)
        {

        }
    }
}
