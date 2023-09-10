
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpHook;

namespace MauiApp1.CustomCode
{
    public class DrawingCanvas : IDrawable
    {
        Player player;

        public static TaskPoolGlobalHook hook;

        public DrawingCanvas()
        {
            hook = new TaskPoolGlobalHook();

            player = new Player();

            new Thread(() =>
            {
                hook.Run();
            }).Start();
        }

        public void Draw(ICanvas canvas, RectF screen)
        {
            player.Draw(canvas, screen);
        }
    }
}
