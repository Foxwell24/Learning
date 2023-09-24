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
    internal class World
    {
        internal static readonly World Instance = new World();

        private const int numGrid = 20;
        Vector2 gridSize;

        Texture2D texture_grid;

        internal void LoadContent(ContentManager content)
        {
            texture_grid = content.Load<Texture2D>("WhiteSquare.png");
        }

        internal void Init()
        {
            int smallest = (FNAGame.Height < FNAGame.Width)? FNAGame.Height : FNAGame.Width;

            gridSize = new Vector2(smallest / numGrid, smallest / numGrid);
        }

        internal void Update(GameTime gameTime)
        {

        }

        internal void Draw(GameTime gameTime, SpriteBatch batch)
        {
            for (int y = 0; y < numGrid; y++)
            {
                for (int x = 0; x < numGrid; x++)
                {
                    batch.Draw(texture_grid, new Rectangle(x * (int)gridSize.X, y * (int)gridSize.Y, (int)gridSize.X - 1, (int)gridSize.Y - 1), Color.LightGray);
                }
            }
        }
    }
}
