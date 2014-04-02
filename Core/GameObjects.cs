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
        protected float _gravity;
        protected Vector2 _vitesse;
        protected float _masse;
        public World _world;
        public Collision.Direction _direction;
        // Rectangle permettant de définir la zone de l'image à afficher
        public Rectangle _Source;
        // Durée depuis laquelle l'image est à l'écran
        public float _time;
        // Durée de visibilité d'une image
        
        // Indice de l'image en cours






  

        public GameObjects(Sprite s,float m,World world) { 

            sprite = s;
            _gravity = 9.81f;
            _vitesse = new Vector2(10, 10);
            _masse = m;
            this._world = world;
        
        }
        public GameObjects(Sprite s)
        {
            sprite = s;
            _gravity = 4;

        }

        public float getGravity()
        {
            return _gravity;
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

       

  

            /*
             * si tu veux être précis alors:
le vecteur d'accélération A=P+A0 avec P le poids et A0 l'accélération du à autre chose (propulsion, etc...)
le vecteur vitesse V
le vecteur D donnant le déplacement pour un incrément de temps t


tu initialises:
D.x,D.y,D.z la position de départ
V.x,V.y,V.z la vitesse initiale
et pour chaque cycle:
tu mets à jour la position:
D.x=.5*A0.x*t*t+V.x*t+D.x
D.y=.5*A0.y*t*t+V.y*t+D.y
D.z=.5*(A0.z-m*g)*t*t+V.z*t+D.y
PUIS la vitesse:
V.x=A0.x*t+V.x
V.y=A0.y*t+V.y
V.z=(A0.z-m*g)*t+V.z
            */



  
        public bool inTheAir()
        {
            return (sprite.location.Y < 490);
        }
    }
}
