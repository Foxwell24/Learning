using FNA_Learning.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.GameStuff
{
    internal class Player : GameObject
    {
        public static Player Instance { get; private set; }

        Vector2 velocity;
        float speed = 5f;

        protected override void Init(object? sender, EventArgs e)
        {
            base.Init(sender, e);

            Input.Instance.KeyPressed += CalculateMovementInput;

            rectangle = new Rectangle(0, 0, 20, 20);
        }

        protected override void LoadContent(object? sender, EventArgs e)
        {
            base.LoadContent(sender, e);

            texture = FNAGame.ContentManager_.Load<Texture2D>("Player.png");
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
            Console.WriteLine("load");
        }

        private void CalculateMovementInput(object? sender, Input.KeyArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                    velocity.Y -= speed;
                    break;

                case Keys.A:
                    velocity.X -= speed;
                    break;

                case Keys.S:
                    velocity.Y += speed;
                    break;

                case Keys.D:
                    velocity.X += speed;
                    break;
            }
        }

        protected override void Update(object? sender, World.UpdateEventArgs e)
        {
            base.Update(sender, e);

            Movement(e.gameTime);
        }

        private void Movement(GameTime gameTime)
        {
            Move(velocity);

            velocity = Vector2.Zero;
        }
    }
}
