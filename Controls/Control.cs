/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion: Control base. Madre de todos los controles
*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
          
namespace BaseLibrary.Controls
{
    public abstract class Control
    {
        #region Elemento

        /// <summary>
        /// 
        /// </summary>
        public string Texto             { get; set; }

        /// <summary>
        /// Para denotar si es dibujado o no.
        /// </summary>
        public bool Visible             { get; set; }

        /// <summary>
        /// Para denotar si va a ser incluido en la logica del app o no.
        /// </summary>
        public bool Activado            { get; set; }

        /// <summary>
        /// Para ver si fue seleccionado Puntos_Y/o aplastado. Sea el caso.
        /// </summary>
        public bool Seleccionar         { get; set; }

        /// <summary>
        /// Para hacer cosas relacionados con tiempos
        /// </summary>
        public float Tiempo             { get; set; }

        public Vector2 Posicion         { get; set; }

        public Color Color              { get; set; }

        public SpriteFont Fuente        { get; set; }

        #region Eventos

        public event EventHandler Click = delegate { };
        public event EventHandler Cambio = delegate { };

        #endregion

        #endregion

        #region Constructores

        public Control()
        {
            Visible = true;
            Seleccionar = false;
            Activado = true;
            Texto = "";
            Tiempo = 200.0f;

            Posicion = Vector2.Zero;
            Color = Color.White;
        }

        #endregion

        #region Funciones de Juego

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion

        #region Otros

        /// <summary>
        /// Corre el evento cuando fue seleccionado.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void EnClick(EventArgs e)
        {
            Click(this, e);
        }

        /// <summary>
        /// Corre el evento cuando fue modificado.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void EnCambio(EventArgs e)
        {
            Cambio(this, e);
        }

        #endregion

    }
}
