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
    internal class PlayerController : IUpdate
    {
        public GameObject player;
        Grid grid;

        int movementX, movementY;
        int currentX, currentY;

        public PlayerController(GameObject player, Grid grid)
        {
            this.player = player;
            this.grid = grid;

            Input.Instance.KeyDown += (_, e) =>
            {
                switch (e.Key)
                {
                    case Keys.W:
                        movementY--;
                        if (movementY < 0) movementY = -1;
                        break;

                    case Keys.A:
                        movementX--;
                        if (movementX < 0) movementX = -1;
                        break;

                    case Keys.S:
                        movementY++;
                        if (movementY > 0) movementY = 1;
                        break;

                    case Keys.D:
                        movementX++;
                        if (movementX > 0) movementX = 1;
                        break;
                }
            };
        }

        public void Update(double deltaTime)
        {
            if (movementX + movementY == 0) return;

            int newX = currentX + movementX;
            int newY = currentY + movementY;

            movementX = 0;
            movementY = 0;

            if (grid.SetObject(newX, newY, Grid.Layer.Entities, player))
            {
                grid.SetObject(currentX, currentY, Grid.Layer.Entities, null);

                currentX = newX;
                currentY = newY;
            }

        }
    }
}
