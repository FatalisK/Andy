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
            PASS = 4
        }



        protected TypeObjet typeobjet;

  


        public GameObjects(Sprite s)
        {
            sprite = s;

        }


        public TypeObjet getTypeObjet(){
            return typeobjet;
        }






        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.getTexture(), sprite.location, Color.White);

        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.getTexture(), sprite.location, _Source, Color.White);
        }

        public void UpdateFrame(GameTime gameTime)
        {
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            while (_time > sprite.getFrameTime())
            {
                sprite.frameIndex.X++;
                _time = 0f;
            }
            if (sprite.frameIndex.X > sprite.getTotalFrame()) 
                sprite.frameIndex.X = 0;


            _Source = new Rectangle(
                (int)(sprite.frameIndex.X * sprite.getFrameWidth()),
               (int)(sprite.frameIndex.Y * sprite.getFrameHeight()),
                sprite.getFrameWidth(),
                sprite.getFrameHeight());

            

        }

        /*Collision virtual*/
        public virtual void colllision(GameObjects p, List<Vector2> inter) { }
       


  
        public bool inTheAir()
        {
            return (sprite.location.Y < 490);
        }
    }
}
