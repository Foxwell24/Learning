using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.GameStuff
{
    internal class Grid
    {
        GridTile[][] grid;

        public Grid(int gridSize)
        {
            grid = new GridTile[gridSize][];
            for (int i = 0; i < gridSize; i++) grid[i] = new GridTile[i];
        }

        public GridTile GetTile(int x, int y) => grid[x][y];
    }

    internal class GridTile
    {

    }
}
