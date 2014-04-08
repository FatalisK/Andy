using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andy.Core
{
    class Decor: GameObjects
    {
        private float mouvementX;
        private float mouvementY;
        public Decor(Sprite s)
            : base(s)
        {
            mouvementX = 0;
            mouvementY = 0;
        }

        public Decor(Sprite s,float X, float Y)
            : base(s)
        {
            mouvementX = X;
            mouvementY = Y;
        }

        public void bouger()
        {
            sprite.location.X = sprite.location.X + mouvementX;
            if (sprite.location.X > Andy.WINDOW_WIDTH + sprite.getFrameWidth())
            {
                sprite.location.X = 0-sprite.getFrameWidth();
            }
        }
    }
}
