﻿using System;
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
  

        public static int Collided (GameObjects c,int indObj,World w){
            int k;
            int i = 0;
            int size;

            //Tests les collision avec les platformes
            if ((size=w.tailleListPlat())>0)
            {
                for (k = 0; k < size; k++)
                {
                    if (!(c.getTypeObjet() == GameObjects.TypeObjet.PLAT && k != indObj))
                    {
                        i = i + Collided2Obj(c, w.getListPlat(k));
                    }

                }
            }
            //Tests les collision avec les Créatures
            if ((size = w.tailleListCreat()) > 0)
            {
                for (k = 0; k < size; k++)
                {

                    if ((c.getTypeObjet() == GameObjects.TypeObjet.PERS) || (c.getTypeObjet() == GameObjects.TypeObjet.ENN && k != indObj))
                    {
                        Collided2Obj(c, w.getListCreat(k));
                    }

                    

                }
            }

    
            
            return i;
        }
  
        public static int Collided2Obj(GameObjects a,GameObjects b)
        {
            
                if (a.sprite.Bbox.Intersects(b.sprite.Bbox))
                {
                    if (IntersectPixels(a, b))
                    {
                        //Console.WriteLine("a" + a.getTypeObjet() + "b" + b.getTypeObjet());
                        return 1;
                    }          
                }

            return 0;

        }


        

        public static bool IntersectPixels(GameObjects a,GameObjects b)
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
     

                    p = (int)a.sprite.frameIndex.X * a.sprite.getFrameWidth()+(x-a.sprite.Bbox.Left);

                    colorA = a.sprite.pixelColor[p + q * a.sprite.getTexture().Width];
                    colorB = b.sprite.pixelColor[(x - b.sprite.Bbox.Left) +
                                         (y - b.sprite.Bbox.Top) * b.sprite.Bbox.Width];

                    // Si les 2 pixels ne snot pas cmoplètement transparent, 
                    if (colorA.A!=0 && colorB.A!=0)
                    {
                        temp.X=x;
                        temp.Y=y;
                        //Console.WriteLine(x +" , "+ y);
                        inter.Add(temp);
                        i++;
                        // alors une intersection est trouvé
                        

                    }
                }
            }


            if (inter.Count > 0) { 
            
                a.colllision(b, inter);
            
            return true;
            }
            return false;
        }
    }


}
    
