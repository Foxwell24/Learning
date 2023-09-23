using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Linq.Expressions;

namespace Learning_SFML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            RenderWindow window = new RenderWindow(
                VideoMode.DesktopMode with
                {
                    Width = 800,
                    Height = 600
                },
                "Learning",
                Styles.Default,
                new ContextSettings()
                {
                    MajorVersion = 0,
                    MinorVersion = 1
                });

            //window.SetMouseCursorVisible(false);

            //Texture texture = new Texture($"{Directory.GetCurrentDirectory()}/Resorces/image0.png");
            CircleShape shape = new CircleShape(50f)
            {
                FillColor = Color.Blue,
                Texture = new Texture($"{Directory.GetCurrentDirectory()}/Resorces/image0.png")
            };

            RectangleShape shape1 = new RectangleShape(new Vector2f(50, 30))
            {
                FillColor = Color.Blue
            };

            Sprite sprite = new Sprite(new Texture(10,10));
            sprite.Color = Color.White;

            Font font = new Font($"{Directory.GetCurrentDirectory()}/Resorces/Fonts/arial.ttf");
            Text text = new Text("Hellooooo", font, 80);

            window.KeyReleased += (_, args) =>
            {
                if (args.Code == Keyboard.Key.Escape) window.Close();
            };

            float charSize = 50;
            while (window.IsOpen)
            {
                window.DispatchEvents();

                charSize = Update(charSize);
                text.CharacterSize = (uint)charSize;

                sprite.Position = (Vector2f)Mouse.GetPosition(window);
                shape.Position = (Vector2f)(Mouse.GetPosition(window) - new Vector2i((int)shape.Radius, (int)shape.Radius));
                //shape1.Position = (Vector2f)Mouse.GetPosition(window) - (shape1.Size / 2);

                window.Clear();

                window.Draw(sprite);
                window.Draw(text);
                window.Draw(shape);
                //window.Draw(shape1);
                window.Display();
            }
        }

        private static float Update(float charSize)
        {
            float increment = 0.01f;
            charSize += increment;
            if (charSize > 100) charSize = 10;
            return charSize;
        }
    }
}