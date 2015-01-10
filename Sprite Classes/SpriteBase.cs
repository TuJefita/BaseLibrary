/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion:
*/

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
        public Color Color { get; set; }
        public float Rotacion   { get; set; }
        public float Depth { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor base del sprite
        /// </summary>
        /// <param name="imagen"></param>
        /// <param name="posicion"></param>
        /// <param name="borders"></param>
        public SpriteBase(Texture2D imagen, Vector2 posicion, Rectangle? borders)
        {
            Imagen = imagen;
            Posicion = posicion;
            Bordes = borders ?? imagen.Bounds;
            Color = Color.White;
            Depth = 1.0f;
        }

        public SpriteBase(Texture2D imagen, Vector2 posicion, Rectangle? borders, float rotacion)
        {
            Imagen = imagen;
            Posicion = posicion;
            Bordes = borders ?? imagen.Bounds;
            Rotacion = rotacion;
            Color = Color.White;
            Depth = 1.0f;
        }

        #endregion

        #region Game related
        
        /// <summary>
        /// Dibuja el sprite con la imagen, posicion Puntos_Y en caso de existir, los borders de la imagen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Imagen, Posicion, Bordes, Color, Rotacion, Vector2.Zero, 1.0f, SpriteEffects.None, Depth);
        }

        #endregion

        #region Other

        #endregion

    }
}
