using FNA_Learning.Helpers;
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
        private const int GridSize = 10;
        internal static readonly World Instance = new World();
        private Grid grid;
        private PlayerController playerController;

        GameObject Grass, Water, Player;

        internal void LoadContent()
        {

            TextureHolder.LoadTextures();

            Grass = new GameObject()
            {
                texture = TextureSelector.WhiteSquare,
                color = Color.LimeGreen,
                rectangle = grid.Tile
            };

            Water = new GameObject()
            {
                texture = TextureSelector.WhiteSquare,
                color = Color.DarkBlue,
                rectangle = grid.Tile
            };


            Player = new GameObject()
            {
                texture = TextureSelector.Player,
                color = Color.Red,
                rectangle = TextureHolder.GetTexture(TextureSelector.Player).Bounds,
            };
            playerController = new PlayerController(Player, grid);
            grid.SetObject(0, 0, Grid.Layer.Entities, playerController.player);


            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    if ((x+y) % 2 == 0) grid.SetObject(x, y, Grid.Layer.Ground, Grass);
                    else grid.SetObject(x, y, Grid.Layer.Ground, Water);
                }
            }
        }

        internal void Init()
        {
            grid = new Grid(GridSize);

        }

        internal void Update(double deltaTime)
        {
            playerController.Update(deltaTime);
        }

        internal void Draw(double deltaTime, SpriteBatch batch)
        {
            grid.Draw(deltaTime, batch);
        }
    }
}
