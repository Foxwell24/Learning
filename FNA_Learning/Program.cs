using FNA_Learning.GameStuff;
using FNA_Learning.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FNA_Learning
{
    internal class FNAGame : Game
    {
        public static readonly int Width = 1920;
        public static readonly int Height = 1080;

        public static ContentManager ContentManager_ {  get; private set; }

        private SpriteBatch _batch;

        [STAThread]
        static void Main(string[] args)
        {
            // following this https://github.com/FNA-XNA/FNA/wiki/2b:-FNA-From-Scratch-Tutorial#your-first-game

            new Input();
            new Player();

            using (FNAGame game = new FNAGame())
            {
                game.Run();
            };

            Console.WriteLine("\n-------------------");
            Console.WriteLine("Game closed, press any key to exit...");
            Console.ReadKey();
        }

        private FNAGame()
        {
            GraphicsDeviceManager gManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gManager.PreferredBackBufferWidth = Width;
            gManager.PreferredBackBufferHeight = Height;
            gManager.IsFullScreen = false;

            gManager.ApplyChanges();

            Window.IsBorderlessEXT = true;
        }

        protected override void Initialize()
        {
            /* This is a nice place to start up the engine, after
             * loading configuration stuff in the constructor
             */

            Input.Instance.KeyDown += (_, a) =>
            {
                if (a.Key == Keys.Escape) Exit();
            };

            World.Instance.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load textures, sounds, and so on in here...

            _batch = new SpriteBatch(GraphicsDevice);

            ContentManager_ = Content;
            World.Instance.LoadContent();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // Clean up after yourself!
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // Run game logic in here. Do NOT render anything here!

            Input.Instance.Update();
            World.Instance.Update(gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Render stuff in here. Do NOT run game logic in here!
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _batch.Begin();
            World.Instance.Draw(gameTime.ElapsedGameTime.TotalSeconds, _batch);
            _batch.End();

            base.Draw(gameTime);
        }
    }
}