/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion: Ver el summary de la clase
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;

namespace BaseLibrary
{
    public enum TipoCambio { Cambio, Pop, Empujar }

    /// <summary>
    /// Maneja todos los estado del juego actual y cambia, borra o agrega nuevos dependiendo
    /// de nuestras necesidades
    /// </summary>
    public class ManejadorEstadosJuegos : GameComponent
    {

        #region Elementos

        public event EventHandler EnCambioEstado;

        Stack<EstadoJuego> estadoJuegos = new Stack<EstadoJuego>();

        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;
        int dibujoOrden;

        public EstadoJuego EstadoActual
        {
            get { return estadoJuegos.Peek(); }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Maneja todo lo que tiene que ver con menus u otras opciones graficas del juego.
        /// </summary>
        /// <param name="game"></param>
        public ManejadorEstadosJuegos(Game game)
            : base(game)
        {
            dibujoOrden = startDrawOrder;
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

        

        #endregion

        #region Otros

        #region Funciones

        /// <summary>
        /// Quita el ultimo estado seleccionado.
        /// </summary>
        public void PopState()
        {
            if (estadoJuegos.Count > 0)
            {
                RemoveState();
                dibujoOrden -= drawOrderInc;

                if (EnCambioEstado != null)
                    EnCambioEstado(this, null);
            }
        }

        /// <summary>
        /// Para quitar el estado del juego a mero arriba del stack.
        /// </summary>
        private void RemoveState()
        {
            EstadoJuego stado = estadoJuegos.Peek();
            EnCambioEstado -= stado.CambioEstado;
            Game.Components.Remove(stado);
            estadoJuegos.Pop();
        }

        /// <summary>
        /// Cambia el estado del juego al indicado pero no borra los anteriores.
        /// </summary>
        /// <param name="nuevoEstado"></param>
        public void PushState(EstadoJuego nuevoEstado)
        {
            dibujoOrden += drawOrderInc;
            nuevoEstado.DrawOrder = dibujoOrden;

            AddState(nuevoEstado);

            if (EnCambioEstado != null)
                EnCambioEstado(this, null);

        }

        private void AddState(EstadoJuego nuevoEstado)
        {
            estadoJuegos.Push(nuevoEstado);
            Game.Components.Add(nuevoEstado);
            EnCambioEstado += nuevoEstado.CambioEstado;
        }

        /// <summary>
        /// Cambia el estado del juego al indicado  y borra los anteriores.
        /// </summary>
        /// <param name="nuevoEstado"> El tipo de estado al que quieras cambiar s</param>
        public void CambioEstado(EstadoJuego nuevoEstado)
        {
            while (estadoJuegos.Count > 0)
                RemoveState();

            nuevoEstado.DrawOrder = startDrawOrder;
            dibujoOrden = startDrawOrder;

            AddState(nuevoEstado);

            if (EnCambioEstado != null)
                EnCambioEstado(this, null);

        }

        #endregion

        #endregion
    }
}
