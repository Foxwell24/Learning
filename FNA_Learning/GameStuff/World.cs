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
            public GameTime gameTime { get; set; }

            public UpdateEventArgs(GameTime gameTime)
            {
                this.gameTime = gameTime;
            }
        }

        public event EventHandler<DrawEventArgs> DrawEvent;
        public class DrawEventArgs : EventArgs
        {
            public GameTime GameTime { get; set; }
            public SpriteBatch spriteBatch { get; set; }

            public DrawEventArgs(GameTime gameTime, SpriteBatch spriteBatch)
            {
                GameTime = gameTime;
                this.spriteBatch = spriteBatch;
            }
        }

        #endregion

        List<GameObject> gameObjects = new();

        private const int numGrid = 10;
        int gridSize;

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

        internal void Update(GameTime gameTime)
        {
            UpdateEvent?.Invoke(this, new UpdateEventArgs(gameTime));
        }

        internal void Draw(GameTime gameTime, SpriteBatch batch)
        {
            for (int y = 0; y < numGrid; y++)
            {
                for (int x = 0; x < numGrid; x++)
                {
                    batch.Draw(texture_grid, new Rectangle(x * gridSize, y * gridSize, gridSize - 1, gridSize - 1), Color.LightGray);
                }
            }

            DrawEvent?.Invoke(this, new DrawEventArgs(gameTime, batch));
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
