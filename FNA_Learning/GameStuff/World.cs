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

        #region Events

        public event EventHandler InitEvent;
        public event EventHandler LoadContentEvent;

        public event EventHandler<UpdateEventArgs> UpdateEvent;
        public class UpdateEventArgs : EventArgs
        {
            public double deltaTime { get; set; }

            public UpdateEventArgs(double deltaTime)
            {
                this.deltaTime = deltaTime;
            }
        }

        public event EventHandler<DrawEventArgs> DrawEvent;
        public class DrawEventArgs : EventArgs
        {
            public double deltaTime { get; set; }
            public SpriteBatch spriteBatch { get; set; }

            public DrawEventArgs(double deltaTime, SpriteBatch spriteBatch)
            {
                this.deltaTime = deltaTime;
                this.spriteBatch = spriteBatch;
            }
        }

        #endregion

        List<GameObject> gameObjects = new();

        private const int numGrid = 10;
        public static int gridSize { get; private set; }

        Texture2D texture_grid;

        internal void LoadContent()
        {
            texture_grid = FNAGame.ContentManager_.Load<Texture2D>("WhiteSquare.png");
            LoadContentEvent?.Invoke(this, null);
        }

        internal void Init()
        {
            gridSize = ((FNAGame.Height < FNAGame.Width)? FNAGame.Height : FNAGame.Width) / numGrid;
            InitEvent?.Invoke(this, null);
        }

        internal void Update(double deltaTime)
        {
            UpdateEvent?.Invoke(this, new UpdateEventArgs(deltaTime));
        }

        internal void Draw(double deltaTime, SpriteBatch batch)
        {
            for (int y = 0; y < numGrid; y++)
            {
                for (int x = 0; x < numGrid; x++)
                {
                    batch.Draw(texture_grid, new Rectangle(x * gridSize, y * gridSize, gridSize - 1, gridSize - 1), Color.LightGray);
                }
            }

            DrawEvent?.Invoke(this, new DrawEventArgs(deltaTime, batch));
        }

        public void AddGameObject(GameObject gameObject)
        {
            lock (gameObjects)
            {
                gameObjects.Add(gameObject);
            }
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            lock(gameObjects)
            {
                gameObjects.Remove(gameObject);
            }
        }
    }
}
