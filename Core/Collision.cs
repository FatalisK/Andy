using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Andy.Core
{

    public static class Collision
    {
        public enum Direction
        {
            NONE = -1,
            RIGHT = 0,
            LEFT = 1,
            BOT = 2,
            TOP = 3,
            PASS = 4
        }


        private static Color GetColorAt(GameObjects gameObject, World world)
        {
            return Color.Aquamarine;
        }

        public static bool Collided(GameObjects a, World w)
        {
            int i,j;
            for (i = (int)a._sprite._location.X; i < (int)a._sprite.getFrameWidth() + (int)a._sprite._location.X ;i++ )
            {

                for(j = (int)a._sprite._location.Y; j < (int)a._sprite.getFrameWidth() + (int)a._sprite._location.Y ;j++ ){

                   //Console.Write(i + "," + j + "X" + (int)a._sprite._location.X + "Y" + (int)a._sprite._location.Y+ "     ");

                    if(w.colorTab[i,j]==w.collisionColor){
                        return true;
                    }
                }

            }

            return false;

        }
    }
}
    
