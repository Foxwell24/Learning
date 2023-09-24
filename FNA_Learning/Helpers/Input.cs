using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNA_Learning.Helpers
{
    internal class Input
    {
        private KeyboardState _keyboardPast = new();
        private MouseState _mousePast = Mouse.GetState();

        public event EventHandler<KeyPressedArgs> KeyPressed;
        public event EventHandler<MousePressedArgs> MousePressed;

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            foreach (var key in _keyboardPast.GetPressedKeys())
                if (keyboardState.IsKeyUp(key))
                    KeyPressed?.Invoke(this, new KeyPressedArgs(key));

            #region MouseClick
            if (mouseState.LeftButton == ButtonState.Released && _mousePast.LeftButton == ButtonState.Pressed)
                MousePressed?.Invoke(this, new MousePressedArgs(MouseButtonType.Left));
            if (mouseState.RightButton == ButtonState.Released && _mousePast.RightButton == ButtonState.Pressed)
                MousePressed?.Invoke(this, new MousePressedArgs(MouseButtonType.Right));
            if (mouseState.MiddleButton == ButtonState.Released && _mousePast.MiddleButton == ButtonState.Pressed)
                MousePressed?.Invoke(this, new MousePressedArgs(MouseButtonType.Middle));
            #endregion

            _keyboardPast = keyboardState;
            _mousePast = mouseState;
        }

        public class KeyPressedArgs : EventArgs
        {
            public Keys Key { get; set; }

            public KeyPressedArgs(Keys key)
            {
                Key = key;
            }
        }
        public class MousePressedArgs : EventArgs
        {
            public MouseButtonType Button { get; set; }

            public MousePressedArgs(MouseButtonType button)
            {
                Button = button;
            }
        }

        public enum MouseButtonType
        {
            Right,
            Left,
            Middle
        }
    }
}
