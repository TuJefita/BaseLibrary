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

        Vector2 posicion;
        public override Vector2 Posicion 
        {
            get { return posicion; }
            set 
            {
                PosicionLineas = posicion + new Vector2(0, Alto);
                posicion = value;
            }
        }

        public override string Texto { get; set; }
        public override float Tiempo { get; set; }

        public delegate float Funcion(float x);

        Texture2D PointTexture;
        SpriteBase[] Lines;

        public Vector2[] Par_Puntos { get; private set; }
        private Vector2 PosicionLineas;

        public float dX { get; private set; }
        public float X1 { get; set; }
        public float X2 { get; set; }
        private float MaxY;
        private float MinY;
        
        public int Longitud { get; set; }
        public int Alto { get; set; }


        #endregion

        #region Constructores

        public GraphPlot(int longitud, int ancho, GraphicsDevice graphicsDevice)
            : base()
        {
            Longitud = longitud;
            Alto = ancho;
 
            PosicionLineas = new Vector2(Posicion.X, Posicion.Y + Alto);
            Color = Color.Green;

            PointTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            UInt32[] pixe = { 0xFFFFFFFF };
            PointTexture.SetData<UInt32>(pixe, 0, 1);
        }

        #endregion

        #region Funciones de Juego

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PointTexture, Posicion, new Rectangle(0, 0, Longitud, Alto), Color.Black);

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
        public void CreatePlot(Funcion f, float x1, float x2)
        {
            float y;
            float x;
            int i;
            List<float> ys = new List<float>();
            List<float> xs = new List<float>();
            List<Line> lineas = new List<Line>();
            Line l;

            Par_Puntos = new Vector2[40];
            
            /*Se crea una lista de valores con la funcion de N elementos*/
            dX = (x2 - x1) / 40;
            for (i = 0; i < 40; i++)
            {
                x = (i * dX + x1);
                y = f(x);

                if (y != float.NaN && y != float.NegativeInfinity && y != float.PositiveInfinity)
                {
                    xs.Add(x);
                    ys.Add(y);
                }


                Par_Puntos[i] = new Vector2(x, y);
            }
            /***********************************************************/

            /*Se saca los maximos y minimos en X y en Y*/
            X1 = x1;
            X2 = x2;
            MaxY = ys.Max();
            MinY = ys.Min();
            /*******************************************/

            /*Se calculan las lineas iniciales*/
            for (i = 1; i < ys.Count; i++)
            {
                l = CalculateLine(new Vector2(xs[i - 1], ys[i - 1]), new Vector2(xs[i], ys[i]));

                lineas.Add(l);
            }
            /******************************************/
            
            /*Se interpolan valores donde hubo un cambio de signo en la pendiente para mayor precision en las curvas*/
            //TODO: Bug cuando una funcion tiene varios agujeros que van al cielo y/o el infierno
            i = 1;
            bool done = false;
            while (!done)
            {
                float abs = Math.Abs(lineas[i].M + lineas[i - 1].M);
                float sba = Math.Abs(lineas[i].M) + Math.Abs(lineas[i - 1].M);
                if (abs < sba)
                {
                    float midPoint1 = (lineas[i - 1].V2.X + lineas[i - 1].V1.X) / 2;
                    float midPoint2 = (lineas[i].V2.X + lineas[i].V1.X) / 2;

                    float ymidPt1 = f(midPoint1);
                    float ymidPt2 = f(midPoint2);

                    if (ymidPt1 != float.NaN && !float.IsInfinity(ymidPt1) && ymidPt2 != float.NaN && !float.IsInfinity(ymidPt2))
                    {
                        if ((ymidPt1 < MaxY && ymidPt1 > MinY) && ((ymidPt2 < MaxY && ymidPt2 > MinY)))
                        {
                            Line l1 = CalculateLine(new Vector2(midPoint1, ymidPt1), lineas[i - 1].V2);
                            Line l2 = CalculateLine(lineas[i - 1].V2, new Vector2(midPoint2, ymidPt2));
                            lineas[i - 1] = CalculateLine(lineas[i - 1].V1, new Vector2(midPoint1, ymidPt1));
                            lineas[i] = CalculateLine(new Vector2(midPoint2, ymidPt2), lineas[i].V2);

                            lineas.Insert(i, l1);
                            lineas.Insert(i + 1, l2);
                        }
                    }

                }


                if (i == lineas.Count - 1) done = true;
                else i++;
            }
            /*********************************************************************************************************************/
            Lines = new SpriteBase[lineas.Count];

            int k = 0;

            Vector2 Origin = ToPixel(lineas[0].V1);
            PosicionLineas += new Vector2(0, -Origin.Y);

            foreach (Line line in lineas)
            {
                line.P1 = PosicionLineas;
                PosicionLineas += line.P2;
                line.P2 = PosicionLineas;
                Lines[k] = PlotLine(line);
                k++;
            }
            
            
        }
        
        /// <summary>
        /// Crea una grafica a partir de una lista de puntos
        /// </summary>
        /// <param name="Puntos_X"></param>
        /// <param name="Puntos_Y"></param>
        /// <param name="device"></param>
        public void CreatePlot(List<float> Puntos_X, List<float> Puntos_Y)
        {
            float yMax = MaxY = Puntos_Y.Max();
            float yMin = MinY = Puntos_Y.Min();

            float xMax = X2 = Puntos_X.Max();
            float xMin = X1 = Puntos_X.Min();

            List<Line> lineas = new List<Line>();
            Line l;
            int i = 0;

            dX = (xMax - xMin) / Longitud;



            for (i = 1; i < Puntos_Y.Count; i++)
            {
                l = CalculateLine(new Vector2(Puntos_X[i - 1], Puntos_Y[i - 1]), new Vector2(Puntos_X[i], Puntos_Y[i]));

                lineas.Add(l);
            }

            Lines = new SpriteBase[Puntos_Y.Count - 1];

            Vector2 Origin = ToPixel(lineas[0].V1);
            PosicionLineas += new Vector2(0, -Origin.Y);

            i = 0;
            foreach (Line line in lineas)
            {
                line.P1 = PosicionLineas;
                PosicionLineas += line.P2;
                line.P2 = PosicionLineas;
                Lines[i] = PlotLine(line);
                i++;
            }

            
        }

        private Line CalculateLine(Vector2 p1, Vector2 p2)
        {
            Line l = new Line();
            l.V1 = p1;
            l.V2 = p2;

            float xx1 = (((Longitud - 1) / (X2 - X1)) * (p1.X - X1));
            float xx2 = (((Longitud - 1) / (X2 - X1)) * (p2.X - X1));

            float yy1 = (((Alto - 1) / (MaxY - MinY)) * (p1.Y - MinY));
            float yy2 = (((Alto - 1) / (MaxY - MinY)) * (p2.Y - MinY));

            Vector2 t = new Vector2(xx2 - xx1, yy2 - yy1);
            l.P1 = Vector2.Zero;
            l.P2 = new Vector2(t.X, -t.Y);

            l.M = -t.Y / t.X;

            l.Longitud = (int)Math.Round(t.Length());
            l.Angulo = -(float)Math.Atan(t.Y / t.X);

            if (t.X < 0 && t.Y > 0)
                l.Angulo += (float)Math.PI;
            else if (t.X < 0 && t.Y < 0)
                l.Angulo -= (float)Math.PI;

            return l;
        }

        private SpriteBase PlotLineNormal(float x1, float x2, float y1, float y2)
        {
            return PlotLine(CalculateLine(new Vector2(x1, y1), new Vector2(x2, y2)));
        }

        private SpriteBase PlotLine(Line l)
        {
            return new SpriteBase(PointTexture, l.P1 , new Rectangle(0, 0, l.Longitud, 1), l.Angulo);
        }

        private Vector2 ToPixel(Vector2 vector)
        {
            return new Vector2((((Longitud - 1) / (X2 - X1)) * (vector.X - X1)), (((Alto - 1) / (MaxY - MinY)) * (vector.Y - MinY)));
        }

        #endregion

        
    }

    public class Line
    {
        public Vector2 P1;
        public Vector2 V1;
        public Vector2 P2;
        public Vector2 V2;
        public int Longitud;
        public float M;
        public float Angulo;

        public Line()
        {

        }
    }
}
 