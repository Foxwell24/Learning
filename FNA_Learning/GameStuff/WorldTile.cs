using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.GameStuff
{
    internal class WorldTile : GameObject
    {
        public WorldTile(Vector2 position)
        {
            this.position = position;
        }

        protected override void Init(object? sender, EventArgs e)
        {
            base.Init(sender, e);
            World.Instance.DrawEvent_0 += Draw;
            rectangle = new Rectangle((int)position.X, (int)position.Y, World.gridSize - 1, World.gridSize - 1);
        }

        protected override void LoadContent(object? sender, EventArgs e)
        {
            base.LoadContent(sender, e);
            texture = FNAGame.ContentManager_.Load<Texture2D>("WhiteSquare.png");
        }
    }
}
