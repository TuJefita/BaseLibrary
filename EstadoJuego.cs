/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion:
*/

using System;

using Microsoft.Xna.Framework;

namespace BaseLibrary
{
    /// <summary>
    /// Un estado de juego base. Tu imaginacion es el limite.
    /// </summary>
    public abstract partial class EstadoJuego : DrawableGameComponent
    {

        #region Elementos

        protected ManejadorEstadosJuegos ManejadorEstado;

        /// <summary>
        /// Regresa este estado de juego.
        /// </summary>
        public EstadoJuego Tag { get; private set; }

        #endregion

        #region Constructores


        public EstadoJuego(Game game, ManejadorEstadosJuegos manager)
            : base(game)
        {
            ManejadorEstado = manager;
            Tag = this;
        }

        #endregion

        #region Funciones de Juego

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {   
            base.Draw(gameTime);
        }

         

        #endregion

        #region Otros

        internal protected virtual void CambioEstado(object sender, EventArgs e)
        {
            if (ManejadorEstado.EstadoActual == Tag)
                Show();
            else
                Hide();

        }

        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;

        }

        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }

        #endregion
    }
}
