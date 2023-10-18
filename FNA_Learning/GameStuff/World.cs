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

        GameObject Grass, Water, Wall, Player;

        internal void LoadContent()
        {

            TextureHolder.LoadTextures();

            Grass = new GameObject(TextureSelector.WhiteSquare, Color.LimeGreen);
            Water = new GameObject(TextureSelector.WhiteSquare, Color.DarkBlue);
            Wall = new GameObject(TextureSelector.WhiteSquare, Color.Brown) { obsticle = true };
            Player = new GameObject(TextureSelector.Player, Color.Red);

            playerController = new PlayerController(Player, grid);
            grid.SetObject(0, 0, Grid.Layer.Entities, playerController.player);


            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    GameObject chosen;

                    if ((x + y) % 2 == 0) chosen = Grass;
                    else chosen = Water;

                    if (x == GridSize-1 || y == GridSize-1) chosen = Wall;

                    grid.SetObject(x, y, Grid.Layer.Ground, chosen);
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
