using FNA_Learning.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FNA_Learning
{
    internal class FNAGame : Game
    {
        [STAThread]
        static void Main(string[] args)
        {
            // following this https://github.com/FNA-XNA/FNA/wiki/2b:-FNA-From-Scratch-Tutorial#your-first-game

            using (FNAGame game = new FNAGame())
            {
                game.Run();
            };

            Console.ReadLine();
        }

        Input input;

        private FNAGame()
        {
            GraphicsDeviceManager gManager = new GraphicsDeviceManager(this);

            gManager.PreferredBackBufferWidth = 1000;
            gManager.PreferredBackBufferHeight = 800;
            gManager.IsFullScreen = false;

            gManager.ApplyChanges();

            //Window.IsBorderlessEXT = true;
        }

        protected override void Initialize()
        {
            /* This is a nice place to start up the engine, after
             * loading configuration stuff in the constructor
             */
            base.Initialize();

            input = new Input();

            input.KeyPressed += (s, e) =>
            {
                Console.WriteLine($"{e.Key} Pressed");
                if (e.Key == Keys.Escape)
                    this.Exit();
            };
            input.MousePressed += (s, e) =>
            {
                Console.WriteLine($"{e.Button} Pressed");
            };
        }

        protected override void LoadContent()
        {
            // Load textures, sounds, and so on in here...
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

            input.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Render stuff in here. Do NOT run game logic in here!
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}