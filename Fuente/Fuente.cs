using System;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLibrary.Fuente
{
    public static class Fuente
    {
        public static SpriteFont Fuente_01 { get; private set; }

        public static void Cargar_Fuentes(ContentManager Content)
        {
            Fuente_01 = Content.Load<SpriteFont>("BigPix2");
        }
    }
}
