using System;
using System.Collections.Generic;
using System.Text;

using BaseLibrary.Controls;
using BaseLibrary.Sprite_Classes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BaseLibrary.Managers
{
    public static class Managers
    {
        public static List<SpriteBase> SpritesManager { get; set; }
        public static ManejadorControles ControlManager { get; set; }

        public static ContentManager ContentManager { get; set; }
        public static Rectangle GameRectangle { get; set; }
    }
}
