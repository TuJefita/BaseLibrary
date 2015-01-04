/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 01 de Enero del 2015
* Descripcion: Boton.
*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using BaseLibrary.Fuente; 
using BaseLibrary.Input;  

namespace BaseLibrary.Controls
{
    public class Boton : Control
    {
        #region Elementos

        public Texture2D BotonBase;
        public Texture2D BottonAplastado;

        private Label label;

        /// <summary>
        /// Texto que aparece en medio del boton.
        /// </summary>
        public Label Label 
        { 
            get { return label;}
            private set { label = value; } 
        }

        public Rectangle Bordes
        {
            get;
            set;
        }

        #endregion

        #region Constructores

        public Boton(string texto, SpriteFont fuente)
            : base()
        {
            label = new Label();
            label.Fuente = fuente;
            label.Texto = texto;
            label.TamañoFuente = 1;
            Bordes = new Rectangle(0, 0, 16, 16);
        }

        #endregion

        #region Funciones de Juego

        /// <summary>
        /// Carga el boton generico.
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            BotonBase = Content.Load<Texture2D>("boton");
            BottonAplastado = Content.Load<Texture2D>("botonAplastado");

            label.Posicion = new Vector2(Posicion.X + (BotonBase.Bounds.Width - label.Tamaño.X) / 2, Posicion.Y + (BotonBase.Bounds.Height - label.Tamaño.Y) / 2);
            Bordes = BotonBase.Bounds;
        }

        public override void Update(GameTime gameTime)
        {
#if WINDOWS || LINUX 
            if (MouseInput.Intersects(Posicion, Bordes.Width, Bordes.Height))
            {
                if(MouseInput.ClickOcurred())
                    EnClick(null);
            }
#elif ANDROID
            //TODO: Agregar touch input
#endif
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Seleccionar)
            {
                spriteBatch.Draw(BottonAplastado, Posicion, Bordes, Color);
            }
            else
            {
                spriteBatch.Draw(BotonBase, Posicion, Bordes, Color);
            }

            label.Draw(spriteBatch);
        }

        #endregion

        #region Otros

        /// <summary>
        /// Funcion obligatoria para cambiar la Funcion del boton. Cambia el boton y el texto.
        /// </summary>
        /// <param name="posicion"></param>
        public void CambiarPosicion(Vector2 posicion)
        {
            Posicion = posicion;
            label.Posicion = new Vector2(Posicion.X + (Bordes.Width - label.Tamaño.X) / 2, Posicion.Y + (Bordes.Height - label.Tamaño.Y) / 2);
        }

        #endregion

    }
}
