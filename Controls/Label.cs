using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Controls
{
    public class Label : Control
    {
        #region Elementos

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
