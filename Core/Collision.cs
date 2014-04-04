using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Andy.Core
{

    public static class Collision
    {
  

        public static int Collided (Creature c,World w){
            int k;
            int i = 0;
            int size;
            if ((size=w.tailleListPlat())>0)
            {
                for (k = 0; k < size; k++)
                {
                    i = i + Collided2Obj(c, w.getListPlat(k));

                }
            }
            return i;
        }
        /*
         public static int Collided (Plateforme p,World w){
            int k;
            int i = 0
            for (k=0;k<w.tailleListe();k++){
                i = i + Collided2Obj(p, w.getElem(k));
                
            }
            Console.WriteLine("i" + i);
            return i;
        }
        */
        public static int Collided2Obj(Creature a,Plateforme b)
        {

                if (a.sprite.Bbox.Intersects(b.sprite.Bbox))
                {

                    if (IntersectPixels(a, b)) { return 1; }          
                }

            return 0;

        }

        public static void colllisionCreaturePlat(Creature a,Plateforme p, List<Vector2> inter)
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

            for (i = 0; i < p.sprite.pixelColor.Count(); i++)
            {
                p.sprite.pixelColor[i] = Color.White;

            }

            for (i = 0; i < inter.Count; i++)
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
                if (inter[i].Y >= p.sprite.Bbox.Bottom - margeErY && inter[i].Y <= p.sprite.Bbox.Bottom + margeErY && a.getVeutSauter())
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
                p.sprite.pixelColor[(int)((inter[i].X - p.sprite.Bbox.Left) +
                                     (inter[i].Y - p.sprite.Bbox.Top) * p.sprite.Bbox.Width)] = Color.Black;

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

                a.sprite.location.X = a.sprite.location.X + a.getVitesseX();
                if (inter[minX].X < p.sprite.Bbox.Right)//Il est dedans la boite il faut l'ejecter !
                {
                    a.sprite.location.X = a.sprite.location.X + (p.sprite.Bbox.Right - inter[minX].X);
                }
            }

            //Se cogne à gauche

            if (max == 0)
            {
                //Console.WriteLine("assss");
                //Console.WriteLine("minX" + inter[maxX].X + "R" + sprite.Bbox.Left);
                a.sprite.location.X = a.sprite.location.X - a.getVitesseX();
                if (inter[maxX].X > p.sprite.Bbox.Left)//Il est dedans la boite il faut l'ejecter !
                {
                    a.sprite.location.X = a.sprite.location.X - (inter[maxX].X - p.sprite.Bbox.Left);

                }
            }

            //se cogne en haut
            if (max == 2)
            {

                float Poids = a.getMasse() * a.getWorld().getGravity();
                float Accel = Poids + a.getSaut();
                //Console.WriteLine("Mass"+a.getMasse()+"P"+Poids+"M"+(0.5f * Accel + a.getVitesseY()));
                a.sprite.location.Y = a.sprite.location.Y - (0.5f * Accel + a.getVitesseY());
                a.collisionEnAir = true;
                if (inter[minY].Y > p.sprite.Bbox.Top)//Il est dedans la boite il faut l'ejecter !
                {
                    a.sprite.location.Y = a.sprite.location.Y - (inter[minY].Y - p.sprite.Bbox.Top);
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

        public static bool IntersectPixels(Creature a,Plateforme b)
        {
            Color colorA = new Color();
            Color colorB = new Color();


            int top = Math.Max(a.sprite.Bbox.Top, b.sprite.Bbox.Top);
            int bottom = Math.Min(a.sprite.Bbox.Bottom, b.sprite.Bbox.Bottom);
            int left = Math.Max(a.sprite.Bbox.Left, b.sprite.Bbox.Left);
            int right = Math.Min(a.sprite.Bbox.Right, b.sprite.Bbox.Right);

            List<Vector2> inter = new List<Vector2>();
            Vector2 temp=new Vector2();
            int i=0;
            int p = 0;
            int q = 0;
            int x = left;
            int y=top;


            for ( y = top; y < bottom; y++)
            {
                q = (int)a.sprite.frameIndex.Y * a.sprite.getFrameHeight() + (y-a.sprite.Bbox.Top);


                for ( x = left; x < right; x++)
                {
     
                    // On prend la couleur du pixel en ce pont

                    //p = (int)a.sprite.frameIndex.X * a.sprite.getFrameWidth()+(a.sprite.getFrameWidth()-(800-x));
                    p = (int)a.sprite.frameIndex.X * a.sprite.getFrameWidth()+(x-a.sprite.Bbox.Left);

                    colorA = a.sprite.pixelColor[p + q * a.sprite.getTexture().Width];
                    colorB = b.sprite.pixelColor[(x - b.sprite.Bbox.Left) +
                                         (y - b.sprite.Bbox.Top) * b.sprite.Bbox.Width];

                    // Si les 2 pixels ne snot pas cmoplètement transparent, 
                    if (colorA.A!=0 && colorB.A!=0)
                    {
                        temp.X=x;
                        temp.Y=y;
                        inter.Add(temp);
                        i++;
                        // alors une intersection est trouvé
                        

                    }
                }
            }


            if (inter.Count > 0) { 
            if (b.getTypeObjet() == GameObjects.TypeObjet.PLAT)
            {
                colllisionCreaturePlat(a,b, inter);
            }
            return true;
            }
            return false;
        }
    }


}
    
