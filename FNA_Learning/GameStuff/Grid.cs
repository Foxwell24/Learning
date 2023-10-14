using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.GameStuff
{
    internal class Grid
    {
        GameObject?[][][] grid;
        public int tileSize {  get; private set; }
        public Rectangle Tile;

        public Grid(int gridSize)
        {
            tileSize = ((FNAGame.Height < FNAGame.Width) ? FNAGame.Height : FNAGame.Width) / gridSize; // grid size will fill the smallest aspect, width or height
            Tile = new Rectangle(0, 0, tileSize, tileSize);

            grid = new GameObject?[gridSize][][];
            for (int x = 0; x < grid.Length; x++)
            {
                grid[x] = new GameObject?[gridSize][];
                for (int y = 0; y < gridSize; y++)
                {
                    grid[x][y] = new GameObject?[Enum.GetNames<Layer>().Length];
                }
            }
        }

        public bool SetObject(int x, int y, Layer layer, GameObject? obj)
        {
            if (x < 0 ||
                y < 0 ||
                x >= grid.Length ||
                y >= grid.Length)
                return false;

            if (obj == null)
            {
                grid[x][y][(int)layer] = null;
                return true;
            }
            else
            {
                GameObject o = obj.Value;
                o.position = new Vector2(x * tileSize, y * tileSize);

                grid[x][y][(int)layer] = o;
                return true;
            }
        }

        public GameObject? GetObject(int x, int y, Layer layer)
        {
            return grid[x][y][(int)layer];
        }

        internal void Draw(double deltaTime, SpriteBatch batch)
        {
            ForAll(obj => obj?.Draw(batch));
        }

        private void ForAll(Action<GameObject?> action)
        {
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    for (int z = 0; z < grid[x][y].Length; z++)
                    {
                        action.Invoke(grid[x][y][z]);
                    }
                }
            }
        }

        public enum Layer
        {
            Ground,
            Deco,
            Items,
            Entities
        }
    }
}
