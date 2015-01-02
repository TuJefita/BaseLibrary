using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Pantallas
{
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
