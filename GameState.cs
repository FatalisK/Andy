using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Andy
{
    public abstract class GameState
    {

        public virtual  void Initialize(){

        }
        public void Draw()
        {

        }

        public void DrawAnimation()
        {
        }

        public void LoadContent()
        {
 
        }

        public void UnloadContent()
        {

        }

       


    }
}
