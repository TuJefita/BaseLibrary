/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion: Mira el summary de la clase
*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Pantallas
{
    /// <summary>
    /// Pantalla base del juego. Sirve para encapsular toda logica de juego Puntos_Y dibujo de juego.
    /// </summary>
    public class EstadoBase : EstadoJuego
    {

        #region Elementos

        protected Game RefJuego;
        protected EstadoBase estadoCambiar;

        protected bool Transicionar;
        protected TipoCambio tipoCambio;
        protected TimeSpan timerTransicion;
        protected TimeSpan intervaloTransicion = TimeSpan.FromSeconds(0.5);
        

        #endregion

        #region Constructores

        public EstadoBase(Game game, ManejadorEstadosJuegos manager)
            : base(game, manager)
        {
            RefJuego = game;
        }

        #endregion

        #region Funciones de Juego

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Transicionar)
            {
                timerTransicion += gameTime.ElapsedGameTime;

                if (timerTransicion >= intervaloTransicion)
                {
                    Transicionar = false;

                    switch (tipoCambio)
                    {
                        case TipoCambio.Cambio:
                            ManejadorEstado.CambioEstado(estadoCambiar);
                            break;

                        case TipoCambio.Pop:
                            ManejadorEstado.PopState();
                            break;

                        case TipoCambio.Empujar:
                            ManejadorEstado.PushState(estadoCambiar);
                            break;
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        #endregion

        #region Otros

        /// <summary>
        /// Pide una transicion del juego a otra
        /// </summary>
        /// <param name="cambio"></param>
        /// <param name="game"></param>
        public virtual void Transicion(TipoCambio cambio, EstadoBase game)
        {
            Transicionar = true;
            tipoCambio = cambio;
            estadoCambiar = game;
            timerTransicion = TimeSpan.Zero;
        }

        #endregion
    }
}
