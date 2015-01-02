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

        public Label Label 
        { 
            get { return label;}
            private set { label = value; } 
        }

        #endregion

        #region Constructores

        public Boton(string texto, SpriteFont fuente)
            : base()
        {
            //TODO: Cargar Imagenes
            label = new Label();
            label.Fuente = fuente;
            label.Texto = texto;
            label.TamañoFuente = 1;

        }

        #endregion

        #region Funciones de Juego

        public void LoadContent(ContentManager Content)
        {
            BotonBase = Content.Load<Texture2D>("boton");
            BottonAplastado = Content.Load<Texture2D>("botonAplastado");

            label.Posicion = new Vector2(Posicion.X + (BotonBase.Bounds.Width - label.Tamaño.X) / 2, Posicion.Y + (BotonBase.Bounds.Height - label.Tamaño.Y) / 2);
        }

        public override void Update(GameTime gameTime)
        {
#if WINDOWS || LINUX 
            if (MouseInput.MouseRectangle().Intersects(
                new Rectangle(
                    (int)Posicion.X, 
                    (int)Posicion.Y, 
                    BotonBase.Bounds.Width, 
                    BotonBase.Bounds.Height)))
            {
                if(MouseInput.ClickOcurred())
                    EnSeleccionado(null);
            }
#elif ANDROID
            //TODO: Agregar touch input
#endif
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Seleccionar)
            {
                spriteBatch.Draw(BottonAplastado, Posicion, BottonAplastado.Bounds, Color);
            }
            else
            {
                spriteBatch.Draw(BotonBase, Posicion, BotonBase.Bounds, Color);
            }

            label.Draw(spriteBatch);
        }

        #endregion

        #region Otros

        public void CambiarPosicion(Vector2 posicion)
        {
            Posicion = posicion;
            label.Posicion = new Vector2(Posicion.X + (BotonBase.Bounds.Width - label.Tamaño.X) / 2, Posicion.Y + (BotonBase.Bounds.Height - label.Tamaño.Y) / 2);
        }

        #endregion

    }
}
