using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Sprite_Classes
{
    /// <summary>
    /// Base de sprite simple con solo una función para dibujarlo.
    /// </summary>
    public class SpriteBase
    {
        #region Elements

        public Texture2D Imagen { get; set; }
        public Vector2 Posicion { get; set; }
        public Rectangle Bordes { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor base
        /// </summary>
        /// <param name="imagen"></param>
        /// <param name="posicion"></param>
        /// <param name="borders"></param>
        public SpriteBase(Texture2D imagen, Vector2 posicion, Rectangle? borders)
        {
            Imagen = imagen;
            Posicion = posicion;
            Bordes = borders ?? imagen.Bounds; 
        }

        #endregion

        #region Game related
        
        /// <summary>
        /// Dibuja el sprite con la imagen, posicion y en caso de existir, los borders de la imagen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Imagen, Posicion, Bordes, Color.White);
        }

        #endregion

        #region Other

        #endregion

    }
}
