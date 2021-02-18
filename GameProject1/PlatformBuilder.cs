using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject1.Collisions;

namespace GameProject1
{
    public static class PlatformBuilder
    {
        public static void GeneratePlatforms(List<PlatformSprite> platformList)
        {
            platformList.Add(new PlatformSprite(new Vector2(300, 100)));
        }
    }
}
