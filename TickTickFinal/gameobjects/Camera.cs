using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTick5.gameobjects
{
    static class Camera
    {
        public static Rectangle viewPort = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
        public static Vector2 cameraPosition = new Vector2(300, 400);

        public static void Update()
        {
            //cameraPosition = new Vector2(player.x, player.y);
        }
    }
}
