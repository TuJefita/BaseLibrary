using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Input
{
    public static class MouseInput
    {
        #region Elementos

        static public Texture2D MouseTexture { private get; set; }
        static MouseState LastMouseState;
        static MouseState CurrentMouseState = Mouse.GetState();

        #endregion

        #region Constructores

        #endregion

        #region Funciones de Juego

        public static void Update(GameTime gameTime)
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MouseTexture, MousePosition(), Color.White);
        }

        #endregion

        #region Otros

        public static bool ClickOcurred()
        {
            return (LastMouseState.LeftButton == ButtonState.Released && CurrentMouseState.LeftButton == ButtonState.Pressed);
        }

        public static Vector2 MousePosition()
        {
#if WINDOWS
            return new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
#elif LINUX
			return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
#endif
        }

        public static Rectangle MouseRectangle()
        {
#if WINDOWS
            return new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1);
#elif LINUX
			return new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
#endif
		
        }

        public static bool Intersects(Rectangle Posicion)
        {
            return MouseInput.MouseRectangle().Intersects(Posicion);
        }

        public static bool Intersects(Vector2 Posicion, int width, int height)
        {
            return MouseInput.MouseRectangle().Intersects(new Rectangle((int)Posicion.X, (int)Posicion.Y, width, height));
        }

        #endregion

    }
}
