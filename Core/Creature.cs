using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;

namespace Andy.Core
{
    public class Creature:World
    {
        protected World _world;
        protected float _mass;
        protected float _saut;
        protected Vector2 _vitesse;
        public Creature(Sprite s, World world)
            :base(s)
        {
            _world = world;

        }

        public override float getVitesseX()
        {
            return _vitesse.X;
        }

        public override float getVitesseY()
        {
            return _vitesse.Y;
        }

        public World getWorld()
        {
            return _world;
        }
    }
}
