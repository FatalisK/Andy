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
        public bool collision;
        public Plateforme(Sprite s):base(s) {

            collision = false;
            typeobjet = TypeObjet.PLAT;
        
        }

         

        public override void colllisionPlat(World a,List<Vector2> inter)
        {
            int maxX = 0;
            int minX = 1000;
            int maxY = 0;
            int minY = 1000;
            //Console.WriteLine("Je me suis fait intersecté par un objet de type" + a.typeobjet);

            if (a.typeobjet == GameObjects.TypeObjet.PERS)//Collision avec une personne
            {

                Console.WriteLine(inter.Count);
                //Objectif repousser le personnage de la zone
                int i;
                sprite.initCoulour();

                for (i = 0; i < sprite.pixelColor.Count(); i++)
                {
                    sprite.pixelColor[i] = Color.White;

                }

                for(i=0;i<inter.Count;i++){

                    if (inter[i].X > maxX) { maxX = i; }
                    if (inter[i].X < minX) { minX = i; }
                    if (inter[i].Y > maxY) { maxY = i; }
                    if (inter[i].Y < minY) { minY = i; }


                    sprite.pixelColor[(int)((inter[i].X - sprite.Bbox.Left) +
                                         (inter[i].Y - sprite.Bbox.Top) * sprite.Bbox.Width)] = Color.Black;
                
                }

                sprite.getTexture().SetData(sprite.pixelColor);
                /*
                float Poids = a.getMasse() * _gravity;
                float Accel = Poids + a.getSaut();
                Console.WriteLine("X" + x + "Bl" + sprite.Bbox.Left + "Br" + sprite.Bbox.Right + "Y" + y + "Bt" + sprite.Bbox.Top);

                if (y >= sprite.Bbox.Top && y < sprite.Bbox.Top + 1)
                {
                    a.sprite.location.Y = a.sprite.location.Y - (0.5f * Accel + a.getVitesseY());
                                        a.collisionEnAir = true;

                }
                */
                //Console.WriteLine("maxX" + inter[maxX] + "minX" + inter[minX] + "maxY" + inter[maxY] + "minY" + inter[minY]);
                /*COTE GAUCHE*/

                if (inter[minY].Y == sprite.Bbox.Top)
                {
                    float Poids = a.getMasse() * a.getGravity();
                    float Accel = Poids + a.getSaut();
                    a.sprite.location.Y = a.sprite.location.Y - (0.5f * Accel + a.getVitesseY());
                    a.collisionEnAir = true;
               

                }
                

                    if (inter[minX].X <= sprite.Bbox.Right && inter[minX].X >= sprite.Bbox.Right - sprite.Bbox.Width / 2 && inter[minY].Y !=sprite.Bbox.Top)
                    {
                        if (inter[minX].X < sprite.Bbox.Right)//Il est dedans la boite il faut l'ejecter !
                        {
                            a.sprite.location.X = a.sprite.location.X + (sprite.Bbox.Right - inter[minX].X);
                        }
                        a.sprite.location.X = a.sprite.location.X + a.getVitesseX();
                    }
                
                /*COTE DROIT*/

                if (inter[maxX].X >= sprite.Bbox.Left && inter[maxX].X < sprite.Bbox.Left + sprite.Bbox.Width / 2 && inter[minY].Y != sprite.Bbox.Top)
                {
                    if(inter[maxX].X>sprite.Bbox.Left)//Il est dedans la boite il faut l'ejecter !
                    {
                        a.sprite.location.X = a.sprite.location.X -(inter[maxX].X-sprite.Bbox.Left);
                    }
                    a.sprite.location.X = a.sprite.location.X - a.getVitesseX();
                }
                /*
                if (x <= sprite.Bbox.Right && x > sprite.Bbox.Right - 15 && y > sprite.Bbox.Top)
                {
                    a.sprite.location.X = a.sprite.location.X + a.getVitesseX();
                }

                */
    
                }
        }




            

        }

    }

