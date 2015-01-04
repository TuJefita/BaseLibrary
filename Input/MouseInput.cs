/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion: Clase estatica que representa el mouse. Solo sirve en Windows o Linux
*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Input
{

#if WINDOWS || LINUX

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

        /// <summary>
        /// Revisa si paso un click izquierdo
        /// </summary>
        /// <returns></returns>
        public static bool ClickOcurred()
        {
            return (LastMouseState.LeftButton == ButtonState.Released && CurrentMouseState.LeftButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Regresa la posicion actual del mouse.
        /// </summary>
        /// <returns></returns>
        public static Vector2 MousePosition()
        {
#if WINDOWS
            return new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
#elif LINUX
			return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
#endif
        }

        /// <summary>
        /// Regresa el rectangle que representa al mouse no el rectangulo de dibujo.
        /// </summary>
        /// <returns></returns>
        public static Rectangle MouseRectangle()
        {
#if WINDOWS
            return new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1);
#elif LINUX
			return new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
#endif
		
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Posicion"> Rectangulo que representa las dimensiones Puntos_Y posicion del objeto </param>
        /// <returns></returns>
        public static bool Intersects(Rectangle Posicion)
        {
            return MouseInput.MouseRectangle().Intersects(Posicion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Posicion"> Del objeto </param>
        /// <param name="width"> Longitud </param>
        /// <param name="height"> Altura /param>
        /// <returns></returns>
        public static bool Intersects(Vector2 Posicion, int width, int height)
        {
            return MouseInput.MouseRectangle().Intersects(new Rectangle((int)Posicion.X, (int)Posicion.Y, width, height));
        }

        #endregion

    }

#endif
}
