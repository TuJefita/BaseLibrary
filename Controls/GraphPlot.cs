/*
* Nombre: Hector Hawley Herrera
* Fecha de creación: 03 de Enero del 2015
* Fecha de Ultima modificación: 03 de Enero del 2015
* Descripcion:
*/
        
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BaseLibrary.Sprite_Classes;

namespace BaseLibrary.Controls
{
    public class GraphPlot : Control
    {
        #region Elementos

        public delegate float Funcion(float x);

        Texture2D PointTexture;
        SpriteBase[] Lines;

        public Vector2[] Par_Puntos { get; private set; }

        public float dX { get; private set; }
        public float X1 { get; set; }
        public float X2 { get; set; }
        private float MaxY;
        private float MinY;
        

        public int N { get; set; }
        public int Longitud { get; set; }
        public int Alto { get; set; }

        public Color PlotColor { get; set; }

        #endregion

        #region Constructores

        public GraphPlot(int longitud, int ancho)
            : base()
        {
            Longitud = longitud;
            Alto = ancho;
            N = ancho;
            Par_Puntos = new Vector2[N];
            PlotColor = Color.Red;
        }

        #endregion

        #region Funciones de Juego

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (SpriteBase b in Lines)
            {
                b.Dibujar(spriteBatch);
            }
           
        }

        #endregion

        #region Otros

        /// <summary>
        /// Crear una grafica a partir de una Funcion.
        /// </summary>
        /// <param name="f"> Funcion que regresa un float </param>
        /// <param name="x1"> Limite inferior </param>
        /// <param name="x2"> Limite superior</param>
        /// <param name="device"> GraphicsDevice del juego </param>
        public void CreatePlot(Funcion f, float x1, float x2, GraphicsDevice device)
        {
            float y;
            float x;
            int i;
            List<float> ys = new List<float>();
            List<float> xs = new List<float>();

            PointTexture = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixe = { 0xFFFFFF };
            PointTexture.SetData<Int32>(pixe, 0, 1);

            Lines = new SpriteBase[15];
            dX = (x2 - x1) / 16;
            
            for (i = 0; i < 16; i++)
            {
                x = (i * dX + x1);
                xs.Add(x);

                y = f(x);
                ys.Add(y);


                Par_Puntos[i] = new Vector2(x, y);
            }

            X1 = x1;
            X2 = x2;
            float yMax = MaxY = ys.Max();
            float yMin = MinY = ys.Min();


            for (i = 1; i < ys.Count; i++)
            {
                Lines[i - 1] = PlotLine(xs[i - 1], xs[i], ys[i - 1], ys[i]);

            }
            
            
        }
        
        /// <summary>
        /// Crea una grafica a partir de una lista de puntos
        /// </summary>
        /// <param name="Puntos_X"></param>
        /// <param name="Puntos_Y"></param>
        /// <param name="device"></param>
        public void CreatePlot(List<float> Puntos_X, List<float> Puntos_Y, GraphicsDevice device)
        {
            PointTexture = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixe = { 0xFFFFFF };
            PointTexture.SetData<Int32>(pixe, 0, 1);

            float yMax = MaxY = Puntos_Y.Max();
            float yMin = MinY = Puntos_Y.Min();

            float xMax = X2 = Puntos_X.Max();
            float xMin = X1 = Puntos_X.Min();

            dX = (xMax - xMin) / Longitud;

            Lines = new SpriteBase[Puntos_Y.Count - 1];

            int i = 0;

            for (i = 1; i < Puntos_Y.Count; i++)
            {
                Lines[i - 1] = PlotLine(Puntos_X[i - 1], Puntos_X[i], Puntos_Y[i - 1], Puntos_Y[i]);
            }

            
        }

        private SpriteBase PlotLine(float x1, float x2, float y1, float y2)
        {
            float xx1 = (((Longitud - 1) / (X2 - X1)) * (x1 - X1));
            float xx2 = (((Longitud - 1) / (X2 - X1)) * (x2 - X1));

            float yy1 = (((Alto - 1) / (MaxY - MinY)) * (y1 - MinY));
            float yy2 = (((Alto - 1) / (MaxY - MinY)) * (y2 - MinY));

            float dxx = xx2 - xx1;
            float dyy = yy2 - yy1;

            Vector2 temp = new Vector2(dxx, dyy);

            Posicion += new Vector2(dxx, -dyy);
            int l = (int)Math.Round(temp.Length());
            float rad = (float)Math.Asin(-dyy / temp.Length());
            return new SpriteBase(PointTexture, Posicion - new Vector2(dxx, -dyy), new Rectangle(0, 0, l, 1), rad);
        }
        #endregion
    }
}
 