using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Andy.Core
{
    public class GameObjects
    {
        
        public Sprite sprite;
        protected Direction _direction;
        public Rectangle _Source;
        public float _time;
        public GameObjects.Direction regard;




        public enum TypeObjet
        {
            PLAT = 0,
            PERS = 1,
            ENN = 2,
    
        }

        public enum Direction
        {
            NONE = -1,
            RIGHT = 0,
            LEFT = 1,
            BOT = 2,
            TOP = 3,
            PASS = 4,
            TAPER=5
        }



        protected TypeObjet typeobjet;

  


        public GameObjects(Sprite s)
        {
            sprite = s;

        }




        public TypeObjet getTypeObjet(){
            return typeobjet;
        }


        public GameObjects.Direction getDirection()
        {
            return _direction;
        }

        public virtual float getPVitesseX() //A E?LEVER D4ICI
        {
            return 0;

        }

        public virtual void setDegatRecul(Vector2 s)
        {
  
        }

        public virtual Vector2 getReculArme(){
            return new Vector2(0,0);
        }
        public virtual Vector4 getPosArmeR()
        {
            return new Vector4(0, 0, 0, 0);
        }
        public virtual Vector4 getPosArmeL()
        {
            return new Vector4(0, 0,0,0);
        }

        public virtual void prendreDegat(float a, float b){}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.getTexture(), sprite.location, Color.White);

        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.getTexture(), sprite.location, _Source, Color.White);
        }

        
        /*Collision virtual*/
        public virtual void colllision(GameObjects p, List<Vector2> inter) { }
       


  

    }
}
