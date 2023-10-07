using FNA_Learning.GameStuff.Physics;
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
        private Grid grid;

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

        /// <summary>
        /// Ground
        /// </summary>
        public event EventHandler<DrawEventArgs> DrawEvent_0;
        /// <summary>
        /// Ground Cover
        /// </summary>
        public event EventHandler<DrawEventArgs> DrawEvent_1;
        /// <summary>
        /// Decorations on Ground, e.g., Pots, Fallen Log
        /// </summary>
        public event EventHandler<DrawEventArgs> DrawEvent_2;
        /// <summary>
        /// Player
        /// </summary>
        public event EventHandler<DrawEventArgs> DrawEvent_3;
        /// <summary>
        /// treetops and such
        /// </summary>
        public event EventHandler<DrawEventArgs> DrawEvent_4;
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

        private const int numGrid = 16;
        public static int gridSize { get; private set; }

        private List<ColliderCircle> colliders_circle = new();
        private List<ColliderRect> colliders_rectangle = new();

        Texture2D texture_grid;

        internal void LoadContent()
        {
            texture_grid = FNAGame.ContentManager_.Load<Texture2D>("WhiteSquare.png");

            LoadContentEvent?.Invoke(this, null);
        }

        internal void Init()
        {
            gridSize = ((FNAGame.Height < FNAGame.Width)? FNAGame.Height : FNAGame.Width) / numGrid;
            grid = new Grid(numGrid);

            for (int y = 0; y < numGrid; y++)
            {
                for (int x = 0; x < numGrid; x++)
                {
                    WorldTile tile = new WorldTile(new Vector2(x * gridSize, y * gridSize));

                    if (x == 4 && y== 4) tile.SetColor(Color.Orange);
                }
            }
            colliders_rectangle.Add(new(4 * gridSize, 4 * gridSize, gridSize, gridSize));

            InitEvent?.Invoke(this, null);
        }

        internal void Update(double deltaTime)
        {
            UpdateEvent?.Invoke(this, new UpdateEventArgs(deltaTime));
        }

        internal void Draw(double deltaTime, SpriteBatch batch)
        {
            DrawEvent_0?.Invoke(this, new DrawEventArgs(deltaTime, batch));
            DrawEvent_1?.Invoke(this, new DrawEventArgs(deltaTime, batch));
            DrawEvent_2?.Invoke(this, new DrawEventArgs(deltaTime, batch));
            DrawEvent_3?.Invoke(this, new DrawEventArgs(deltaTime, batch));
            DrawEvent_4?.Invoke(this, new DrawEventArgs(deltaTime, batch));
        }

        public bool CheckCollision(ColliderRect other, out ColliderRect collision)
        {
            foreach (var collider in colliders_rectangle) if (collider.CollidesRect(other))
                {
                    collision = collider;
                    return true;
                }

            collision = default;
            return false;
        }
    }
}
