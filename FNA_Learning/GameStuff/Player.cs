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
        double time = 0;
        double elapsed = 0;

        protected override void Init(object? sender, EventArgs e)
        {
            base.Init(sender, e);

            Input.Instance.KeyDown += CalculateMovementInput;
            Input.Instance.KeyPressed += (sender, e) =>
            {
                elapsed += time;
                if (elapsed < 0.25) return;
                CalculateMovementInput(sender, e);
            };

            rectangle = new Rectangle(0, 0, 20, 20);
            scale = 0.5f;
        }

        protected override void LoadContent(object? sender, EventArgs e)
        {
            base.LoadContent(sender, e);

            texture = FNAGame.ContentManager_.Load<Texture2D>("Player.png");
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
            position = new Vector2((World.gridSize / 2) - (rectangle.Width * scale / 2));
            Console.WriteLine("load");
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
            time = e.deltaTime;

            Movement();
        }

        private void Movement()
        {
            if (velocity.Length() == 0) return;

            Vector2 movement = Vector2.Clamp(velocity, Vector2.One * -1, Vector2.One);

            base.Move(velocity);
            velocity = Vector2.Zero;
            Console.WriteLine(elapsed);
            elapsed = 0;
        }
    }
}
