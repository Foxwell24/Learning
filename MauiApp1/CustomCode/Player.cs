using KaelsToolBox_2.Math;
using SharpHook;
using SharpHook.Native;

namespace MauiApp1.CustomCode
{
    public class Player : IDrawable
    {
        Vector2f position = new Vector2f(100, 100);
        Vector2f movement = Vector2f.Zero;
        float speed = 1f;
        float size = 30;
        //Color color = Color.FromRgba(118, 103, 33, 1);
        Color color = Colors.Red;

        public Player()
        {
            DrawingCanvas.hook.KeyPressed += KeyPressed;
        }

        private void KeyPressed(object sender, KeyboardHookEventArgs e)
        {
            switch (e.Data.KeyCode)
            {
                case KeyCode.VcA: movement.x -= speed; break;
                case KeyCode.VcD: movement.x += speed; break;
                case KeyCode.VcW: movement.y -= speed; break;
                case KeyCode.VcS: movement.y += speed; break;
            }
        }

        int count = 0;

        public void Draw(ICanvas canvas, RectF screen)
        {
            Move();
            canvas.StrokeColor = color;
            canvas.StrokeSize = 1f;
            canvas.DrawCircle(position.x, position.y, size);

            count++;
        }

        private void Move()
        {
            position += movement;
            movement = Vector2f.Zero;
        }
    }
}
