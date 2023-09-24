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
        public static Input Instance { get; private set; }

        private KeyboardState _keyboardPast = new();
        private MouseState _mousePast = Mouse.GetState();

        #region Events
        public event EventHandler<KeyArgs> KeyPressed;
        public event EventHandler<MouseArgs> MousePressed;

        public event EventHandler<KeyArgs> KeyDown;
        public event EventHandler<MouseArgs> MouseDown;

        public event EventHandler<KeyArgs> KeyUp;
        public event EventHandler<MouseArgs> MouseUp;
        #endregion

        public Input() => Instance = this;

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            #region Keyboard
            // down
            foreach (var key in keyboardState.GetPressedKeys())
                if (_keyboardPast.IsKeyUp(key))
                    KeyDown?.Invoke(this, new KeyArgs(key));

            // pressed
            foreach (var key in _keyboardPast.GetPressedKeys())
                if (keyboardState.IsKeyDown(key))
                    KeyPressed?.Invoke(this, new KeyArgs(key));

            // up
            foreach (var key in _keyboardPast.GetPressedKeys())
                if (keyboardState.IsKeyUp(key))
                    KeyUp?.Invoke(this, new KeyArgs(key));
            #endregion

            #region Mouse
            // down
            if (mouseState.LeftButton == ButtonState.Pressed && _mousePast.LeftButton == ButtonState.Released)
                MouseDown?.Invoke(this, new MouseArgs(MouseButtonType.Left));
            if (mouseState.RightButton == ButtonState.Pressed && _mousePast.RightButton == ButtonState.Released)
                MouseDown?.Invoke(this, new MouseArgs(MouseButtonType.Right));
            if (mouseState.MiddleButton == ButtonState.Pressed && _mousePast.MiddleButton == ButtonState.Released)
                MouseDown?.Invoke(this, new MouseArgs(MouseButtonType.Middle));

            // pressed
            if (mouseState.LeftButton == ButtonState.Pressed && _mousePast.LeftButton == ButtonState.Pressed)
                MousePressed?.Invoke(this, new MouseArgs(MouseButtonType.Left));
            if (mouseState.RightButton == ButtonState.Pressed && _mousePast.RightButton == ButtonState.Pressed)
                MousePressed?.Invoke(this, new MouseArgs(MouseButtonType.Right));
            if (mouseState.MiddleButton == ButtonState.Pressed && _mousePast.MiddleButton == ButtonState.Pressed)
                MousePressed?.Invoke(this, new MouseArgs(MouseButtonType.Middle));

            // up
            if (mouseState.LeftButton == ButtonState.Released && _mousePast.LeftButton == ButtonState.Pressed)
                MouseUp?.Invoke(this, new MouseArgs(MouseButtonType.Left));
            if (mouseState.RightButton == ButtonState.Released && _mousePast.RightButton == ButtonState.Pressed)
                MouseUp?.Invoke(this, new MouseArgs(MouseButtonType.Right));
            if (mouseState.MiddleButton == ButtonState.Released && _mousePast.MiddleButton == ButtonState.Pressed)
                MouseUp?.Invoke(this, new MouseArgs(MouseButtonType.Middle));
            #endregion

            _keyboardPast = keyboardState;
            _mousePast = mouseState;
        }

        #region Args
        public class KeyArgs : EventArgs
        {
            public Keys Key { get; set; }

            public KeyArgs(Keys key)
            {
                Key = key;
            }
        }
        public class MouseArgs : EventArgs
        {
            public MouseButtonType Button { get; set; }

            public MouseArgs(MouseButtonType button)
            {
                Button = button;
            }
        }
        #endregion

        public enum MouseButtonType
        {
            Right,
            Left,
            Middle
        }
    }
}
