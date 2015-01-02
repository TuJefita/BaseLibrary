// Trabajador: Hector Hawley Herrera
// Fecha:
// Numero de revision:

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
          
namespace BaseLibrary.Controls
{
    public abstract class Control
    {

        #region Elemento

        public string Texto         { get; set; }

        /// <summary>
        /// Para denotar si es dibujado o no.
        /// </summary>
        public bool Visible         { get; set; }

        /// <summary>
        /// Para denotar si va a ser incluido en la logica del app o no.
        /// </summary>
        public bool Activado     { get; set; }

        /// <summary>
        /// Para ver si fue seleccionado y/o aplastado. Sea el caso.
        /// </summary>
        public bool Seleccionar     { get; set; }

        /// <summary>
        /// Para hacer cosas relacionados con tiempos
        /// </summary>
        public float Tiempo { get; set; }

        public Vector2 Posicion     { get; set; }

        public Color Color          { get; set; }

        public SpriteFont Fuente { get; set; }

        /// <summary>
        /// El evento al realizar si fue seleccionado.
        /// </summary>
        public event EventHandler Seleccionado;

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
        /// Funcion para poder simplificar el evento incluido en este control.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void EnSeleccionado(EventArgs e)
        {
            if (Seleccionado != null)
                Seleccionado(this, e);
        }

        #endregion

    }
}
