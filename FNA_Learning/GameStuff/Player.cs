using FNA_Learning.GameStuff.Physics;
using FNA_Learning.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace FNA_Learning.GameStuff
{
    internal class Player : GameObject
    {
        public static Player Instance { get; private set; }

        private ColliderRect collider;

        Vector2 velocity;
        float speed = 300f;

        protected override void Init(object? sender, EventArgs e)
        {
            base.Init(sender, e);

            World.Instance.DrawEvent_3 += Draw;

            Input.Instance.KeyPressed += CalculateMovementInput;

            rectangle = new Rectangle(0, 0, 20, 20);
        }

        protected override void LoadContent(object? sender, EventArgs e)
        {
            base.LoadContent(sender, e);

            texture = FNAGame.ContentManager_.Load<Texture2D>("Player.png");
            color = Color.Black;
            scale = 1;
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
            position = new Vector2((World.gridSize / 2) - (rectangle.Width * scale / 2));

            collider = new(position.X, position.Y, rectangle.Width, rectangle.Height);
        }

        private void CalculateMovementInput(object? sender, Input.KeyArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                    velocity.Y -= 1;
                    break;

                case Keys.A:
                    velocity.X -= 1;
                    break;

                case Keys.S:
                    velocity.Y += 1;
                    break;

                case Keys.D:
                    velocity.X += 1;
                    break;
            }
        }

        protected override void Update(object? sender, World.UpdateEventArgs e)
        {
            base.Update(sender, e);

            Movement((float)e.deltaTime);
        }

        private void Movement(float deltaTime)
        {
            if (velocity.Length() == 0) return;

            velocity = Vector2.Normalize(velocity);

            Vector2 movement = velocity * speed * deltaTime;


            base.Move(movement);
            velocity = Vector2.Zero;

            collider.SetPos(position);

        }
    }
}
