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
        protected int _collisions;
        public float Poids;
        public float Accel;
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

        public void setCollisions(int c)
        {
            _collisions = c;
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

        public override void colllision(GameObjects p, List<Vector2> inter)
        {
            int maxX = 0;
            int minX = 1000;
            int maxY = 1000;//Y inversés
            int minY = 0;
            int margeErX = 4;
            int margeErY = 4;

            int[] tabCollision = new int[4];//0 gauche 1 droite 2 haut 3 bas
            //Console.WriteLine("Je me suis fait intersecté par un objet de type" + a.typeobjet);

            //if (a.typeobjet == GameObjects.TypeObjet.PERS)//Collision avec une personne
            //{

            //Objectif repousser le personnage de la zone
            int i;
            p.sprite.initCoulour();
/*
           for (i = 0; i < p.sprite.pixelColor.Count(); i++)
            {
                if (p.sprite.pixelColor[i].A != 0) { p.sprite.pixelColor[i] = Color.White; }

            }
            
   */         for (i = 0; i < inter.Count; i++)
            {

                
                if (inter[i].X > maxX) { maxX = i; }
                if (inter[i].X < minX) { minX = i; }
                if (inter[i].Y > minY) { minY = i; } // y orienté vers le bas
                if (inter[i].Y < maxY) { maxY = i; }
                

                if (inter[i].Y >= p.sprite.Bbox.Top - margeErY && inter[i].Y <= p.sprite.Bbox.Top + margeErY)
                {
                    tabCollision[2]++;
                }
                //BOT
                if (inter[i].Y >= p.sprite.Bbox.Bottom - margeErY && inter[i].Y <= p.sprite.Bbox.Bottom + margeErY && getVeutSauter())
                {
                    tabCollision[3]++;
                }

                if (inter[i].X >= p.sprite.Bbox.Left - margeErX && inter[i].X <= p.sprite.Bbox.Left + margeErX)
                {//Coté gauche
                    tabCollision[0]++;
                }
                //Console.WriteLine("X" + inter[i].X + "BR" + sprite.Bbox.Right);
                if (inter[i].X >= p.sprite.Bbox.Right - margeErX && inter[i].X <= p.sprite.Bbox.Right + margeErX)
                {//Coté droit
                    tabCollision[1]++;
                }




                //Console.WriteLine(inter[i]);
                /*p.sprite.pixelColor[(int)((inter[i].X - p.sprite.Bbox.Left) +
                                     (inter[i].Y - p.sprite.Bbox.Top) * p.sprite.Bbox.Width)] = Color.Black;
                */
            }
            int max = 0;
            int val = 0;
            for (i = 0; i < 4; i++)
            {
                if (tabCollision[i] > val)
                {
                    val = tabCollision[i];
                    max = i;

                }
                //Console.WriteLine(max); 
            }
            //Console.WriteLine("G" + tabCollision[0] + "D" + tabCollision[1] + "T" + tabCollision[2] + "B" + tabCollision[3] + "Max" + max);
            p.sprite.getTexture().SetData(p.sprite.pixelColor);

            if (max == 1)
            {
                Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left + "T" + getTypeObjet());

                sprite.location.X = sprite.location.X + getVitesseX();
                if (inter[minX].X < p.sprite.Bbox.Right)//Il est dedans la boite il faut l'ejecter !
                {
                    sprite.location.X = sprite.location.X + (p.sprite.Bbox.Right - inter[minX].X);
                }
            }

            //Se cogne à gauche

            if (max == 0)
            {

                //Console.WriteLine("assss");
                //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left + "T" + getTypeObjet());
                sprite.location.X = sprite.location.X - getVitesseX();
                if (inter[maxX].X > p.sprite.Bbox.Left && inter[maxX].X < p.sprite.Bbox.Left+p.sprite.Bbox.Width/2)//Il est dedans la boite il faut l'ejecter !
                {
                  sprite.location.X = sprite.location.X - (inter[maxX].X - p.sprite.Bbox.Left);

                }
            }

            //se cogne en haut
            if (max == 2)
            {


                //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left + "T" + getTypeObjet());
                //sprite.location.Y = sprite.location.Y - (0.5f * Accel + getVitesseY()); //Pourquoi !
                collisionEnAir = true;
                if (inter[minY].Y > p.sprite.Bbox.Top)//Il est dedans la boite il faut l'ejecter !
                {
                    sprite.location.Y = sprite.location.Y - (inter[minY].Y - p.sprite.Bbox.Top);
                }
            }

            //se cogne en bas
            if (max == 3)
            {
                //Console.WriteLine("Mass"+a.getMasse()+"P"+Poids+"M"+(0.5f * Accel + a.getVitesseY()));
                setVeutSauter(false);

            }


            //}
        }      

    }
}
