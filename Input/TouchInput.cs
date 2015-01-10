using System;
using System.Collections.Generic;
using System.Text;

using Android.App;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BaseLibrary.Input
{
#if ANDROID
    public static class TouchInput
    {
        #region Elementos

        static TouchCollection CurrentTouchState;
        static GestureSample CurrentGestureState;
        static bool tapOcurred;
        static bool doubletabOcurred;
        #endregion

        #region Constructores

        #endregion

        #region Funciones de Juego

        public static void Update(GameTime gameTime)
        {
            CurrentTouchState = TouchPanel.GetState();
            
            if (TouchPanel.IsGestureAvailable)
            {
                CurrentGestureState = TouchPanel.ReadGesture();

                switch (CurrentGestureState.GestureType)
                {
                    case GestureType.Tap:
                        tapOcurred = true;
                        break;

                    case GestureType.DoubleTap:
                        doubletabOcurred = true;
                        break;

                    default:
                        tapOcurred = false;
                        doubletabOcurred = false;
                        break;
                }
            }
            else
            {
                tapOcurred = false;
                doubletabOcurred = false;
            }
        }

        #endregion

        #region Otros

        public static TouchCollection GetCurrentTouchState()
        {
            return CurrentTouchState;
        }

        public static bool TapOcurred()
        {
            return tapOcurred;
        }

        public static bool DoubleTapOcurred()
        {
            return CurrentGestureState.GestureType == GestureType.DoubleTap;
        }

        public static Vector2 LastGesturePosition()
        {
            return CurrentGestureState.Position;
        }

        public static Rectangle GestureRectangle()
        {
            return new Rectangle((int)CurrentGestureState.Position.X, (int)CurrentGestureState.Position.Y, 1, 1);
        }

        public static bool IntersectsGesture(Vector2 Posicion, int width, int height)
        {
            return (new Rectangle((int)Posicion.X, (int)Posicion.Y, width, height)).Intersects(GestureRectangle());
        }

        #endregion
    }
#endif
}
