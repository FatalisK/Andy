﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Andy.ScreenCore;


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
        private List<Decor> _listeDecor;
        



        public World(Sprite s):base(s)
        {
            _listePlat = new List<Plateforme>();
            _listeCreat = new List<Creature>();
            _listeDecor = new List<Decor>();
            _sol = MenuPrincipal.WINDOW_HEIGHT;
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

            //Console.WriteLine("pp" + p.getVeutSauter());
            if(p.getTypeObjet()==TypeObjet.PERS)
            { 
                if (p.getVeutSauter())
                {
                    if (p.getVitesseY()>0)
                    {
                        if (p.getDirection() == Direction.RIGHT)
                        {
                            p.sprite.location.X = p.sprite.location.X + 0.5f * p.Accel + p.getVitesseX();

                        }
                        if (p.getDirection() == Direction.LEFT)
                        {
                            p.sprite.location.X = p.sprite.location.X - (p.getVitesseX() + 0.5f * p.Accel);

                        }
                        p.sprite.location.Y = p.sprite.location.Y - (0.5f * p.Accel + p.getVitesseY());
                        p.setVitesseY(p.getVitesseY() - 1);
                    }
                    else
                    {
                        p.setVeutSauter(false);
                        p.setVitesseY(p.getVitesseYAbs());
                    }
                    Console.WriteLine(p.getVitesseY());
                }

                if (!p.getVeutSauter())
                {
                    //Console.WriteLine(p.getSol() + "," + (p.sprite.location.Y + p.sprite.getFrameHeight()));

                    if (p.getWorld().getSol() != (p.sprite.location.Y + p.sprite.getFrameHeight()))
                    {


                        p.sprite.location.Y = p.sprite.location.Y + p.Poids;


                        if (p.sprite.location.Y > _sol)
                        {
                            if (p.getTypeObjet() == GameObjects.TypeObjet.PERS)
                            {
                                p.sprite.location.X = 0; p.sprite.location.Y = 400;
                                p.setPvActuel(p.getPvActuel() - 1);
                                //p.setPvActuel(0);//La mort
                            }
                        }

                        //if (p.sprite.location.Y + p.sprite.getFrameHeight() > _sol - p.sprite.getFrameHeight()) { p.sprite.location.Y = 0; p.sprite.location.X = 0; }

                    }
                }

            }
            else
            {



                p.sprite.location.Y = p.sprite.location.Y + p.Poids;

                //Console.WriteLine("ici" + p.Poids);
                //if (p.sprite.location.Y + p.sprite.getFrameHeight() > _sol - p.sprite.getFrameHeight()) { p.sprite.location.Y = 0; p.sprite.location.X = 0; }



            }

            if (p.collisionEnAir)
            {
                //sprite.location.Y = sprite.location.Y - (0.5f * Accel + _vitesse.Y);
                
                p.collisionEnAir = false;

            }
            
        }


 

        public void leMondeEve()
        {
            Collision.Collided(_player, -1, _player.getWorld());
            _player.Move(Keyboard.GetState(), GamePad.GetState(PlayerIndex.One));
            _player.updatePlayer();
           gravite(_player);

           for (int i = 0; i < _listeCreat.Count; i++)
           {
               gravite(_listeCreat[i]);

               _listeCreat[i].setCollisions(Collision.Collided(_listeCreat[i],i,_listeCreat[i].getWorld()));
               _listeCreat[i].updateCrea();
               if (_listeCreat[i].getPvActuel() == 0)
               {
                   _listeCreat[i].setMort(true);
                   _listeCreat.RemoveAt(i);
               }
           }
        }
}
}
