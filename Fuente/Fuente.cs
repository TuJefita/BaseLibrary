//Test

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using BaseLibrary.Managers;

namespace BaseLibrary.Fuente
{
    public static class Fuentes
    {
        public static SpriteFont FuenteBase { get; private set; }
        public static Dictionary<string, SpriteFont> FuentesCargadas;

        public static void Cargar_Fuentes_Bases(ContentManager Content)
        {
            FuentesCargadas = new Dictionary<string, SpriteFont>();

            FuenteBase = Content.Load<SpriteFont>("BigPix2");

            
        }

        public static void AgregarFuente(string fuente){
            FuentesCargadas.Add(fuente, Managers.Managers.ContentManager.Load<SpriteFont>(fuente));
        }
    }
}
