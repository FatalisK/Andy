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

        private Player _player;
        private List<Plateforme> _listePlat;
        private List<Creature> _listeCreat;
        



        public World(Sprite s):base(s)
        {
            _listePlat = new List<Plateforme>();
            _listeCreat = new List<Creature>();
            _sol = 600;
            _gravity = 9.81f;
            toucheSol = true;

        }

        public Player getPlayer()
        {
            return _player;
        }

        public void setPlayer(Player p)
        {
            _player = p;
        }
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

        /*Acces ListePlat*/
        public int tailleListPlat(){
            return _listePlat.Count;
        }

        public Plateforme getListPlat(int i)
        {
            return _listePlat[i];
        }

        public void addListPlat(Plateforme p)
        {
            _listePlat.Add(p);
        }

        /*Acces ListeCreature*/
        public int tailleListCreat()
        {
            return _listeCreat.Count;
        }

        public Creature getListCreat(int i)
        {
            return _listeCreat[i];
        }

        public void addListCreat(Creature c)
        {
            _listeCreat.Add(c);
        }

 
        public World getElem(int i)
        {
            return _listePlat[i];
        }


        public void gravite(Creature p)
        {
           
            float Poids = p.getMasse() * p.getWorld().getGravity();
            float Accel = Poids + p.getSaut();
            //Console.WriteLine("pp" + p.getVeutSauter());

            if (p.getVeutSauter())
            {
                if (p.sprite.location.Y > p.getHauteurSaut())
                {
                    if (p.getDirection() == Direction.RIGHT)
                    {
                        p.sprite.location.X = p.sprite.location.X + 0.5f * Accel + p.getVitesseX();

                    }
                    if (p.getDirection() == Direction.LEFT)
                    {
                        p.sprite.location.X = p.sprite.location.X - (p.getVitesseX() + 0.5f * Accel);

                    }
                    p.sprite.location.Y = p.sprite.location.Y - (0.5f * Accel + p.getVitesseY());
                }
                else
                {
                    p.setVeutSauter(false);
                }
            }
            
            if (!p.getVeutSauter())
            {
                //Console.WriteLine(p.getSol() + "," + (p.sprite.location.Y + p.sprite.getFrameHeight()));

                if (p.getWorld().getSol() != (p.sprite.location.Y + p.sprite.getFrameHeight()))
                {
                   // Console.WriteLine("P" + Poids + "Physique Monde"+(0.5f * Accel + _vitesse.Y));

                    if (p.sprite.location.Y < _sol - p.sprite.getFrameHeight()) { p.sprite.location.Y = p.sprite.location.Y + (0.5f * Accel + p.getVitesseY()); }

                    if (p.sprite.location.Y > _sol - p.sprite.getFrameHeight()) { p.sprite.location.Y = _sol - p.sprite.getFrameHeight(); }

                }
            }
            

            if (p.collisionEnAir)
            {
                //sprite.location.Y = sprite.location.Y - (0.5f * Accel + _vitesse.Y);
                
                p.collisionEnAir = false;

            }

        }
        

        public void Physique()
        {
           gravite(_player);
           Collision.Collided(_player,-1, _player.getWorld());
           for (int i = 0; i < _listeCreat.Count; i++)
           {
               gravite(_listeCreat[i]);
               _listeCreat[i].setCollisions(Collision.Collided(_listeCreat[i],i,_listeCreat[i].getWorld()));
           }
        }
}
}
