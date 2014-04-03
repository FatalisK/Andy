using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Andy.Core
{
    public class World : GameObjects
    {

        /*Un monde contient une physique, gravité*/
        protected float _gravity;
        protected float _sol;
        public bool toucheSol;

        private List<World> _listeMonde;
        



        public World(Sprite s):base(s)
        {
            _listeMonde = new List<World>();
            _sol = 600;
            _gravity = 9.81f;
            toucheSol = true;

        }

        public virtual void colllisionPlat(World w, List<Vector2> v) { }

        public float getGravity()
        {
            return _gravity;
        }

        public void setSol(float f){
            _sol=f;
        }

        public float getSol()
        {
            return _sol;
        }
        public void ajouterElem(World e)
        {
            _listeMonde.Add(e);
   
        }

        public int tailleListe()
        {
            return _listeMonde.Count();
        }

        public World getElem(int i)
        {
            return _listeMonde[i];
        }

        public void Physique(Player p)
        {
            float Poids = p.getMasse() * _gravity;
            float Accel = Poids; //+ p.getSaut();
            if (_sol == p.sprite.location.Y+p.sprite.getFrameHeight()) { toucheSol = true; } else { toucheSol = false; }
            if (p.getVeutSauter())
            {
                if (sprite.location.Y > p.getHauteurSaut())
                {
                    if (_direction == Collision.Direction.RIGHT)
                    {
                        sprite.location.X = sprite.location.X + 0.5f * Accel + _vitesse.X;

                    }
                    if (_direction == Collision.Direction.LEFT)
                    {
                        sprite.location.X = sprite.location.X - (_vitesse.X + 0.5f * Accel);

                    }
                    sprite.location.Y = sprite.location.Y - (0.5f * Accel + _vitesse.Y);
                }
                else
                {
                    p.setVeutSauter(false);
                }

                //_vitesse.X = 0.5f * Accel + _vitesse.X + sprite.location.X;
                //_vitesse.Y = 0.5f * Accel + _vitesse.Y + sprite.location.Y;
            }
            
            if (!p.getVeutSauter())
            {
                //Console.WriteLine(p.getSol() + "," + (p.sprite.location.Y + p.sprite.getFrameHeight()));

                if (p.getSol() != (p.sprite.location.Y + p.sprite.getFrameHeight()))
                {
                    if (sprite.location.Y < _sol - p.sprite.getFrameHeight()) { sprite.location.Y = sprite.location.Y + (0.5f * Accel + _vitesse.Y); }

                    if (sprite.location.Y > _sol - p.sprite.getFrameHeight()) { sprite.location.Y = _sol - p.sprite.getFrameHeight(); }
                }
            }
            

            if (collisionEnAir)
            {
                //sprite.location.Y = sprite.location.Y - (0.5f * Accel + _vitesse.Y);
                collisionEnAir = false;

            }

        }

    }
}
