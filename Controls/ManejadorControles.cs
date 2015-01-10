/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion: Vea summary en clase
*/

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BaseLibrary.Fuente;

namespace BaseLibrary.Controls
{
    /// <summary>
    /// Se utiliza para hacer facil la escritura de varias controles Puntos_Y el
    /// manejo de tal sin tener que escribir cada control independientemente.
    /// Ademas de que me ayuda a poner control de logica sobre los controles
    /// sin crear funciones en otro lado.
    /// </summary>
    public class ManejadorControles : List<Control>
    {
        #region Elementos

        public int ControlSeleccionado { get; set; }
        public bool AceptarInput { get; set; }
        public SpriteFont Fuente { get; private set; }

        #endregion

        #region Constructores

        public ManejadorControles()
            : base()
        {
            this.Fuente = Fuentes.FuenteBase;
        }

        /// <summary>
        /// Inizializa una lista de controles con la fuente seleccionada.
        /// </summary>
        /// <param name="fuente"> Tipo de fuente a usar </param>
        public ManejadorControles(SpriteFont fuente)
            : base()
        {
            this.Fuente = fuente;
        }

        /// <summary>
        /// Inizializa una lista de cierto tamaño con la fuente seleccionada.
        /// </summary>
        /// <param name="capacidad"> Capacidad de la lista </param>
        /// <param name="fuente"> Tipo de fuente a usar </param>
        public ManejadorControles(int capacidad, SpriteFont fuente)
            : base(capacidad)
        {
            this.Fuente = fuente;
        }

        /// <summary>
        /// Inizializa una lista con una colleccion Puntos_Y la fuente seleccionada.
        /// </summary>
        /// <param name="colleccion"> Colleccion previa </param>
        /// <param name="fuente"> Tipo de fuente a usar </param>
        public ManejadorControles(IEnumerable<Control> colleccion, SpriteFont fuente)
            : base(colleccion)
        {
            this.Fuente = fuente;
        }

        #endregion

        #region Funciones de Juego

        public void Update(GameTime gameTime)
        {
            if (Count == 0)
                return;

            foreach (Control c in this)
            {
                if (c.Activado)
                    c.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Count == 0)
                return;

            foreach (Control c in this)
            {
                if (c.Visible)
                    c.Draw(spriteBatch);
            }
        }

        #endregion

        #region Otros

        #endregion

    }
}
