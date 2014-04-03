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
        public enum Direction
        {
            NONE = -1,
            RIGHT = 0,
            LEFT = 1,
            BOT = 2,
            TOP = 3,
            PASS = 4
        }


        private static Color GetColorAt(GameObjects gameObject, World world)
        {
            return Color.Aquamarine;
        }

        public static void Collided (World a,World w){
            int k;
            int i = 0;
            for (k=0;k<w.tailleListe();k++){
                if (Collided2Obj(a, w.getElem(k))) { i++; }
            }

            if (i == 0) {

                if(a.getSol() != (a.sprite.location.Y + a.sprite.getFrameHeight())){
                
                a.setSol(600);

                }
                
            }
        }

        public static bool Collided2Obj(World a, World b)
        {
 
            
                if (a.sprite.Bbox.Intersects(b.sprite.Bbox))
                {
                   
 
                            IntersectPixels(a, b);

                  
                }
                else{

                    return false;
                }

                

            return false;

        }

        public static void IntersectPixels(World a, World b)
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
            if (b.typeobjet == GameObjects.TypeObjet.PLAT)
            {
                b.colllisionPlat(a, inter);

            }
            }
        }
    }


}
    
