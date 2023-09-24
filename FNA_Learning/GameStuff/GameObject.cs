using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.GameStuff
{
    internal class GameObject
    {
        protected Texture2D texture;
        protected Color color = Color.Wheat;
        protected Vector2 position = new();
        protected Rectangle rectangle = new();
        protected float scale = 1f;

        public GameObject()
        {
            World.Instance.DrawEvent += Draw;
            World.Instance.UpdateEvent += Update;
            World.Instance.InitEvent += Init;
            World.Instance.LoadContentEvent += LoadContent;
        }

        protected virtual void LoadContent(object? sender, EventArgs e)
        {

        }

        protected virtual void Init(object? sender, EventArgs e)
        {

        }

        protected virtual void Update(object? sender, World.UpdateEventArgs e)
        {

        }

        protected virtual void Draw(object? sender, World.DrawEventArgs e)
        {
            e.spriteBatch.Draw(texture, position, rectangle, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
        }

        public virtual void Move(Vector2 movement)
        {
            position += movement * World.gridSize;
        }
    }
}
