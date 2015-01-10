/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 01 de Enero del 2015
* Fecha de Ultima modificación: 09 de Enero del 2015
* Descripcion: Boton.
*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using BaseLibrary.Fuente; 
using BaseLibrary.Input;
using BaseLibrary.Managers;

namespace BaseLibrary.Controls
{
    public class Boton : Control
    {
        #region Elementos

        Vector2 posicion;
        public override Vector2 Posicion 
        {
            get { return posicion; } 
            set 
            { 
                Label.Posicion = new Vector2(value.X + (Bordes.Width - label.Tamaño.X) / 2, value.Y + (Bordes.Height - label.Tamaño.Y) / 2);
                posicion = value;
            }
        }

        public override string Texto
        {
            get
            {
                return Label.Texto;
            }
            set
            {
                if (Label == null)
                {
                    Label = new Label();
                    Label.Texto = value;
                }
                else Label.Texto = value;
            }
        }

        public override float Tiempo { get; set; }

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

        public bool TextoVisible 
        { 
            get { return Label.Visible; }
            set { Label.Visible = value; }
        }

        public Rectangle Bordes
        {
            get;
            set;
        }

        #endregion

        #region Constructores

        public Boton()
            : base()
        {
            if (label == null)
                label = new Label();

            LoadContent();

            Bordes = new Rectangle(0, 0, (int)label.Tamaño.X, (int)label.Tamaño.Y);
        }

        #endregion

        #region Funciones de Juego

        /// <summary>
        /// Carga el boton generico.
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent()
        {
            BotonBase = Managers.Managers.ContentManager.Load<Texture2D>("base");
            BottonAplastado = Managers.Managers.ContentManager.Load<Texture2D>("base");
            
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
            if(TouchInput.IntersectsGesture(Posicion, Bordes.Width, Bordes.Height))
            {
                if(TouchInput.TapOcurred())
                    EnClick(null);
            }
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

            if(Label.Visible)
                label.Draw(spriteBatch);
        }

        #endregion

        #region Otros



        #endregion

    }
}
