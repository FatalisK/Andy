using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Andy.Core
{
    class Plateforme : World
    {
        private Vector2 _positionBase; 
        public bool collision;
        public Plateforme(Sprite s, Vector2 positionBase ):base(s) {

            collision = false;
            typeobjet = TypeObjet.PLAT;
            _positionBase=positionBase;
        }

        public void se_deplacer(int x, int y)
        {
            if (sprite.location.X > _positionBase.X + x) { _direction = Collision.Direction.LEFT; }
            if (sprite.location.X <= _positionBase.X) { _direction = Collision.Direction.RIGHT; }

            if (_direction == Collision.Direction.RIGHT) { sprite.location.X = sprite.location.X + 2; }
            if (_direction == Collision.Direction.LEFT) { sprite.location.X = sprite.location.X - 2; }





        }

        public override void colllisionPlat(World a,List<Vector2> inter)
        {
            int maxX = 0;
            int minX = 1000;
            int maxY = 1000;//Y inversé
            int minY = 0;
            int margeErX=4;
            int margeErY = 4;
   

            int[] tabCollision = new int[4];//0 gauche 1 droite 2 haut 3 bas
            //Console.WriteLine("Je me suis fait intersecté par un objet de type" + a.typeobjet);

            //if (a.typeobjet == GameObjects.TypeObjet.PERS)//Collision avec une personne
            //{

                //Objectif repousser le personnage de la zone
                int i;
                sprite.initCoulour();

                for (i = 0; i < sprite.pixelColor.Count(); i++)
                {
                    sprite.pixelColor[i] = Color.White;

                }
                
                for (i = 0; i < inter.Count; i++)
                {

                    if (inter[i].X > maxX) { maxX = i; }
                    if (inter[i].X < minX) { minX = i; }
                    if (inter[i].Y > minY) { minY = i; } // y orienté vers le bas
                    if (inter[i].Y < maxY) { maxY = i; }


                    if (inter[i].Y >= sprite.Bbox.Top - margeErY && inter[i].Y <= sprite.Bbox.Top + margeErY)
                    {
                        tabCollision[2]++;
                    }
                    //BOT
                    if (inter[i].Y >= sprite.Bbox.Bottom - margeErY && inter[i].Y <= sprite.Bbox.Bottom + margeErY && a.getVeutSauter())
                    {
                        tabCollision[3]++;
                    }

                    if (inter[i].X >= sprite.Bbox.Left - margeErX && inter[i].X <= sprite.Bbox.Left + margeErX)
                    {//Coté gauche
                        tabCollision[0]++;
                    }
                    //Console.WriteLine("X" + inter[i].X + "BR" + sprite.Bbox.Right);
                    if (inter[i].X >= sprite.Bbox.Right - margeErX && inter[i].X <= sprite.Bbox.Right + margeErX )
                    {//Coté droit
                        tabCollision[1]++;
                    }

                


                    //Console.WriteLine(inter[i]);
                    sprite.pixelColor[(int)((inter[i].X - sprite.Bbox.Left) +
                                         (inter[i].Y - sprite.Bbox.Top) * sprite.Bbox.Width)] = Color.Black;
                    
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
                sprite.getTexture().SetData(sprite.pixelColor);
                
                if (max==1  )
                    {

                        a.sprite.location.X = a.sprite.location.X + a.getVitesseX();
                        if (inter[minX].X < sprite.Bbox.Right)//Il est dedans la boite il faut l'ejecter !
                        {
                           a.sprite.location.X = a.sprite.location.X + (sprite.Bbox.Right - inter[minX].X);
                        }
                    }
                
                    //Se cogne à gauche
                
                     if (max==0)
                    {
                        //Console.WriteLine("assss");
                        //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left);
                        a.sprite.location.X = a.sprite.location.X - a.getVitesseX();
                        if (inter[maxX].X > sprite.Bbox.Left)//Il est dedans la boite il faut l'ejecter !
                        {
                            a.sprite.location.X = a.sprite.location.X - (inter[maxX].X-sprite.Bbox.Left );
                            
                        }
                    }
                
                    //se cogne en haut
                     if (max==2)
                     {

                         float Poids = a.getMasse() * a.getGravity();
                         float Accel = Poids + a.getSaut();
                         //Console.WriteLine("Mass"+a.getMasse()+"P"+Poids+"M"+(0.5f * Accel + a.getVitesseY()));
                         a.sprite.location.Y = a.sprite.location.Y -  (0.5f * Accel + a.getVitesseY());
                         a.collisionEnAir = true;
                         if (inter[minY].Y > sprite.Bbox.Top)//Il est dedans la boite il faut l'ejecter !
                         {
                             a.sprite.location.Y = a.sprite.location.Y - (inter[minY].Y - sprite.Bbox.Top );
                         }
                     }

                     //se cogne en bas
                     if (max == 3)
                     {
                         //Console.WriteLine("Mass"+a.getMasse()+"P"+Poids+"M"+(0.5f * Accel + a.getVitesseY()));
                         a.setVeutSauter(false);

                     }
                
                
            //}
        }




            

        }

    }

