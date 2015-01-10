/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 09 de Enero del 2015
* Descripcion: Label.
*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Controls
{
    /// <summary>
    /// Dibuja un string a la pantalla
    /// </summary>
    public class Label : Control
    {
        #region Elementos

        public override Vector2 Posicion { get; set; }
        public override string Texto { get; set; }
        public override float Tiempo { get; set; }

        private float tamañoFuente;

        public float TamañoFuente { get { return tamañoFuente * 0.125f; } set { tamañoFuente = value; } }

        public Vector2 Tamaño { get { return TamañoFuente * Fuente.MeasureString(Texto); } }

        #endregion

        #region Constructores

        public Label() 
            : base()
        {
            TamañoFuente = 1; 
        }

        #endregion

        #region Funciones de Juego

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!string.IsNullOrEmpty(Texto))
                spriteBatch.DrawString(
                    Fuente,             //Fuete a usar
                    Texto,              //string
                    Posicion,
                    Color,
                    0.0f,               //Rotacion
                    Vector2.Zero,       //Origen de la imagen
                    TamañoFuente,       //Zoom
                    SpriteEffects.None, //Efecto
                    1.0f);              //Depth
        }

        #endregion

        #region Otros

        #endregion
    }
}
