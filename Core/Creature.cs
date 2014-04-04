using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;

namespace Andy.Core
{
    public class Creature :GameObjects
    {
        protected World _world;
        protected float _mass;
        protected float _saut;
        public bool collisionEnAir;
        protected Vector2 _vitesse;
        public Creature(Sprite s,  World  world)
            :base(s)
        {
            _world = world;

        }

        public Direction getDirection()
        {
            return _direction;
        }

        public virtual float getVitesseX()
        {
            return _vitesse.X;
        }

        public virtual float getVitesseY()
        {
            return _vitesse.Y;
        }

        public World getWorld()
        {
            return _world;
        }

        public float getSaut() { return _saut; }//Sale peut mieux faire
        public float getMasse() { return _mass; }//idem

        public virtual void setVeutSauter(bool b) { }//
        public virtual bool getVeutSauter()
        {
            return false;
        }


        public virtual float getHauteurSaut()
        {
            return 1;
        }

    }
}
